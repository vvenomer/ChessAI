using System;
using System.Collections.Generic;
using System.Text;

namespace ChessAI.Pieces
{
    class Pawn : Piece
    {
        static Figure Figure = Figure.Pawn;
        static char Letter = 'P';

        public Pawn(Color color) : base(color) { }
        public Pawn() { }
        public override char letter { get { return Letter; } }

        public override Figure figure { get { return Figure; } }

        public override Point[] GetMoves(Board board, Point myPos)
        {
            List<Point> list = new List<Point>();
            int way = color == Color.White ? 1 : -1;
            //move 2 squares on the first move
            if (firstMove && board.board[myPos.x, myPos.y + way * 2] == null)
                list.Add(new Point(myPos.x, myPos.y + way * 2));
            //move 1 square forward
            if( (way>0 && myPos.y<8) || (way<0 && myPos.y>0) )
             if ( board.board[myPos.x, myPos.y + way] == null)
                    list.Add(new Point(myPos.x, myPos.y + way));
            //hit enemy on diagonal left
            if (myPos.x > 0)
            {
                Piece diagLeft = board.board[myPos.x - 1, myPos.y + way];
                if (diagLeft != null && diagLeft.color != color)
                    list.Add(new Point(myPos.x - 1, myPos.y + way));
            }
            //hit enemy on diagonal right
            if (myPos.x < 8)
            {
                Piece diagRight = board.board[myPos.x + 1, myPos.y + way];
                if (diagRight != null && diagRight.color != color)
                    list.Add(new Point(myPos.x + 1, myPos.y + way));
            }
            //special moves
            //...
            return list.ToArray();
        }
    }
}
