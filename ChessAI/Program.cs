using System;
using System.Collections.Generic;
using System.Linq;
namespace ChessAI
{
	class Program
	{
		static void Main(string[] args)
		{
			Player A = new RandomPlayer(Color.White);
			Player B = new RandomPlayer(Color.Black);
            /*int[] results = new int[3];
            for (int i = 0; i < 1000000; i++)
            {*/
                Board board = new Board(A, B);

                do
                {
                    board.ExecuteTurn();
                    //board.Print(null);
                    //Console.WriteLine("Turns: " + board.Turns);
                    //Console.ReadLine();
                } while (!board.MatchEnded());
                /*switch(board.GameState)
                {
                    case Win.Black:
                        results[0]++;
                        break;
                    case Win.White:
                        results[1]++;
                        break;
                    case Win.Stalemate:
                        results[2]++;
                        break;
                }
            }*/
			//Console.Clear();
			board.PrintMatchResult();
            //for(int i = 0; i < 3; i++)
            //    Console.WriteLine(results[i] + "\t");
		}
	}
}
