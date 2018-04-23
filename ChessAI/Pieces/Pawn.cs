using System;
using System.Collections.Generic;
using System.Text;

namespace ChessAI.Pieces
{
    class Pawn : Piece
    {
        const char Letter = 'P';

        public Pawn(Color color) : base(color) { }
        public Pawn() : base() { }
        public override char letter { get { return Letter; } }

        public override List<Point> GetMoves(Board board, Point myPos)
        {
            List<Point> list = new List<Point>();
            int way = color == Color.White ? 1 : -1;
            //move 2 file on the first move (double step move)
            if (moves==0 && board.board[myPos.x, myPos.y + way * 2] == null && board.board[myPos.x, myPos.y + way] == null)
                list.Add(new Point(myPos.x, myPos.y + way * 2));
            //move 1 file forward
            if( (way>0 && myPos.y<8) || (way<0 && myPos.y>0) )
             if ( board.board[myPos.x, myPos.y + way] == null)
                    list.Add(new Point(myPos.x, myPos.y + way));
            //hit enemy on diagonal left
            if (myPos.x > 0)
            {
                Piece diagLeft = board.board[myPos.x - 1, myPos.y + way];
                if (diagLeft != null && diagLeft.color != color)
                    list.Add(new Point(myPos.x - 1, myPos.y + way));
                //en passant:
                //on the left
                //there is enemy pawn
                //that moved just once
                //and is on either 4th or 5th rank (y=4||5)
                //and that was his latest move? - move history could come  in handy here

            }
            //hit enemy on diagonal right
            if (myPos.x < 8)
            {
                Piece diagRight = board.board[myPos.x + 1, myPos.y + way];
                if (diagRight != null && diagRight.color != color)
                    list.Add(new Point(myPos.x + 1, myPos.y + way));
                //en passant on the right
            }
            //en passant
            
            return list;
        }
    }
}
