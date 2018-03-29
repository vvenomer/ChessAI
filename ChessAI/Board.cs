using System;

namespace ChessAI
{
    class Board
    {
        
        public byte[,] board { get; private set; }
        Player whitePlayer, blackPlayer;
        public int turns { get; private set; }
        public Board(Player a, Player b)
        {
            Console.ForegroundColor = ConsoleColor.White;
            turns = 0;
            whitePlayer = a.color == Color.White ? a : b;
            blackPlayer = a.color != Color.White ? a : b;
            board = new byte[8,8];
            board[0, 0] = board[7, 0] = board[0, 7] = board[7, 7] = (byte)Piece.Rook;
            board[1, 0] = board[6, 0] = board[1, 7] = board[6, 7] = (byte)Piece.Knight;
            board[2, 0] = board[5, 0] = board[2, 7] = board[5, 7] = (byte)Piece.Bishop;
            board[3, 0] = board[4, 7] = (byte)Piece.Queen;
            board[4, 0] = board[3, 7] = (byte)Piece.King;
            for(int i = 0; i < 8; i++)
            {
                board[i, 1] = (byte)Piece.Pawn;
                board[i, 6] = (byte)Piece.Pawn;
            }
            for(int w = 0; w < 8; w++)
            {
                for(int h = 0; h < 2; h++)
                {
                    board[w, h] += (byte)Color.White;
                    board[w, 7-h] += (byte)Color.Black;
                }
            }
        }
        public void Print()
        {
			Console.Clear();
            for(int h = 0; h < 9; h++)
            {
                for (int w = 0; w < 19; w++)
                    Console.Write( (w%2==0) ? "+" : "-");
                Console.WriteLine();
                for(int w = -1; w < 8; w++)
                {
                    Console.Write("|");
                    if (w == -1)
                    {
                        if (h < 8)
                            Console.Write(8 - h);
                        else
                            Console.Write(" ");
                    }
                    else if (h == 8)
                            Console.Write((char)('a' + w));
                    else
                    {
                        bool white = (board[w, h] & (byte)Color.White) > 0;
                        Piece piece = (Piece)(board[w, h] - (white ? (byte)Color.White : 0));
                        if (!white)
                            Console.ForegroundColor = ConsoleColor.Gray;
                        switch (piece)
                        {
                            case Piece.Bishop:
                                Console.Write("B");
                                break;
                            case Piece.King:
                                Console.Write("K");
                                break;
                            case Piece.Knight:
                                Console.Write("N");
                                break;
                            case Piece.Pawn:
                                Console.Write("P");
                                break;
                            case Piece.Queen:
                                Console.Write("Q");
                                break;
                            case Piece.Rook:
                                Console.Write("R");
                                break;
                            default:
                                Console.Write(" ");
                                break;
                        }

                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    if (w == 7)
                        Console.Write("|");
                }
                Console.WriteLine();
                if(h == 8)
                {

                    for (int w = 0; w < 19; w++)
                        Console.Write((w % 2 == 0) ? "+" : "-");
                    Console.WriteLine();
                }
            }
        }

        public bool IsValidMove(short move)
        {
            //check if given move is valid

            //pawns move forward, but capture diagonally

            //Special moves:
            //en  passant
            //castling
            //double-step move
            //promotion
            return true; // temporary
        }
        private Win Execute(short move)
        {
            //save move and update board
            byte y1 = (byte)(move & 0b111);
            byte x1 = (byte)((move/8) & 0b111);
            byte y2 = (byte)((move / 8 / 8) & 0b111);
            byte x2 = (byte)((move / 8 / 8 / 8) & 0b111);
            board[x2, y2] = board[x1, y1];
            board[x1, y1] = 0;
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
            short res;
            do
            {
                res = whitePlayer.Decide(this);
            } while (!IsValidMove(res));
            Win win = Execute(res);
            if (win!=Win.None)
                return win;
            do
            {
                res = blackPlayer.Decide(this);
            } while (!IsValidMove(res));
            win = Execute(res);
            return win;
        }
    }
}
