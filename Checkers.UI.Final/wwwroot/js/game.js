"use strict";

window.onload = function () {
    var connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();

    var selectedPiece = null;

    connection.on("ReceiveMove", function (oldX, oldY, newX, newY, captured) {
        var oldCell = document.getElementById(oldX + "-" + oldY);
        var newCell = document.getElementById(newX + "-" + newY);
        newCell.appendChild(oldCell.getElementsByClassName("piece")[0]);
        if (captured) {
            var capturedCell = document.getElementById(captured[0] + "-" + captured[1]);
            capturedCell.removeChild(capturedCell.getElementsByClassName("piece")[0]);
            checkEndOfGame();
        }
    });

    connection.start().then(function () {
        console.log('SignalR Connected!');
    }).catch(function (err) {
        return console.error(err.toString());
    });

    function checkEndOfGame() {
        var blackPieces = document.getElementsByClassName("piece black");
        var redPieces = document.getElementsByClassName("piece red");

        if (blackPieces.length === 0) {
            alert("Red wins!");
            // Game state logic
        } else if (redPieces.length === 0) {
            alert("Black wins!");
            // Game state logic
        }
    }

    // Pieces
    var pieces = document.getElementsByClassName("piece");
    for (var i = 0; i < pieces.length; i++) {
        pieces[i].addEventListener("click", function (event) {
            console.log('Piece clicked');
            if (selectedPiece) {
                var oldId = selectedPiece.parentNode.id.split("-").map(Number);
                var newId = this.parentNode.id.split("-").map(Number);
                if (Math.abs(newId[0] - oldId[0]) === 1 && Math.abs(newId[1] - oldId[1]) === 1) {
                    connection.invoke("SendMove", oldId[0], oldId[1], newId[0], newId[1], null).catch(function (err) {
                        return console.error(err.toString());
                    });
                    selectedPiece = null;
                } else if (Math.abs(newId[0] - oldId[0]) === 2 && Math.abs(newId[1] - oldId[1]) === 2) {
                    var captured = [(oldId[0] + newId[0]) / 2, (oldId[1] + newId[1]) / 2];
                    connection.invoke("SendMove", oldId[0], oldId[1], newId[0], newId[1], captured).catch(function (err) {
                        return console.error(err.toString());
                    });
                    var capturedCell = document.getElementById(captured[0] + "-" + captured[1]);
                    capturedCell.removeChild(capturedCell.getElementsByClassName("piece")[0]);
                    checkEndOfGame();
                    selectedPiece = null;
                }
            } else {
                selectedPiece = this;
            }
            event.stopPropagation();
        });
    }

    // Board
    for (var i = 0; i < 8; i++) {
        for (var j = 0; j < 8; j++) {
            var cell = document.getElementById(i + "-" + j);
            cell.addEventListener("click", function (event) {
                console.log('Cell clicked');
                if (selectedPiece) {
                    var oldId = selectedPiece.parentNode.id.split("-").map(Number);
                    var newId = this.id.split("-").map(Number);
                    if (Math.abs(newId[0] - oldId[0]) === 1 && Math.abs(newId[1] - oldId[1]) === 1) {
                        connection.invoke("SendMove", oldId[0], oldId[1], newId[0], newId[1], null).catch(function (err) {
                            return console.error(err.toString());
                        });
                        this.appendChild(selectedPiece);
                        selectedPiece = null;
                    } else if (Math.abs(newId[0] - oldId[0]) === 2 && Math.abs(newId[1] - oldId[1]) === 2) {
                        var captured = [(oldId[0] + newId[0]) / 2, (oldId[1] + newId[1]) / 2];
                        connection.invoke("SendMove", oldId[0], oldId[1], newId[0], newId[1], captured).catch(function (err) {
                            return console.error(err.toString());
                        });
                        this.appendChild(selectedPiece);
                        var capturedCell = document.getElementById(captured[0] + "-" + captured[1]);
                        capturedCell.removeChild(capturedCell.getElementsByClassName("piece")[0]);
                        checkEndOfGame();
                        selectedPiece = null;
                    }
                }
            });
        }
    }
}