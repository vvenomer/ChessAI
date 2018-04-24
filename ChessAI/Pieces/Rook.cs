using System;
using System.Collections.Generic;
using System.Text;

namespace ChessAI.Pieces
{
    class Rook : Piece
    {
        const char Letter = 'R';
        public Rook() : base() { }

        public Rook(Color color) : base(color) { }

        public override char letter { get { return Letter; } }

        private bool AddToList(List<Point> list, Board board, int x, int y)
        {

            //null -> add, continue
            //color=mycolor -> stop
            //color!=mycolor -> add, stop
            if (board.BoardTab[x, y] == null)
                list.Add(new Point(x, y));
            else if (board.BoardTab[x, y].color == color)
                return false;
            else
            {
                list.Add(new Point(x, y));
                return false;
            }
            return true;
        }

        public override List<Point> GetMoves(Board board, Point myPos)
        {
            List<Point> list = new List<Point>();
            if(myPos.y<7) //down
            {
                for (int i = myPos.y + 1; i <= 7; i++)
                {
                    if (!AddToList(list, board, myPos.x, i))
                        break;
                }
            }
            if (myPos.y > 0)//up
            {
                for (int i = myPos.y - 1; i >= 0; i--)
                {
                    if (!AddToList(list, board, myPos.x, i))
                        break;
                }
            }
            if (myPos.x < 7)//right
            {
                for (int i = myPos.x + 1; i <= 7; i++)
                {
                    if (!AddToList(list, board, i, myPos.y))
                        break;
                }
            }
            if (myPos.x > 0)//left
            {
                for (int i = myPos.x - 1; i >= 0; i--)
                {
                    if (!AddToList(list, board, i, myPos.y))
                        break;
                }
            }
            return list;
        }
    }
}
