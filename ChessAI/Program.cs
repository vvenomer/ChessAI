using System;

namespace ChessAI
{
	class Program
	{
		static void Main(string[] args)
		{
			Player A = new Human(Color.White);
			Player B = new Human(Color.Black);
			Board board = new Board(A, B);
			Win playerWon;
			do
			{
				board.Turn();
				playerWon = board.GameState;
				//or
				//playerWon = board.Turn();
			} while (playerWon != Win.Black && playerWon != Win.White && playerWon != Win.Stalemate);

			Console.Clear();
			board.PrintMatchResult();
		}
	}
}
