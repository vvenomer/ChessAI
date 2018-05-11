using System;
using System.Collections.Generic;
using System.Text;

namespace ChessAI
{
    class RandomPlayer : Player
    {
        Random random = new Random();

        public RandomPlayer(Color color) : base(color) { }

        public override Point[] Decide(Board board)
        {
            Point[] playerChoice = new Point[2];
            List<Point> possibilites = board.GetAllPiecesPositions(color);
            int choice;
            List<Point> aviableMoves;
            do
            {
                choice = random.Next() % possibilites.Count;
                aviableMoves = board.BoardTab[possibilites[choice].x, possibilites[choice].y].GetValidMoves(board, possibilites[choice]);
            } while (aviableMoves.Count == 0);

            playerChoice[0] = possibilites[choice];

            choice = random.Next() % aviableMoves.Count;
            playerChoice[1] = aviableMoves[choice];

            return playerChoice;
        }

        public override char PromotePawn(char[] options)
        {
            return options[random.Next() % options.Length];
        }
    }
}
