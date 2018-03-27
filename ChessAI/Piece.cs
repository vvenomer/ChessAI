using System;
using System.Collections.Generic;
using System.Text;

namespace ChessAI
{
    enum Piece
    {
        King = 1,
        Queen = 2,
        Rook = 3,
        Bishop = 4,
        Knight = 5,
        Pawn = 6
    };
    enum Color
    {
        Black = 0,
        White = 8
    };
    enum Win
    {
        Black,
        White,
        None,
        Stalemate
    }
}
