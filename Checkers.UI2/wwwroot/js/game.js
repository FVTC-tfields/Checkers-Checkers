"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();

function updateCheckerboard(gameState) {
    var cells = document.querySelectorAll('.cell');
    cells.forEach(cell => {
        cell.classList.remove('player1', 'player2', 'king');
    });

    for (var i = 0; i < 8; i++) {
        for (var j = 0; j < 8; j++) {
            var cell = cells[i * 8 + j];
            if (gameState[i][j] == 1) {
                cell.classList.add('player1');
            } else if (gameState[i][j] == 2) {
                cell.classList.add('player2');
            } else if (gameState[i][j] == 3) {
                cell.classList.add('player1', 'king');
            } else if (gameState[i][j] == 4) {
                cell.classList.add('player2', 'king');
            }
        }
    }
}

connection.on("ReceiveGameState", function (gameState) {
    updateCheckerboard(gameState);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("moveButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var move = document.getElementById("moveInput").value;
    connection.invoke("SendMove", user, move).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});