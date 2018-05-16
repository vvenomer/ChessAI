using System;
using System.Collections.Generic;
using System.Text;

namespace ChessAI.Pieces
{
    class Knight : Piece
    {
        static int[,] arrayPiecePosition = new int[8, 8]{
           { -50,-40,-30,-30,-30,-30,-40,-50 },
           { -40,-20,  0,  0,  0,  0,-20,-40 },
           { -30,  0, 10, 15, 15, 10,  0,-30 },
           { -30,  5, 15, 20, 20, 15,  5,-30 },
           { -30,  0, 15, 20, 20, 15,  0,-30 },
           { -30,  5, 10, 15, 15, 10,  5,-30 },
           { -40,-20,  0,  5,  5,  0,-20,-40 },
           { -50,-40,-30,-30,-30,-30,-40,-50 },
        };

        const char letter = 'N';
        const int valueOfPiece = 320;
        const int maxValueAtPosition = 20;
        const Figure id = Figure.Knight;

        public override int MaxValueAtPosition { get => maxValueAtPosition; }
        public override int[,] ArrayPiecePosition { get => arrayPiecePosition; }
        public override char Letter { get => letter; }
        public override int ValueOfPiece { get => valueOfPiece; }
        public override Figure ID { get => id; }

        public Knight() { }
        public Knight(Color color) : base(color) { }

        public override List<Point> GetMoves(Board board, Point myPos)
        {
            List<Point> list = new List<Point>();
            int x = 2, y = 1;
            for (int i = 0; i < 8; i++)
            {
                //start magic
                x ^= y;
                y ^= x;
                x ^= y;
                y *= i != 4 ? -1 : 1;
                //end magic
                if (myPos.x + x < 8 && myPos.x + x >= 0
                        && myPos.y + y < 8 && myPos.y + y >= 0
                        && (board.BoardTab[myPos.x + x, myPos.y + y] == null || board.BoardTab[myPos.x + x, myPos.y + y].Color != Color))
                    list.Add(new Point(myPos.x + x, myPos.y + y));
            }
            return list;
        }
    }
}
