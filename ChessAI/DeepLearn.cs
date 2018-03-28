using System;
using System.Collections.Generic;
using System.Text;

namespace ChessAI
{
    class DeepLearn : Player
    {
        public DeepLearn(Color color) : base(color)
        {
        }

        public override short Decide(Board board)
        {
            throw new NotImplementedException();
        }
    }
}
