using System;

namespace ChessAI
{
    class Program
    {
        static void Main(string[] args)
        {
            Player A = new Human();
            Player B = new Human();
            Board board = new Board(A,B);
            Color playerWon; //stalemate?
            do
            {
                playerWon = board.Turn();
            } while (playerWon == Color.None);
            Console.ReadKey();
        }
    }
}
