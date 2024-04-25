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
        self.red_king_image = tk.PhotoImage(file="Photo/black_king.png")

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

    def on_click(self, event, x, y):
        if self.selected_piece is None:
            piece = self.board[x][y]
            if piece is not None and piece.color == self.turn:
                self.selected_piece = piece
                self.selected_square = (x, y)
        else:
            if self.validate_move(self.selected_piece, self.selected_square, (x, y)):
                move = {'start': self.selected_square, 'end': (x, y)}
                #self.sio.emit('send move', {'move': move})
                self.move_piece(self.selected_piece, self.selected_square, (x, y))
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
        if piece.king:
            if abs(start[0] - end[0]) != 1 and abs(start[0] - end[0]) != 2:
                return False
        else:
            if piece.color == "R" and start[0] - end[0] != 1 and start[0] - end[0] != 2:
                return False
            if piece.color == "B" and end[0] - start[0] != 1 and end[0] - start[0] != 2:
                return False
        if abs(start[1] - end[1]) != 1 and abs(start[1] - end[1]) != 2:
            return False
        if abs(start[0] - end[0]) == 2 and self.board[(start[0] + end[0]) // 2][(start[1] + end[1]) // 2] is None:
            return False
        return True

    def move_piece(self, piece, start, end):
        self.board[start[0]][start[1]] = None
        self.board[end[0]][end[1]] = piece
        if start[0] - end[0] == 2:
            self.board[(start[0] + end[0]) // 2][(start[1] + end[1]) // 2] = None
        elif end[0] - start[0] == 2:
            self.board[(start[0] + end[0]) // 2][(start[1] + end[1]) // 2] = None
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


class Game:
    def __init__(self, sio):
        root = tk.Tk()
        root.geometry("605x645")
        self.board = Board(root, sio)
        self.turn = "W"
        self.board.print_board()
        root.mainloop()

sio = socketio.Client()

game = Game(sio)

@sio.event
def connect():
    print('connection established')

@sio.event
def disconnect():
    print('disconnected from server')

@sio.event
def receive_game_state(data):
    game_state = data['gameState']
    game.board.update_from_game_state(game_state)

sio.connect('http://localhost:7270')
sio.wait()