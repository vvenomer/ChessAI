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
            List<Point> possibilites = board.GetAllPiecesPositions(isMin ? (color==Color.Black ? Color.White : Color.Black) : color);
            foreach (Point piecePos in possibilites)
            {

                foreach (Point move in board.BoardTab[piecePos.x, piecePos.y].GetValidMoves(board, piecePos))
                {
                    Point[] tempMove = new Point[2] { piecePos, move };
                    var pieces = board.GetAllPiecesPositions(Color.White).Concat(board.GetAllPiecesPositions(Color.Black));
                    board.Execute(tempMove);
                    Node result = DoAlphaBeta(!isMin, board, depth -1, alpha, beta);
                    board.UndoMove(1);
                    /*var pieces2 = board.GetAllPiecesPositions(Color.White).Concat(board.GetAllPiecesPositions(Color.Black));
                    foreach(var piece in pieces)
                    {
                        if (pieces2.Where(x => {
                            var find = board.BoardTab[x.x, x.y]; var toFind = board.BoardTab[piece.x, piece.y];
                            return x.x == piece.x && x.y == piece.y && find.letter == toFind.letter && find.color == find.color && toFind.moves == toFind.moves;
                            }).Count()==0)
                            Console.WriteLine("!");
                    }*/

                    if (board.GetAllPiecesPositions(Color.Black).Concat(board.GetAllPiecesPositions(Color.White)).Where(x => {
                        var piece = board.BoardTab[x.x, x.y];
                        return piece.letter == 'P' &&
                            ((piece.color == Color.White && x.y == 7) || (piece.color == Color.Black && x.y == 0));
                    }).Count() != 0)
                    {
                        board.Print(null);
                    }

                    if ((isMin && result.eval < MinMax.eval) || (!isMin && result.eval > MinMax.eval))
                    {
                        MinMax.eval = result.eval;
                        MinMax.move = tempMove;
                    }
                    if ((isMin && MinMax.eval == 1) || (!isMin && MinMax.eval == 0))
                        MinMax.move = tempMove;
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
