using System;
using System.Collections.Generic;
using System.Text;

namespace ChessAI
{
    abstract class Player
    {
        public Color color;

        protected Player(Color color)
        {
            this.color = color;
        }

        public abstract short Decide(Board board);
    }
}
