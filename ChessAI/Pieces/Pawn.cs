using System;
using System.Collections.Generic;
using System.Text;

namespace ChessAI.Pieces
{
    class Pawn : Piece
    {
        const char Letter = 'P';

        const int ValueOfPiece = 100;
        const int MaxValueOfPosition = 30;
        public override int maxValueAtPosition { get { return MaxValueOfPosition; } }

        static int[,] arrayPiecePosition = new int[8, 8]{
           { 0,  0,  0,  0,  0,  0,  0,  0 },
           { 50, 50, 50, 50, 50, 50, 50, 50},
           { 10, 10, 20, 30, 30, 20, 10, 10 },
           { 5,  5, 10, 25, 25, 10,  5,  5 },
           { 0,  0,  0, 20, 20,  0,  0,  0 },
           { 5, -5,-10,  0,  0,-10, -5,  5 },
           { 5, 10, 10,-20,-20, 10, 10,  5 },
           { 0,  0,  0,  0,  0,  0,  0,  0 },
        };

        public override int[,] ArrayPiecePosition { get { return arrayPiecePosition; } }

        public Pawn(Color color) : base(color) { }
        public Pawn() : base() { }
        public override char letter { get { return Letter; } }
        public override int valueOfPiece { get { return ValueOfPiece; } }
        public override List<Point> GetMoves(Board board, Point myPos)
        {
            List<Point> list = new List<Point>();
            int way = color == Color.White ? 1 : -1;
            //move 2 file on the first move (double step move)
            if (moves == 0 && board.BoardTab[myPos.x, myPos.y + way * 2] == null && board.BoardTab[myPos.x, myPos.y + way] == null) //err white?
                list.Add(new Point(myPos.x, myPos.y + way * 2));
            //move 1 file forward
            if ((way > 0 && myPos.y < 7) || (way < 0 && myPos.y > 0))
                if (board.BoardTab[myPos.x, myPos.y + way] == null)
                    list.Add(new Point(myPos.x, myPos.y + way));
            //hit enemy on diagonal left
            if (myPos.x > 0)
            {
                Piece diagLeft = board.BoardTab[myPos.x - 1, myPos.y + way]; //err black?
                if (diagLeft != null && diagLeft.color != color)
                    list.Add(new Point(myPos.x - 1, myPos.y + way));
            }
            //hit enemy on diagonal right
            if (myPos.x < 7)
            {
                Piece diagRight = board.BoardTab[myPos.x + 1, myPos.y + way];
                if (diagRight != null && diagRight.color != color)
                    list.Add(new Point(myPos.x + 1, myPos.y + way));
            }
            //en passant
            if (board.LatestMoved == null) //nothing moved yet
                return list;
            Piece latestMoved = board.BoardTab[board.LatestMoved.to.x, board.LatestMoved.to.y];

            if (latestMoved.letter == 'P' && //pawn
                latestMoved.color != color && //enemy
                latestMoved.moves == 1 && //moved once
                (board.LatestMoved.to.y == 3 || board.LatestMoved.to.y == 4)) //it was double step move
            {
                if (myPos.x > 0 && myPos.x - 1 == board.LatestMoved.to.x && myPos.y == board.LatestMoved.to.y) //this pawn is next to it
                {
                    list.Add(new Point(myPos.x - 1, myPos.y + way));
                }
                if (myPos.x < 7 && myPos.x + 1 == board.LatestMoved.to.x && myPos.y == board.LatestMoved.to.y) //this pawn is next to it
                {
                    list.Add(new Point(myPos.x + 1, myPos.y + way));
                }
            }
            return list;
        }
        public Piece Promote(char choice)
        {
            Piece newPiece = null;
            switch (choice)
            {
                case 'Q':
                    newPiece = new Queen(color);
                    break;
                case 'N':
                    newPiece = new Knight(color);
                    break;
                case 'R':
                    newPiece = new Rook(color);
                    break;
                case 'B':
                    newPiece = new Bishop(color);
                    break;
            }
            newPiece.moves = moves;
            return newPiece;
        }
    }
}
