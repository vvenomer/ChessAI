using System;
using System.Collections.Generic;
using System.Linq;
namespace ChessAI
{
	class Program
	{
		static void Main(string[] args)
		{


            //NeuralNetwork A = new NeuralNetwork(Color.White, @"activationNN.dat", true).Learn();
            Player A = new MinMax(Color.White, 3);
            Player B = new AlphaBeta(Color.Black, 3);
            int[] results = new int[3];
            for (int i = 0; i < 10; i++)
            {
                Board board = new Board(A, B);
                do
                {
                    board.ExecuteTurn();
                    //board.Print();
                } while (!board.MatchEnded());
                /*if (board.GameState == Win.White)
                    A.Fix();
                else
                    A.Clear();*/
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
                if ((i + 1) % 1 == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Games: " + (i + 1));
                    Console.WriteLine("Black wins: " + results[0]);
                    Console.WriteLine("White wins: " + results[1]);
                    Console.WriteLine("Stalemates: " + results[2]);
                    //Console.WriteLine("myDecisions: " + A.myDecisions);
                }
            }
            //Console.Clear();
            //board.PrintMatchResult();
            Console.Read();

            /*Player A = new AlphaBeta(Color.White);
            Player B = new RandomPlayer(Color.Black);
            Board board = new Board(A, B);

            do
            {
                board.ExecuteTurn();

                //Console.SetCursorPosition(0, 0);
                //Console.WriteLine(board.Turns);
                //Console.ReadLine();
            } while (!board.MatchEnded());
            board.PrintMatchResult();*/
            //Console.Read();
		}
	}
}
