using ChessAI.Pieces;
using System;

namespace ChessAI
{
    class Board
    {
        
        public Piece[,] board { get; private set; }
        Player whitePlayer, blackPlayer;
        public int turns { get; private set; }
        public Board(Player a, Player b)
        {
            Console.ForegroundColor = ConsoleColor.White;
            turns = 0;
            whitePlayer = a.color == Color.White ? a : b;
            blackPlayer = a.color != Color.White ? a : b;
            board = new Piece[8,8];
            //board[0, 0] = board[7, 0] = board[0, 7] = board[7, 7] = (byte)Piece.Rook;
            //board[1, 0] = board[6, 0] = board[1, 7] = board[6, 7] = (byte)Piece.Knight;
            //board[2, 0] = board[5, 0] = board[2, 7] = board[5, 7] = (byte)Piece.Bishop;
            //board[3, 0] = board[4, 7] = (byte)Piece.Queen;
            board[4, 0] = new King(Color.White);
            board[3, 7] = new King(Color.Black);
            for(int i = 0; i < 8; i++)
            {
                board[i, 1] = new Pawn(Color.White);
                board[i, 6] = new Pawn(Color.Black);
            }
        }
        public void Print()
        {
			Console.Clear();
            for(int h = 0; h < 9; h++)
            {
                //display vertical line over the squares
                for (int w = 0; w < 19; w++)
                    Console.Write( (w%2==0) ? "+" : "-");
                Console.WriteLine();
                for(int w = -1; w < 8; w++)
                {
                    //put horizontal line before square
                    Console.Write("|");
                    if (w == -1)
                    {
                        //number the rows
                        if (h < 8)
                            Console.Write(8 - h);
                        else
                            Console.Write(" ");
                    }
                    //number the columns
                    else if (h == 8)
                        Console.Write((char)('a' + w));
                    else if (board[w, h] != null)
                    {
                        if (board[w, h].color == Color.Black)
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                        //display piece
                        Console.Write(board[w, h].letter);

                    }
                    else Console.Write(' ');
                    Console.ForegroundColor = ConsoleColor.White;
                    //additional verical line on the very right
                    if (w == 7)
                        Console.Write("|");
                }
                Console.WriteLine();
                if(h == 8)
                {
                    //additional horizontal line on the very bottom
                    for (int w = 0; w < 19; w++)
                        Console.Write((w % 2 == 0) ? "+" : "-");
                    Console.WriteLine();
                }
            }
        }
        //Special moves: - to include in GetMoves method of Piece class
        //en  passant
        //castling 
        //double-step move
        //promotion
        //toggleable special moves?
        private Win Execute(Point[] move)
        {
            //save move and update board

            if (board[move[0].x, move[0].y].firstMove)
                board[move[0].x, move[0].y].firstMove = false;
            board[move[1].x, move[1].y] = board[move[0].x, move[0].y];
            board[move[0].x, move[0].y] = null;
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
