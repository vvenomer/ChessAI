using System;
using System.Collections.Generic;
using System.Text;

namespace ChessAI
{
    abstract class Piece
    {
        static Figure Figure;
        static char Letter;
        public bool firstMove { get; set; }
        public Color color { get; set; }

        public abstract char letter { get;  }
        public abstract Figure figure { get; }

        public Piece(Color color)
        {
            firstMove = true;
            this.color = color;
        }
        public Piece()
        {
            firstMove = true;
        }

        public abstract Point[] GetMoves(Board board, Point myPos);

    }
}
