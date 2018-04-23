﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ChessAI.Pieces
{
    class King : Piece
    {
        const char Letter = 'K';

        public King(Color color) : base(color) { }
        public King() : base() { }
        public override char letter { get { return Letter; } }

        public override List<Point> GetMoves(Board board, Point myPos)
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
            if(moves==0) //king hasn't moved
            {
                if(board.board[0, myPos.y] != null && board.board[0, myPos.y].moves == 0) //there is rook on the left, that hasn't moved
                {
                    bool isEmpty = true;
                    for (int i = 1; i < myPos.x; i++) //all spots on the way are empty
                    {
                        if (board.board[i, myPos.y] != null)
                        {
                            isEmpty = false;
                            break;
                        }
                    }
                    if(isEmpty)
                        list.Add(new Point(myPos.x-2, myPos.y));
                }
                else if(board.board[7, myPos.y] != null && board.board[7, myPos.y].moves == 0)
                {
                    bool isEmpty = true;
                    for (int i = myPos.x+1; i < 7; i++) //all spots on the way are empty
                    {
                        if (board.board[i, myPos.y] != null)
                        {
                            isEmpty = false;
                            break;
                        }
                    }
                    if (isEmpty)
                        list.Add(new Point(myPos.x + 2, myPos.y));
                }
            }
            //special moves
            //... castling
            return list;
        }
    }
}
