using System;
using System.Collections.Generic;
using System.Text;

namespace ChessAI.Pieces
{
	class Pawn : Piece
	{
		const char Letter = 'P';

		public Pawn(Color color) : base(color) { }
		public Pawn() : base() { }
		public override char letter { get { return Letter; } }

		public override List<Point> GetMoves(Board board, Point myPos)
		{
			List<Point> list = new List<Point>();
			int way = color == Color.White ? 1 : -1;
			//move 2 file on the first move (double step move)
			if (moves == 0 && board.BoardTab[myPos.x, myPos.y + way * 2] == null && board.BoardTab[myPos.x, myPos.y + way] == null)
				list.Add(new Point(myPos.x, myPos.y + way * 2));
			//move 1 file forward
			if ((way > 0 && myPos.y < 8) || (way < 0 && myPos.y > 0))
				if (board.BoardTab[myPos.x, myPos.y + way] == null)
					list.Add(new Point(myPos.x, myPos.y + way));
			//hit enemy on diagonal left
			if (myPos.x > 0)
			{
				Piece diagLeft = board.BoardTab[myPos.x - 1, myPos.y + way];
				if (diagLeft != null && diagLeft.color != color)
					list.Add(new Point(myPos.x - 1, myPos.y + way));
			}
			//hit enemy on diagonal right
			if (myPos.x < 7)
			{
				Piece diagRight = board.BoardTab[myPos.x + 1, myPos.y + way];
				if (diagRight != null && diagRight.color != color)
					list.Add(new Point(myPos.x + 1, myPos.y + way));
			}
			//en passant
			if (board.LatestMoved == null) //nothing moved yet
				return list;
			Piece latestMoved = board.BoardTab[board.LatestMoved.x, board.LatestMoved.y];
			if (latestMoved.letter == 'P' && //pawn
				latestMoved.color != color && //enemy
				latestMoved.moves == 1 && //moved once
				board.LatestMoved.y == 4 || board.LatestMoved.y == 5) //it was double step move
			{
				if (myPos.x > 0 && myPos.x - 1 == board.LatestMoved.x && myPos.y == board.LatestMoved.y) //this pawn is next to it
					list.Add(new Point(myPos.x - 1, myPos.y + way));
				if (myPos.x < 7 && myPos.x + 1 == board.LatestMoved.x && myPos.y == board.LatestMoved.y) //this pawn is next to it
					list.Add(new Point(myPos.x + 1, myPos.y + way));
			}
			return list;
		}
	}
}
