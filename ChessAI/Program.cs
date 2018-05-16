using System;
using System.Collections.Generic;
using System.Linq;
namespace ChessAI
{
    class Program
    {
        static void Main(string[] args)
        {
            NeuralNetwork A = new NeuralNetwork(Color.White, @"activationNN.dat", true).Learn(500);
            Player B = new RandomPlayer(Color.Black);
            int[] results = new int[3];
            for (ulong i = 0; ; i++)
            {
                Board board = new Board(A, B);
                do
                {
                    board.ExecuteTurn();
                } while (!board.MatchEnded());
                if (board.GameState == Win.White)
                    A.Fix();
                else
                    A.Clear();
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
                Console.Clear();
                Console.WriteLine("Games: " + (i + 1));
                Console.WriteLine("Black wins: " + results[0]);
                Console.WriteLine("White wins: " + results[1]);
                Console.WriteLine("Stalemates: " + results[2]);
                Console.WriteLine("myDecisions: " + A.myDecisions);
            }
        }
    }
}
