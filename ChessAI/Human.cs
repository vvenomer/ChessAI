using System;
using System.Collections.Generic;
using System.Text;

namespace ChessAI
{
    class Human : Player
    {
        public Human(Color color) : base(color) { }
        private byte[] GetPiece(string message)
        {
            byte []outPos = new byte[2];
            string piece;
            while (true)
            {
                Console.WriteLine(message);
                piece = Console.ReadLine();
                if (piece[0] < 'a' || piece[0] > 'h' || piece[1] < '0' || piece[1] > '9')
                {
                    Console.WriteLine("Złe polecenie");
                    continue;
                }
                break;
            }
            outPos[0] = (byte)(piece[0] - 'a');
            outPos[1] = (byte)(8 - (piece[1] - '0'));
            return outPos;

        }
        public override short Decide(Board board)
        {
            board.Print();
            Console.WriteLine( "Tura nr" + board.turns +" " + (color == Color.White ? "Białych" : "Czarnych"));
            short res=0;
            while (true)
            {
                byte[] pos = GetPiece("Wybierz bierkę");
                res = (short)(8 * pos[0] + pos[1]);

                byte onBoard = board.board[pos[0], pos[1]];
                if (onBoard == 0 || (onBoard & (byte)Color.White) != (byte)color)
                {
                    //not your piece
                    Console.WriteLine("To nie jest twoja bierka");
                }
                else break;
            }
            while (true)
            {
                byte[] pos = GetPiece("Ustaw wybraną bierkę");
                res += (short)((8 * pos[0] + pos[1])*64);
                byte onBoard = board.board[pos[0], pos[1]];
                if (onBoard!=0 && (onBoard & (byte)Color.White) == (byte)color)
                {
                    //your piece
                    Console.WriteLine("Nie możesz postawić bierki na swojej bierce");
                }
                else break;
            }
            return res;
        }
    }
}
