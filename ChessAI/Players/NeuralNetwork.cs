using Accord.Neuro;
using Accord.Neuro.Learning;
using Accord.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
namespace ChessAI
{
	class NeuralNetwork : Player
    {
        public int myDecisions;
        ActivationNetwork network;
        ResilientBackpropagationLearning teacher;
        double eps;
        string path;
        int depth;
        int times;
        Player Substitute;
        List<double[]> boardSnapshot;
        List<double[]> moveSnapshot;
    
        public NeuralNetwork(Color color, string path, bool keepLearning, int depth = 4, int times = 5000, double eps = 1e-6) : base(color)
		{
            if(File.Exists(path))
                network = (ActivationNetwork)Network.Load(path);
            else
                network = new ActivationNetwork(new SigmoidFunction(2), 64, 8, 1);
            teacher = new ResilientBackpropagationLearning(network);
            this.myDecisions = 0;
            this.eps = eps;
            this.path = path;
            this.depth = depth;
            this.times = times;
            this.Substitute = new AlphaBeta(color, depth);
            this.boardSnapshot = new List<double[]>();
            this.moveSnapshot = new List<double[]>();
        }

        Point[] DoubleToMove(double inp, double eps)
        {
            int moves = (int)((inp + eps) * 4095);
            return new Point[]
            {
                    new Point((moves & (0b111 << (3 * 0))) >> (3 * 0), (moves & (0b111 << (3 * 1))) >> (3 * 1) ),
                    new Point((moves & (0b111 << (3 * 2))) >> (3 * 2), (moves & (0b111 << (3 * 3))) >> (3 * 3) )
            };
        }

        double MoveToDouble(Point from, Point to)
        {
            int res = (from.x & 0b111) << (3 * 0);
            res += (from.y & 0b111) << (3 * 1);
            res += (to.x & 0b111) << (3 * 2);
            res += (to.y & 0b111) << (3 * 3);
            return (double)res / 4095;
        }

        public NeuralNetwork Learn(int games = 20)
        {
            Player B = new RandomPlayer(Board.OppositeColor(color));
            for (int i = 0; i < games;)
            {
                Clear();
                Board board = new Board(Substitute, B);
                do
                {
                    if (color == Color.White)
                        boardSnapshot.Add(board.BoardToDouble());

                    board.SinglePlayerTurn(Color.White);
                    if (color == Color.White)
                        moveSnapshot.Add(new double[] { MoveToDouble(board.LatestMoved.from, board.LatestMoved.to) });

                    if (board.MatchEnded())
                        continue;
                    
                    if (color == Color.Black)
                        boardSnapshot.Add(board.BoardToDouble());

                    board.SinglePlayerTurn(Color.Black);

                    if (color == Color.Black)
                        moveSnapshot.Add(new double[] { MoveToDouble(board.LatestMoved.from, board.LatestMoved.to) });

                } while (!board.MatchEnded());
                if(board.GameState== (color==Color.Black ? Win.Black : Win.White))
                {
                    i++;
                    double[][] boards = boardSnapshot.ToArray();
                    double[][] moves = moveSnapshot.ToArray();

                    for (int j = 0; j < times; j++)
                    {
                        double error = teacher.RunEpoch(boards, moves);
                        if (error < eps)
                            break;
                    }
                }

            }
            network.Save(path);
            return this;
        }
        
        public override Point[] Decide(Board board)
		{
            Point[] choice = DoubleToMove(network.Compute(board.BoardToDouble())[0], eps);
            Piece toMove = board.BoardTab[choice[0].x, choice[0].y];
            if (toMove == null || toMove.Color != color || !toMove.GetValidMoves(board, choice[0]).Contains(choice[1]))
            {
                choice = Substitute.Decide(board);
                boardSnapshot.Add(board.BoardToDouble());
                moveSnapshot.Add(new double[] { MoveToDouble(choice[0], choice[1]) });
            }
            else myDecisions++;
			return choice;
		}

        public void Fix()
        {
            double[][] boards = boardSnapshot.ToArray();
            double[][] moves = moveSnapshot.ToArray();

            for (int i = 0; i < times; i++)
            {
                double error = teacher.RunEpoch(boards, moves);
                if (error < eps)
                    break;
            }
            Clear();
            network.Save(path);
        }
        public void Clear()
        {
            boardSnapshot.Clear();
            moveSnapshot.Clear();
        }

        public override char PromotePawn(char[] options)
        {
            return 'Q';
        }
    }
}
