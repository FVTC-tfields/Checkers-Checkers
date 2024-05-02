using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

public class GameHub : Hub
{
    private static int[,] gameState = {
        {0, 1, 0, 1, 0, 1, 0, 1},
        {1, 0, 1, 0, 1, 0, 1, 0},
        {0, 1, 0, 1, 0, 1, 0, 1},
        {0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0},
        {2, 0, 2, 0, 2, 0, 2, 0},
        {0, 2, 0, 2, 0, 2, 0, 2},
        {2, 0, 2, 0, 2, 0, 2, 0}
    };

    public override async Task OnConnectedAsync()
    {
        await Clients.Caller.SendAsync("ReceiveGameState", gameState);
        await base.OnConnectedAsync();
    }

    public async Task SendMove(string user, string move)
    {
        var parts = move.Split(' ');
        var startIndex = int.Parse(parts[0]);
        var endIndex = int.Parse(parts[1]);

        var startX = startIndex / 8;
        var startY = startIndex % 8;
        var endX = endIndex / 8;
        var endY = endIndex % 8;

        if (!ValidateMove(startX, startY, endX, endY, gameState[startX, startY]))
        {
            return;
        }

        // Move the piece in the game state
        gameState[endX, endY] = gameState[startX, startY];
        gameState[startX, startY] = 0;

        // Upgrade to king
        if (endX == 0 && gameState[endX, endY] == 1)
        {
            // Player 1 to king
            gameState[endX, endY] = 3;
        }
        else if (endX == 7 && gameState[endX, endY] == 2)
        {
            // Player 2 to king
            gameState[endX, endY] = 4;
        }

        // Check for win
        bool player1HasPieces = false;
        bool player2HasPieces = false;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (gameState[i, j] == 1 || gameState[i, j] == 3)
                {
                    player1HasPieces = true;
                }
                else if (gameState[i, j] == 2 || gameState[i, j] == 4)
                {
                    player2HasPieces = true;
                }
            }
        }

        if (!player1HasPieces)
        {
            // Player 2 wins
            await Clients.All.SendAsync("EndGame", "Player 2 wins");
        }
        else if (!player2HasPieces)
        {
            // Player 1 wins
            await Clients.All.SendAsync("EndGame", "Player 1 wins");
        }

        await Clients.All.SendAsync("ReceiveGameState", gameState);
    }

    private bool ValidateMove(int startX, int startY, int endX, int endY, int piece)
    {
        // Check if king
        bool isKing = piece == 3 || piece == 4;

        // Check if capture
        if (Math.Abs(endX - startX) == 2 && Math.Abs(endY - startY) == 2)
        {
            int midX = (startX + endX) / 2;
            int midY = (startY + endY) / 2;

            // Check if the piece being jumped over is an opponent's
            if (gameState[midX, midY] == 0 || gameState[midX, midY] == piece || gameState[midX, midY] == piece + 2)
            {
                return false;
            }

            // Capture is valid - update game state
            gameState[midX, midY] = 0;
            gameState[endX, endY] = gameState[startX, startY];
            gameState[startX, startY] = 0;
            return true;
        }

        // Add validation for normal moves
        else if (Math.Abs(endX - startX) == 1 && Math.Abs(endY - startY) == 1)
        {
            // Check if the destination square is empty
            if (gameState[endX, endY] != 0)
            {
                return false;
            }

            // Check if the move is forward for normal pieces
            if (!isKing && ((piece == 1 && endX <= startX) || (piece == 2 && endX >= startX)))
            {
                return false;
            }

            // Move is valid - update game state
            gameState[endX, endY] = gameState[startX, startY];
            gameState[startX, startY] = 0;
            return true;
        }

        return false;
    }
}