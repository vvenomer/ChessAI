using System;
using System.Collections.Generic;
using System.Text;

namespace ChessAI
{
    class AlphaBeta : Player
    {
        struct Node
        {
            public Point[] move;
            public double eval;
        }


        private Node DoAlphaBeta(bool isMin, Board board, int depth)
        {
            Node MinMax = new Node
            {
                eval = isMin ? 1 : 0
            };
            if (depth == 0)
            {
                return new Node() { move = null, eval = board.Evaluate(color) };
            }
            List<Point> possibilites = board.GetAllPiecesPositions(color);
            foreach (Point piecePos in possibilites)
            {

                foreach (Point move in board.BoardTab[piecePos.x, piecePos.y].GetValidMoves(board, piecePos))
                {
                    Point[] tempMove = new Point[2] { piecePos, move };
                    board.Execute(tempMove);
                    Node result = DoAlphaBeta(!isMin, board, depth -1);
                    if ((isMin && result.eval < MinMax.eval) || (!isMin && result.eval > MinMax.eval))
                    {
                        MinMax.eval = result.eval;
                        MinMax.move = tempMove;
                       
                    }
                    board.UndoMove(1);
                }
            }
            return MinMax;
        }
        public AlphaBeta(Color color) : base(color)
        {
        }

        public override Point[] Decide(Board board)
        {

            Node alfaBeta = DoAlphaBeta(false, board, 3);
            return alfaBeta.move;
        }

        public override char PromotePawn(char[] options)
        {
            return 'Q';
        }
    }
}
