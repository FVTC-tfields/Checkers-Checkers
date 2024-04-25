# server.py

from flask import Flask
from flask_socketio import SocketIO, emit

app = Flask(__name__)
socketio = SocketIO(app)


class ServerGame:
    def __init__(self, socketio):
        self.socketio = socketio
        self.game_state = [[None for _ in range(8)] for _ in range(8)]
        self.current_turn = "B"
        for i in range(8):
            for j in range(8):
                if i < 3 and (i + j) % 2 == 0:
                    self.game_state[i][j] = ("B", False)
                elif i > 4 and (i + j) % 2 == 0:
                    self.game_state[i][j] = ("R", False)

    def validate_and_update(self, move):
        start, end = move['start'], move['end']
        piece = self.game_state[start[0]][start[1]]
        if piece is None or self.game_state[end[0]][end[1]] is not None:
            return
        color, king = piece
        if color != self.current_turn:
            return
        if abs(start[0] - end[0]) != 1 and abs(start[0] - end[0]) != 2:
            return
        if abs(start[1] - end[1]) != 1 and abs(start[1] - end[1]) != 2:
            return
        if abs(start[0] - end[0]) == 2 and self.game_state[(start[0] + end[0]) // 2][(start[1] + end[1]) // 2] is None:
            return
        self.game_state[start[0]][start[1]] = None
        self.game_state[end[0]][end[1]] = (color, king or end[0] in {0, 7})
        if abs(start[0] - end[0]) == 2:
            self.game_state[(start[0] + end[0]) // 2][(start[1] + end[1]) // 2] = None
        self.current_turn = "B" if self.current_turn == "R" else "R"
        self.socketio.emit('receive game state', {'gameState': self.game_state}, broadcast=True)

game = ServerGame(socketio)

@app.route('/')
def index():
    return "Checkers Game Server"

@socketio.on('send move')
def handle_move(data):
    move = data['move']
    game.validate_and_update(move)

if __name__ == '__main__':
    socketio.run(app)