using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Wolven_Menuapp
{
    internal class Menu 
    {
        public static void menukaart()
        {
            int screen = 1;
            int pgmax = 3;
            ConsoleKey input;
            while (true)
            {
                if (screen == 1)
                {
                    Screen1();
                }
                else if (screen == 2)
                {
                    Screen2();

                }
                else if (screen == 3)
                {
                    Screen3();
                }
                input = Console.ReadKey().Key;
                if (input == ConsoleKey.RightArrow & pgmax != screen)
                {
                    screen++;
                }
                else if (input == ConsoleKey.LeftArrow & pgmax != 1)
                {
                    screen -= 1;
                }
                else if (input == ConsoleKey.Escape)
                {
                    break;
                }

            }
            void Screen1()
            {
                Console.Clear();
                Console.WriteLine("MENUKAART\n\n");
                //Plaats hieronder de eerste 10 menuopties
                Console.WriteLine($"menuoptie 1, prijsinformatie, allergeneninformatie");
                Console.WriteLine($"Dit is pagina {screen}\n\nDruk op de pijltjestoetsen om van pagina te wisselen, \nDruk ok Escape om terug te gaan.");
            }
            void Screen2()
            {
                Console.Clear();
                Console.WriteLine("MENUKAART\n\n");
                //Plaats hieronder de tweede 10 menuopties
                Console.WriteLine($"menuoptie 2, prijsinformatie, allergeneninformatie");
                Console.WriteLine($"Dit is pagina {screen}\n\nDruk op de pijltjestoetsen om van pagina te wisselen, \nDruk ok Escape om terug te gaan.");
            }
            void Screen3()
            {
                Console.Clear();
                Console.WriteLine("MENUKAART\n\n\n");
                //Plaats hieronder de derde 10 menuopties
                Console.WriteLine($"menuoptie 3, prijsinformatie, allergeneninformatie");
                Console.WriteLine($"Dit is pagina {screen}\n\nDruk op de pijltjestoetsen om van pagina te wisselen, \nDruk ok Escape om terug te gaan.");
            }
        }
    }
}
