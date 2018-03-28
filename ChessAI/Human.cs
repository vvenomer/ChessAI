using System;
using System.Collections.Generic;
using System.Text;

namespace ChessAI
{
    class Human : Player
    {
        public Human(Color color) : base(color) { }
        public override short Decide(Board board)
        {
            board.Print();
            Console.WriteLine( "Tura " + (color == Color.White ? "Białych" : "Czarnych"));
            short res=0;
            while (true)
            {
                Console.WriteLine("Wybierz bierkę");
                string piece = Console.ReadLine();
                if (piece[0] < 'a' || piece[0] > 'h' || piece[1] < '0' || piece[1] > '9')
                {
                    Console.WriteLine("Złe polecenie");
                    continue;
                }
                byte x = (byte)(piece[0] - 'a');
                byte y = (byte)(8 - (piece[1] - '0'));
                res = (short)(8 * x + y);
                byte onBoard = board.board[x, y];
                if (onBoard == 0 || (onBoard & (byte)Color.White) != (byte)color)
                {
                    //not your piece
                    Console.WriteLine("To nie jest twoja bierka");
                }
                else break;
            }
            while (true)
            {
                Console.WriteLine("Ustaw wybraną bierkę");
                string piece = Console.ReadLine();
                if (piece[0] < 'a' || piece[0] > 'h' || piece[1] < '0' || piece[1] > '9')
                {
                    Console.WriteLine("Złe polecenie");
                    continue;
                }
                byte x = (byte)(piece[0] - 'a');
                byte y = (byte)(8 - (piece[1] - '0'));
                res += (short)((8 * x + y)*64);
                byte onBoard = board.board[x, y];
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
