using System;

namespace ChessAI
{
    class Program
    {
        static void Main(string[] args)
        {
            Player A = new Human(Color.White);
            Player B = new Human(Color.Black);
            Board board = new Board(A,B);
            Win playerWon;
            do
            {
                playerWon = board.Turn();
            } while (playerWon == Win.None);
            Console.ReadKey();
        }
    }
}
