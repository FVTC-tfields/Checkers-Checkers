"use strict";

window.onload = function () {
    var connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();

    var selectedPiece = null;

    connection.on("ReceiveMove", function (oldX, oldY, newX, newY) {
        var oldCell = document.getElementById(oldX + "-" + oldY);
        var newCell = document.getElementById(newX + "-" + newY);
        newCell.appendChild(oldCell.getElementsByClassName("piece")[0]);
    });

    connection.start().then(function () {
        console.log('SignalR Connected!');
    }).catch(function (err) {
        return console.error(err.toString());
    });

    // Add click event listeners to the pieces
    var pieces = document.getElementsByClassName("piece");
    for (var i = 0; i < pieces.length; i++) {
        pieces[i].addEventListener("click", function (event) {
            console.log('Piece clicked');
            if (selectedPiece) {
                var oldId = selectedPiece.parentNode.id.split("-");
                var newId = this.parentNode.id.split("-");
                connection.invoke("SendMove", oldId[0], oldId[1], newId[0], newId[1]).catch(function (err) {
                    return console.error(err.toString());
                });
                selectedPiece = null;
            } else {
                selectedPiece = this;
            }
            event.stopPropagation();
        });
    }

    // Add click event listeners to the board
    for (var i = 0; i < 8; i++) {
        for (var j = 0; j < 8; j++) {
            var cell = document.getElementById(i + "-" + j);
            cell.addEventListener("click", function (event) {
                console.log('Cell clicked');
                if (selectedPiece) {
                    var oldId = selectedPiece.parentNode.id.split("-");
                    var newId = this.id.split("-");
                    connection.invoke("SendMove", oldId[0], oldId[1], newId[0], newId[1]).catch(function (err) {
                        return console.error(err.toString());
                    });
                    this.appendChild(selectedPiece);
                    selectedPiece = null;
                }
            });
        }
    }
}