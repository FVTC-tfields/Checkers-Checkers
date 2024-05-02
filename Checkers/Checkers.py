import socketio
import tkinter as tk
from tkinter import messagebox
from PIL import Image, ImageTk


class Piece:
    def __init__(self, color):
        self.color = color
        self.king = False

    def to_tuple(self):
        return self.color, self.king


class Board:
    def __init__(self, root, sio):
        self.root = root
        self.sio = sio
        self.board = self.create_board()
        self.selected_piece = None
        self.selected_square = None
        self.turn = "B"
        self.black_piece_image = tk.PhotoImage(file="Photo/black_piece.png")
        self.red_piece_image = tk.PhotoImage(file="Photo/red_piece.png")
        self.black_king_image = tk.PhotoImage(file="Photo/black_king.png")
        self.red_king_image = tk.PhotoImage(file="Photo/red_king.png")

    def create_board(self):
        board = [[None for _ in range(8)] for _ in range(8)]
        for i in range(8):
            for j in range(8):
                if i < 3 and (i + j) % 2 == 0:
                    board[i][j] = Piece("B")
                elif i > 4 and (i + j) % 2 == 0:
                    board[i][j] = Piece("R")
        return board

    def print_board(self):
        for i, row in enumerate(self.board):
            for j, piece in enumerate(row):
                if (i + j) % 2 == 0:
                    color = "white"
                else:
                    color = "black"
                if piece is None:
                    label = tk.Label(self.root, text=" ", width=10, height=5, bg=color)
                elif piece.color == "B":
                    if piece.king:
                        label = tk.Label(self.root, image=self.black_king_image, width=10, height=5, bg=color)
                    else:
                        label = tk.Label(self.root, image=self.black_piece_image, width=10, height=5, bg=color)
                else:
                    if piece.king:
                        label = tk.Label(self.root, image=self.red_king_image, width=10, height=5, bg=color)
                    else:
                        label = tk.Label(self.root, image=self.red_piece_image, width=10, height=5, bg=color)
                label.grid(row=i, column=j)
                label.bind("<Button-1>", lambda event, x=i, y=j: self.on_click(event, x, y))

    def can_capture(self, piece, position):
        x, y = position
        for dx, dy in [(-2, -2), (-2, 2), (2, -2), (2, 2)]:
            nx, ny = x + dx, y + dy
            if 0 <= nx < 8 and 0 <= ny < 8:
                if self.board[(x + nx) // 2][(y + ny) // 2] is not None and \
                   self.board[(x + nx) // 2][(y + ny) // 2].color != piece.color and \
                   self.board[nx][ny] is None:
                    return True
        return False

    def on_click(self, event, x, y):
        piece = self.board[x][y]
        if self.selected_piece is None:
            if piece is not None and piece.color == self.turn:
                self.selected_piece = piece
                self.selected_square = (x, y)
        else:
            if (x, y) == self.selected_square:
                # Deselect the piece if the same piece is clicked again
                self.selected_piece = None
                self.selected_square = None
            elif self.validate_move(self.selected_piece, self.selected_square, (x, y)):
                move = {'start': self.selected_square, 'end': (x, y)}
                self.pending_move = move  # Store the move to be sent later
                self.move_piece(self.selected_piece, self.selected_square, (x, y))
                if abs(self.selected_square[0] - x) == 2 and abs(self.selected_square[1] - y) == 2:
                    if self.can_capture(self.selected_piece, (x, y)):
                        self.selected_square = (x, y)
                    else:
                        self.selected_piece = None
                        self.selected_square = None
                        self.print_board()
                        self.switch_turn()
                else:
                    self.selected_piece = None
                    self.selected_square = None
                    self.print_board()
                    self.switch_turn()
                winner = self.check_win()
                if winner is not None:
                    self.announce_winner(winner)

    def announce_winner(self, winner):
        winner_color = "Black" if winner == "B" else "Red"
        messagebox.showinfo("Game Over", f"{winner_color} wins!")

    def switch_turn(self):
        self.turn = "R" if self.turn == "B" else "B"

    def validate_move(self, piece, start, end):
        if self.board[end[0]][end[1]] is not None:
            return False
        if abs(start[0] - end[0]) != 1 and abs(start[0] - end[0]) != 2:
            return False
        if abs(start[1] - end[1]) != 1 and abs(start[1] - end[1]) != 2:
            return False
        if abs(start[0] - end[0]) == 2 and self.board[(start[0] + end[0]) // 2][(start[1] + end[1]) // 2] is None:
            return False
        if abs(start[0] - end[0]) == 2 and self.board[(start[0] + end[0]) // 2][(start[1] + end[1]) // 2].color == piece.color:
            return False
        if not piece.king and ((piece.color == "R" and start[0] < end[0]) or (piece.color == "B" and start[0] > end[0])):
            return False
        return True

    def move_piece(self, piece, start, end):
        self.board[start[0]][start[1]] = None
        self.board[end[0]][end[1]] = piece
        if abs(start[0] - end[0]) == 2:
            self.board[(start[0] + end[0]) // 2][(start[1] + end[1]) // 2] = None
            self.selected_piece = piece
            self.selected_square = end
            self.print_board()
        if end[0] == 0 or end[0] == 7:
            piece.king = True

    def check_win(self):
        red_pieces = 0
        black_pieces = 0
        for row in self.board:
            for piece in row:
                if piece is not None:
                    if piece.color == "R":
                        red_pieces += 1
                    else:
                        black_pieces += 1
        if red_pieces == 0:
            return "B"
        elif black_pieces == 0:
            return "R"
        else:
            return None

    def update_from_game_state(self, game_state):
        for i in range(8):
            for j in range(8):
                piece_data = game_state[i][j]
                if piece_data is None:
                    self.board[i][j] = None
                else:
                    color, king = piece_data
                    piece = Piece(color)
                    piece.king = king
                    self.board[i][j] = piece
        self.print_board()


class InitialScreen:
    def __init__(self, game):
        self.root = tk.Tk()
        self.root.geometry("605x645")
        self.game = game
        self.start_button = tk.Button(self.root, text="Start Game", command=self.start_game)
        self.start_button.pack()
        self.root.mainloop()

    def start_game(self):
        self.root.destroy()
        self.game.start_game()


class Game:
    def __init__(self, sio):
        self.sio = sio
        self.board = None

    def start_game(self):
        root = tk.Tk()
        root.geometry("605x645")
        self.board = Board(root, self.sio)
        self.turn = "W"
        self.board.print_board()
        root.mainloop()


sio = socketio.Client()

game = Game(sio)
initial_screen = InitialScreen(game)

@sio.event
def connect():
    print('connection established')
    if game.board.pending_move is not None:
        sio.emit('send move', {'move': game.board.pending_move})
        game.board.pending_move = None

@sio.event
def disconnect():
    print('disconnected from server')

@sio.event
def receive_game_state(data):
    game_state = data['gameState']
    game.board.update_from_game_state(game_state)

sio.connect('http://localhost:7270')
sio.wait()