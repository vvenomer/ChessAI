using System;
using System.Collections.Generic;
using System.Text;

namespace ChessAI
{
    class Human : Player
    {
        public override String Decide(Board board)
        {
            board.Print();
            Console.WriteLine("Podaj swój ruch");
            return Console.ReadLine();
        }
    }
}
