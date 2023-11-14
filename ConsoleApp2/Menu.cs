using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Menu
    {



        public static int Show(int minStrel, int maxStrel)
        {

            int pos = 3;
            ConsoleKeyInfo key;
            do
            {

        Console.SetCursorPosition(0, pos);
                Console.WriteLine("->");

                key = Console.ReadKey();

                Console.SetCursorPosition(0, pos);
                Console.WriteLine("  ");
                if (key.Key == ConsoleKey.UpArrow && pos != minStrel)
                    pos--;
                else if (key.Key == ConsoleKey.DownArrow && pos != maxStrel)
                    pos++;


            } while (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Escape);

            if (key.Key == ConsoleKey.Enter)
                return pos;
            else
                return -1;
        }

    }


    

    }

