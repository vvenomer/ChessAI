using System;
using System.Collections.Generic;
using System.Text;

namespace ChessAI
{
    abstract class Piece
    {
        public int moves { get; set; }
        public Color color { get; set; }

        public abstract char letter { get;  }

        public Piece(Color color)
        {
            moves = 0;
            this.color = color;
        }
        public Piece()
        {
            moves = 0;
        }

        public abstract List<Point> GetMoves(Board board, Point myPos);

    }
}
