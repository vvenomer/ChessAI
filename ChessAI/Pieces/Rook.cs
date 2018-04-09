using System;
using System.Collections.Generic;
using System.Text;

namespace ChessAI.Pieces
{
    class Rook : Piece
    {
        static char Letter = 'R';
        public Rook() : base() { }

        public Rook(Color color) : base(color) { }

        public override char letter { get { return Letter; } }

        private bool AddToList(List<Point> list, Board board, int x, int y)
        {

            //null -> add, continue
            //color=mycolor -> stop
            //color!=mycolor -> add, stop
            if (board.board[x, y] == null)
                list.Add(new Point(x, y));
            else if (board.board[x, y].color == color)
                return false;
            else
            {
                list.Add(new Point(x, y));
                return false;
            }
            return true;
        }

        public override Point[] GetMoves(Board board, Point myPos)
        {
            List<Point> list = new List<Point>();
            if(myPos.y<7)
            {
                for (int i = myPos.y + 1; i <= 7; i++)
                {
                    if (!AddToList(list, board, myPos.x, i))
                        break;
                }
            }
            if (myPos.y > 0)
            {
                for (int i = myPos.y - 1; i >= 0; i--)
                {
                    if (!AddToList(list, board, myPos.x, i))
                        break;
                }
            }
            if (myPos.x < 7)
            {
                for (int i = myPos.x + 1; i <= 7; i++)
                {
                    if (!AddToList(list, board, i, myPos.y))
                        break;
                }
            }
            if (myPos.x > 0)
            {
                for (int i = myPos.x - 1; i >= 0; i--)
                {
                    if (!AddToList(list, board, i, myPos.y))
                        break;
                }
            }
            return list.ToArray();
        }
    }
}
