using System;
using System.Collections.Generic;
using System.Text;

namespace ChessAI
{
	class NeuralNetwork : Player
	{
		public NeuralNetwork(Color color) : base(color)
		{
		}

		public override Point[] Decide(Board board)
		{
			throw new NotImplementedException();
		}

        public override char PromotePawn(char[] options)
        {
            throw new NotImplementedException();
        }
    }
}
