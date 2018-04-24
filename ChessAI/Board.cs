﻿using ChessAI.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessAI
{
	class Board
	{
		// members
		public Piece[,] BoardTab { get; private set; }
		Player whitePlayer, blackPlayer;
		public int Turns { get; private set; }
        public Point LatestMoved { get; private set; }
        // constructors
        public Board(Player a, Player b)
		{
			Console.ForegroundColor = ConsoleColor.White;
			Turns = 0;
            LatestMoved = new Point(-1, -1);
			whitePlayer = a.color == Color.White ? a : b;
			blackPlayer = a.color != Color.White ? a : b;
			BoardTab = new Piece[8, 8];

			BoardTab[0, 0] = new Rook(Color.White); BoardTab[7, 0] = new Rook(Color.White);
			BoardTab[0, 7] = new Rook(Color.Black); BoardTab[7, 7] = new Rook(Color.Black);

			BoardTab[2, 0] = new Bishop(Color.White); BoardTab[5, 0] = new Bishop(Color.White);
			BoardTab[2, 7] = new Bishop(Color.Black); BoardTab[5, 7] = new Bishop(Color.Black);

			BoardTab[1, 0] = new Knight(Color.White); BoardTab[6, 0] = new Knight(Color.White);
			BoardTab[1, 7] = new Knight(Color.Black); BoardTab[6, 7] = new Knight(Color.Black);

			BoardTab[3, 0] = new Queen(Color.White); BoardTab[3, 7] = new Queen(Color.Black);

			BoardTab[4, 0] = new King(Color.White); BoardTab[4, 7] = new King(Color.Black);

			for (int i = 0; i < 8; i++)
			{
				BoardTab[i, 1] = new Pawn(Color.White);
				BoardTab[i, 6] = new Pawn(Color.Black);
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
					else if (BoardTab[width, height] != null)
					{
						if (BoardTab[width, height].color == Color.Black)
							Console.ForegroundColor = ConsoleColor.DarkGray;
						//display piece
						Console.Write(BoardTab[width, height].letter);

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
		//en passant - done
		//castling - done, but gonna need to come back to it once checking for win is done
		//double-step move - done
		//promotion - done

		//toggleable special moves?
		private Win Execute(Point[] move)
		{
			//save move and update board

            //need to check if my king won't be in check after move(maybe in GetMoves)
            if(BoardTab[move[0].x, move[0].y].letter=='P' && BoardTab[move[1].x, move[1].y]==null && move[0].x!=move[1].x)
                BoardTab[move[1].x, move[0].y] = null; //en passant
			BoardTab[move[0].x, move[0].y].moves++;
			BoardTab[move[1].x, move[1].y] = BoardTab[move[0].x, move[0].y];
			BoardTab[move[0].x, move[0].y] = null;
            Piece movedPiece = BoardTab[move[1].x, move[1].y];
            Color playerColor = movedPiece.color;
            //castling
            if(movedPiece.letter=='K' && Math.Abs(move[0].x-move[1].x)==2)
            {
                Point[] newMove = new Point[2] {
                    new Point((move[1].x - move[0].x) > 0 ? 7 : 0, move[1].y),
                    new Point((move[1].x + move[0].x) / 2, move[1].y)
                };
                Win w = Execute(newMove); //to rethink (when checking for win is done)
            }
            //pawn promotion
            if ((
					(move[1].y == 7 && playerColor == Color.White)
					||
					(move[1].y == 0 && playerColor == Color.Black)
				)
				&& movedPiece.letter == 'P')
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
							newPiece = new Queen(playerColor);
							break;
						case 'N':
                            newPiece = new Knight(playerColor);
							break;
						case 'R':
							newPiece = new Rook(playerColor);
							break;
						case 'B':
							newPiece = new Bishop(playerColor);
							break;
						default:
							continue;
					}
					if (newPiece != null)
						break;
				}
                newPiece.moves = movedPiece.moves;
				BoardTab[move[1].x, move[1].y] = newPiece;
            }
            this.LatestMoved.x = move[1].x;
            this.LatestMoved.y = move[1].y;
            //move history?
            //handle special moves

            //check for checkmate/stalemate and other draw options

            //possible optimalization:
            //start searching from top for white player and from bottom player
            //stop searching when found nr of pieces they have (maybe keep nr of them somewhere)
            List<Piece> checingPieces = new List<Piece>();
            for (int height = 0; height < 8; height++)
            {
                for (int width = 0; width < 8; width++)
                {
                    if (BoardTab[width, height] != null && BoardTab[width, height].color == playerColor)
                    {
                        Piece piece = BoardTab[width, height];
                        List<Point> checking = piece.GetMoves(this, new Point(width, height)).Where(
                            x => { Piece dest = BoardTab[x.x, x.y]; return dest == null ? false : (dest.color != playerColor && dest.letter == 'K'); }
                            ).ToList();
                        checingPieces.Add(piece);
                    }
                }
            }

            if (checingPieces.Count == 0)
                return Win.None;
            else
            {
                //pseudo code to differentiate between check and checkmate
                //simulate each possible opponent move, then run checking for check again
                //if check stopped - cool
                //else continue checking
                //nothing stopped it - checkmate
                //^ optimalization - checking for check can be started with peices that were checking it before,
                //but the rest should be checked too in case:
                //B Q|
                //Br | player color differentiated by upper/lower case letters
                //  k| can't move rook to the left, because bishop will be able to hit king
                //---* so that needs to be detected as check mate

                if (true) //temp - should be: can opponent block, which is kinda hard as described above
                    return playerColor==Color.Black ? Win.BlackCheck : Win.WhiteCheck;
                else return playerColor==Color.Black? Win.Black : Win.White;
            }
            //stalemate?
		}
		public int Evaluate()
		{
			//tells in how good position player is
			throw new NotImplementedException();
		}
		public Win Turn()
		{
			Turns++;

			Win win = Execute(whitePlayer.Decide(this));

			if (win == Win.White  || win == Win.Stalemate)
				return win;
			win = Execute(blackPlayer.Decide(this));
			return win;
		}
	}
}
