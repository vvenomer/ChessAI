using System;
using System.Collections.Generic;
using System.Linq;
namespace ChessAI
{
	class Program
	{
		static void Main(string[] args)
		{
            /*	Player A = new RandomPlayer(Color.White);
                Player B = new RandomPlayer(Color.Black);
                int[] results = new int[3];
                for (int i = 0; i < 1000; i++)
                {
                    Board board = new Board(A, B);

                    do
                    {
                        board.ExecuteTurn();
                        //Console.ReadLine();
                    } while (!board.MatchEnded());
                    if ((i + 1) % 10 == 0)
                    {
                        Console.SetCursorPosition(0,0);
                        Console.WriteLine("Games: " + (i + 1));
                    }
                    switch (board.GameState)
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
                }
                //Console.Clear();
                //board.PrintMatchResult();
                Console.WriteLine("Black wins: " + results[0]);
                Console.WriteLine("White wins: " + results[1]);
                Console.WriteLine("Stalemates: " + results[2]);
                */

            Player A = new RandomPlayer(Color.White);
            Player B = new AlphaBeta(Color.Black);
            Board board = new Board(A, B);

            do
            {
                board.ExecuteTurn();
                
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(board.Turns);
                //Console.ReadLine();
            } while (!board.MatchEnded());
            board.PrintMatchResult();
            //Console.Read();
		}
	}
}
