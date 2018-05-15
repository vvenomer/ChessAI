using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ChessAI
{
    class AlphaBeta : Player
    {
        struct Node
        {
            public Point[] move;
            public double eval;
        }
        int depth;

        public AlphaBeta(Color color, int searchDepth = 4) : base(color)
        {
            depth = searchDepth;
        }

        private Node DoAlphaBeta(bool isMin, Board board, int depth, double alpha = 0, double beta = 1)
        {
            Node MinMax = new Node()
            {
                eval = isMin ? 1 : 0
            };
            if (depth == 0)
            {
                return new Node() { move = null, eval = board.Evaluate(color) };
            }
            List<Point> playerPieces = board.GetAllPiecesPositions(isMin ? (color == Color.Black ? Color.White : Color.Black) : color);
            foreach (Point piecePos in playerPieces)
            {

                foreach (Point move in board.BoardTab[piecePos.x, piecePos.y].GetValidMoves(board, piecePos))
                {
                    Point[] tempMove = new Point[2] { piecePos, move };
                    if (depth == 6)
                    {
                        //board.Print();
                    }

                    board.Execute(tempMove);

                    if (depth == 6)
                    {
                        //board.Print();
                    }

                    Node result = DoAlphaBeta(!isMin, board, depth - 1, alpha, beta);

                    if (depth == 6)
                    {
                       // board.Print();
                    }

                    board.UndoMove(1);

                    if (depth == 6)
                    {
                       // board.Print();
                    }

                    if (board.GetAllPiecesPositions(Color.Black).Concat(board.GetAllPiecesPositions(Color.White)).Where(x =>
                    {
                        var piece = board.BoardTab[x.x, x.y];
                        return piece.letter == 'P' &&
                            ((piece.color == Color.White && x.y == 7) || (piece.color == Color.Black && x.y == 0));
                    }).Count() != 0)
                    {
                        board.Print();
                    }

                    if ((isMin && result.eval <= MinMax.eval) || (!isMin && result.eval >= MinMax.eval))
                    {
                        MinMax.eval = result.eval;
                        MinMax.move = tempMove;
                    }
                    if (isMin)
                        beta = Math.Min(result.eval, beta);
                    else
                        alpha = Math.Max(result.eval, alpha);
                    if (beta <= alpha)
                        return MinMax;
                }
            }
            return MinMax;
        }


        public override Point[] Decide(Board board)
        {
            Node alfaBeta = DoAlphaBeta(false, board, depth);
            return alfaBeta.move;
        }

        public override char PromotePawn(char[] options)
        {
            return 'Q'; // temp
        }
    }
}
