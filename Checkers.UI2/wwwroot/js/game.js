"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();

var selectedCell;

function updateCheckerboard(gameState) {
    var cells = document.querySelectorAll('.cell');
    cells.forEach(cell => {
        cell.classList.remove('player1', 'player2', 'king', 'selected');
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

connection.start().then(function () {
    console.log('Connection started');
}).catch(function (err) {
    console.error(err.toString());
    alert('Could not connect to the game server. Please refresh the page to try again.');
});

connection.onclose(function () {
    alert('Connection to the game server was lost. Please refresh the page to reconnect.');
});

document.querySelectorAll('.cell').forEach(cell => {
    cell.addEventListener('click', function (event) {
        if (selectedCell) {
            var move = selectedCell.dataset.index + ' ' + cell.dataset.index;
            connection.invoke("SendMove", 'user', move).catch(function (err) {
                return console.error(err.toString());
            });
            selectedCell.classList.remove('selected');
            selectedCell = null;
        } else if (cell.classList.contains('player1') || cell.classList.contains('player2')) {
            selectedCell = cell;
            selectedCell.classList.add('selected');
        }
    });
});