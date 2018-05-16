using System.Collections.Generic;

namespace ChessAI
{
	abstract class Piece
	{
		public int Moves { get; set; }
		public Color Color { get; set; }

        public abstract int ValueOfPiece { get;}
        public abstract int[,] ArrayPiecePosition { get; }
        public abstract int MaxValueAtPosition { get; }
        public abstract Figure ID { get; }

        public abstract char Letter { get; }

		public Piece(Color color)
		{
			Moves = 0;
			this.Color = color;
		}
		public Piece()
		{
			Moves = 0;
		}

		public List<Point> GetValidMoves(Board board, Point myPos)
		{
            List<Point> possibleMoves = GetMoves(board, myPos);
            bool promoted;
			//additional validation for all pieces
			for (int i = 0; i < possibleMoves.Count; i++)
			{
                promoted = false;
                //would this move cause my king to be checked
                Point[] mov = { myPos, possibleMoves[i] };
                board.Execute(mov, true); //but don't check for check

                promoted = board.LatestMoved.hasPromoted;
				bool check = board.PiecesCheckingKing(Color, true).Count != 0;
                Board.Move move = board.LatestMoved;
                board.UndoMove(1);
                if (!check && promoted)
                {
                    for(int j = 1; j < Board.promoteOptions.Length; j++)
                    {
                        board.Execute(mov, true, j); //but don't check for check
                        promoted = board.LatestMoved.hasPromoted;
                        check = board.PiecesCheckingKing(Color, true).Count != 0;

                        board.UndoMove(1);
                        if (check) break;
                    }
                }
				if (check)
				{
					possibleMoves.Remove(possibleMoves[i]);
					i--;
				}
			}
            return possibleMoves;
		}
		public abstract List<Point> GetMoves(Board board, Point myPos);

	}
}
