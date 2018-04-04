
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessAI
{
	class Human : Player
	{
		public Human(Color color) : base(color) { }
		private Point GetPiece(string message)
		{
			string piece;
			while (true)
			{
				Console.WriteLine(message);
				piece = Console.ReadLine();
				if (piece.Length < 2 || piece[0] < 'a' || piece[0] > 'h' || piece[1] < '0' || piece[1] > '9')
				{
					Console.WriteLine("Złe polecenie");
					continue;
				}
				break;
			}
            return new Point(piece[0] - 'a', 8 - (piece[1] - '0'));

		}
		public override Point[] Decide(Board board)
		{
			board.Print();
			Console.WriteLine("Tura nr" + board.turns + " " + (color == Color.White ? "Białych" : "Czarnych"));
			Point[] res = new Point[2];
            Piece onBoard;
            Point[] moves;
			while (true)
			{
				res[0] = GetPiece("Wybierz bierkę");
				onBoard = board.board[res[0].x, res[0].y];
				if (onBoard == null || onBoard.color != color)
				{
					//not your piece
					Console.WriteLine("To nie jest twoja bierka");
				}
				else break;
			}
            moves = onBoard.GetMoves(board, res[0]);
			while (true)
            {
                Console.WriteLine("Dostępne ruchy");
                foreach (Point move in moves)
                {
                    Console.Write( (char)('a' + move.x) + (8-move.y).ToString() + " " );
                }
                Console.WriteLine();
                res[1] = GetPiece("Ustaw wybraną bierkę");
				if (!Array.Exists(moves, x => x.x == res[1].x && x.y == res[1].y))
				{
					//your piece
					Console.WriteLine("Nie możesz wykonać takiego ruchu");
				}
				else break;
			}
			return res;
		}
	}
}
