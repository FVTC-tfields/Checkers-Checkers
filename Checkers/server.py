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
            self.emit_game_state()
            return
        color, king = piece
        if color != self.current_turn:
            self.emit_game_state()
            return
        if abs(start[0] - end[0]) != 1 and abs(start[0] - end[0]) != 2:
            self.emit_game_state()
            return
        if abs(start[1] - end[1]) != 1 and abs(start[1] - end[1]) != 2:
            self.emit_game_state()
            return
        if abs(start[0] - end[0]) == 2 and self.game_state[(start[0] + end[0]) // 2][(start[1] + end[1]) // 2] is None:
            self.emit_game_state()
            return
        if not king and ((color == "R" and start[0] < end[0]) or (color == "B" and start[0] > end[0])):
            self.emit_game_state()
            return
        self.game_state[start[0]][start[1]] = None
        self.game_state[end[0]][end[1]] = (color, king or end[0] in {0, 7})
        if abs(start[0] - end[0]) == 2:
            self.game_state[(start[0] + end[0]) // 2][(start[1] + end[1]) // 2] = None
            if not self.can_capture(end):
                self.switch_turn()
        else:
            self.switch_turn()
        self.emit_game_state()

    def emit_game_state(self):
        self.socketio.emit('receive game state', {'gameState': self.game_state}, broadcast=True)

    def can_capture(self, position):
        color, king = self.game_state[position[0]][position[1]]
        opponent = "B" if color == "R" else "R"
        directions = [(1, 1), (1, -1), (-1, 1), (-1, -1)]
        if not king:
            if color == "R":
                directions = [(1, 1), (1, -1)]  # Regular red pieces can only move upwards
            else:
                directions = [(-1, 1), (-1, -1)]  # Regular black pieces can only move downwards
        for dx, dy in directions:
            x, y = position[0] + dx, position[1] + dy
            nx, ny = x + dx, y + dy
            if (0 <= x < 8 and 0 <= y < 8 and self.game_state[x][y] is not None and
                self.game_state[x][y][0] == opponent and
                0 <= nx < 8 and 0 <= ny < 8 and self.game_state[nx][ny] is None):
                return True
        return False

    def switch_turn(self):
        self.current_turn = "B" if self.current_turn == "R" else "R"

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