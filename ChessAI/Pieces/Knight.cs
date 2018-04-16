using System;
using System.Collections.Generic;
using System.Text;

namespace ChessAI.Pieces
{
    class Knight : Piece
    {
        const char Letter = 'N';
        public Knight() { }

        public Knight(Color color) : base(color) { }

        public override char letter { get { return Letter; } }

        public override Point[] GetMoves(Board board, Point myPos)
        {
            List<Point> list = new List<Point>();
            int x = 2, y =1;
            for(int i = 0; i < 8; i++)
            {
                //start magic
                x ^= y;
                y ^= x;
                x ^= y;
                y *= i!=4 ? -1 : 1;
                //end magic
                if (myPos.x + x < 8 && myPos.x + x >= 0 
                        && myPos.y + y < 8 && myPos.y + y >= 0
                        && (board.board[myPos.x + x, myPos.y+y]==null || board.board[myPos.x + x, myPos.y + y].color!=color))
                    list.Add(new Point(myPos.x + x, myPos.y + y));
            }
            return list.ToArray();
        }
    }
}
