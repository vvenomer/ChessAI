using ChessAI.Pieces;
using System;

namespace ChessAI
{
	class Board
	{
		// members
		public Piece[,] board { get; private set; }
		Player whitePlayer, blackPlayer;
		public int turns { get; private set; }

		// constructors
		public Board(Player a, Player b)
		{
			Console.ForegroundColor = ConsoleColor.White;
			turns = 0;
			whitePlayer = a.color == Color.White ? a : b;
			blackPlayer = a.color != Color.White ? a : b;
			board = new Piece[8, 8];

			board[0, 0] = new Rook(Color.White); board[7, 0] = new Rook(Color.White);
			board[0, 7] = new Rook(Color.Black); board[7, 7] = new Rook(Color.Black);

			board[2, 0] = new Bishop(Color.White); board[5, 0] = new Bishop(Color.White);
			board[2, 7] = new Bishop(Color.Black); board[5, 7] = new Bishop(Color.Black);

			board[1, 0] = new Knight(Color.White); board[6, 0] = new Knight(Color.White);
			board[1, 7] = new Knight(Color.Black); board[6, 7] = new Knight(Color.Black);

			board[3, 0] = new Queen(Color.White); board[3, 7] = new Queen(Color.Black);

			board[4, 0] = new King(Color.White); board[4, 7] = new King(Color.Black);

			for (int i = 0; i < 8; i++)
			{
				board[i, 1] = new Pawn(Color.White);
				board[i, 6] = new Pawn(Color.Black);
			}
		}

		// methods
		public void Print(Point[] markers)
		{
			Console.Clear();
			for (int height = 0; height < 9; height++)
			{
				//display vertical line over the squares
				for (int width = 0; width < 19; width++)
					Console.Write((width % 2 == 0) ? "+" : "-");
				Console.WriteLine();
				for (int width = -1; width < 8; width++)
				{
					//put horizontal line before square
					Console.Write("|");
					if (markers != null)
					{
						foreach (Point marker in markers)
						{
							if (marker.x == width && marker.y == height)
								Console.BackgroundColor = ConsoleColor.DarkRed;
						}
					}
					if (width == -1)
					{
						//number the rows
						if (height < 8)
							Console.Write(8 - height);
						else
							Console.Write(" ");
					}
					//number the columns
					else if (height == 8)
						Console.Write((char)('a' + width));
					else if (board[width, height] != null)
					{
						if (board[width, height].color == Color.Black)
							Console.ForegroundColor = ConsoleColor.DarkGray;
						//display piece
						Console.Write(board[width, height].letter);

					}
					else Console.Write(' ');
					Console.BackgroundColor = ConsoleColor.Black;
					Console.ForegroundColor = ConsoleColor.White;
					//additional verical line on the very right
					if (width == 7)
						Console.Write("|");
				}
				Console.WriteLine();
				if (height == 8)
				{
					//additional horizontal line on the very bottom
					for (int w = 0; w < 19; w++)
						Console.Write((w % 2 == 0) ? "+" : "-");
					Console.WriteLine();
				}
			}
		}
		//Special moves: - to include in GetMoves method of Piece class
		//en passant - described in pawn class
		//castling 
		//double-step move - done
		//promotion - done

		//toggleable special moves?
		private Win Execute(Point[] move)
		{
			//save move and update board

			board[move[0].x, move[0].y].moves++;
			board[move[1].x, move[1].y] = board[move[0].x, move[0].y];
			board[move[0].x, move[0].y] = null;
			//pawn promotion
			if ((
					(move[1].y == 7 && board[move[1].x, move[1].y].color == Color.White)
					||
					(move[1].y == 0 && board[move[1].x, move[1].y].color == Color.Black)
				)
				&& board[move[1].x, move[1].y].letter == 'P')
			{
				//promote this pawn
				Console.WriteLine("Pionek doszedł do linii przemiany. Wybierz na co chcesz go promować:");
				Console.WriteLine("Q - hetman, N - goniec, R - wieża, B - skoczek");
				string figure;
				Piece newPiece = null;
				while (true)
				{
					figure = Console.ReadLine();
					if (figure.Length != 1) continue;
					switch (figure[0])
					{
						case 'Q':
							newPiece = new Queen(board[move[1].x, move[1].y].color);
							break;
						case 'N':
                            newPiece = new Knight(board[move[1].x, move[1].y].color);
							break;
						case 'R':
							newPiece = new Rook(board[move[1].x, move[1].y].color);
							break;
						case 'B':
							newPiece = new Bishop(board[move[1].x, move[1].y].color);
							break;
						default:
							continue;
					}
					if (newPiece != null)
						break;
				}
				board[move[1].x, move[1].y] = newPiece;
			}
			//move history?
			//handle special moves
			//check for checkmate/stalemate and other draw options
			return Win.None;
		}
		public int Evaluate()
		{
			//tells in how good position player is
			throw new NotImplementedException();
		}
		public Win Turn()
		{
			turns++;

			Win win = Execute(whitePlayer.Decide(this));
			if (win != Win.None)
				return win;
			win = Execute(blackPlayer.Decide(this));
			return win;
		}
	}
}
