using System;

namespace ChessAI
{
	class Program
	{
		static void Main(string[] args)
		{
			Player A = new RandomPlayer(Color.White);
			Player B = new RandomPlayer(Color.Black);
			Board board = new Board(A, B);

			do
			{
				board.ExecuteTurn();
                board.Print(null);
                Console.WriteLine("Turns: " + board.Turns);
                Console.ReadLine();
			} while (!board.MatchEnded());

			Console.Clear();
			board.PrintMatchResult();
		}
	}
}
