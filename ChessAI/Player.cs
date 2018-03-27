using System;
using System.Collections.Generic;
using System.Text;

namespace ChessAI
{
    abstract class Player
    {
        public abstract String Decide(Board board);
    }
}
