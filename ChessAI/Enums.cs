namespace ChessAI
{
	enum Figure
	{
		King = 0,
		Queen = 1,
		Rook = 2,
		Bishop = 3,
		Knight = 4,
		Pawn = 5
	};
	enum Color
	{
		Black,
		White
	};
	enum Win
	{
		Black,
		BlackCheck,
		White,
		WhiteCheck,
		None,
		Stalemate
	}
}
