using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessAI
{
    class MinMax : Player
    {
        struct Node
        {
            public Point[] move;
            public double eval;
        }
        int depth;

        public MinMax(Color color, int searchDepth = 4) : base(color)
        {
            depth = searchDepth;
        }

        private Node DoMinMax(bool isMin, Board board, int depth)
        {
            Node MinMax = new Node()
            {
                eval = isMin ? 1 : 0
            };
            if (depth == 0)
            {
                return new Node() { move = null, eval = board.Evaluate(color) };
            }
            List<Point> playerPieces = board.GetAllPiecesPositions(isMin ? Board.OppositeColor(color) : color);
            foreach (Point piecePos in playerPieces)
            {

                foreach (Point move in board.BoardTab[piecePos.x, piecePos.y].GetValidMoves(board, piecePos))
                {
                    Point[] tempMove = new Point[2] { piecePos, move };

                    board.Execute(tempMove);

                    Node result = DoMinMax(!isMin, board, depth - 1);

                    board.UndoMove(1);

                    if ((isMin && result.eval <= MinMax.eval) || (!isMin && result.eval >= MinMax.eval))
                    {
                        MinMax.eval = result.eval;
                        MinMax.move = tempMove;
                    }
                }
            }
            return MinMax;
        }


        public override Point[] Decide(Board board)
        {
            Node minMax = DoMinMax(false, board, depth);
            return minMax.move;
        }

        public override char PromotePawn(char[] options)
        {
            return 'Q';
        }
    }
}
