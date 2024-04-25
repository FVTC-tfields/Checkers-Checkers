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
        var startX = int.Parse(parts[0]);
        var startY = int.Parse(parts[1]);
        var endX = int.Parse(parts[2]);
        var endY = int.Parse(parts[3]);

        if (!ValidateMove(startX, startY, endX, endY, gameState[startX, startY]))
        {
            return;
        }

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
            // Captured - remove piece
            gameState[(startX + endX) / 2, (startY + endY) / 2] = 0;

            gameState[endX, endY] = gameState[startX, startY];
            gameState[startX, startY] = 0;

            // Multiple capture
            if (isKing && (
                ValidateMove(endX, endY, endX + 2, endY + 2, piece) ||
                ValidateMove(endX, endY, endX + 2, endY - 2, piece) ||
                ValidateMove(endX, endY, endX - 2, endY + 2, piece) ||
                ValidateMove(endX, endY, endX - 2, endY - 2, piece)))
            {
                return true;
            }
            else if (!isKing && (
                ValidateMove(endX, endY, endX + 2, endY + 2, piece) ||
                ValidateMove(endX, endY, endX + 2, endY - 2, piece)))
            {
                return true;
            }
        }

        return false;
    }
}