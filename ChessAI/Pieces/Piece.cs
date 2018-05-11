using System.Collections.Generic;

namespace ChessAI
{
	abstract class Piece
	{
		public int moves { get; set; }
		public Color color { get; set; }

        public abstract int valueOfPiece { get;}
        public abstract int[,] ArrayPiecePosition { get; }
        public abstract int maxValueAtPosition { get; }

        public abstract char letter { get; }

		public Piece(Color color)
		{
			moves = 0;
			this.color = color;
		}
		public Piece()
		{
			moves = 0;
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
                promoted = board.History.Peek().hasPromoted;
				bool check = board.PiecesCheckingKing(color, true).Count != 0;
				board.UndoMove(1);
                if(!check && promoted)
                {
                    for(int j = 1; j < Board.promoteOptions.Length; j++)
                    {
                        board.Execute(mov, true, j); //but don't check for check
                        promoted = board.History.Peek().hasPromoted;
                        check = board.PiecesCheckingKing(color, true).Count != 0;
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
