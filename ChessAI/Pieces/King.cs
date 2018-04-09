using System;
using System.Collections.Generic;
using System.Text;

namespace ChessAI.Pieces
{
    class King : Piece
    {
        static char Letter = 'K';

        public King(Color color) : base(color) { }
        public King() : base() { }
        public override char letter { get { return Letter; } }

        public override Point[] GetMoves(Board board, Point myPos)
        {
            List<Point> list = new List<Point>();
            int xMin = myPos.x > 1 ? myPos.x - 1 : myPos.x;
            int xMax = myPos.x < 7 ? myPos.x + 1 : myPos.x;
            int yMin = myPos.y > 1 ? myPos.y - 1 : myPos.y;
            int yMax = myPos.y < 7 ? myPos.y + 1 : myPos.y;
            for (int x = xMin; x <= xMax; x++) 
            {
                for (int y = yMin; y <= yMax; y++)
                {
                    if(board.board[x,y]==null)
                    {
                        list.Add(new Point(x, y));
                    }
                }
            }
            //special moves
            //...
            return list.ToArray();
        }
    }
}
