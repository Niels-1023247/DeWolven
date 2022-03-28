using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Wolven_Menuapp
{
    public static class Desserts
    {
        public static void Dessertsmenukaart()
        {
            int screen = 1; // bewaart op welke van de 5 schermen de gebruiker zit
            int pgmax = 5; // variable die bewaart hoeveel schermen er in totaal zijn
            ConsoleKey input;
            while (true)
            {
                if (screen == 1) // activeert de juiste method voor het juiste scherm mbv de screen-variable
                {
                    Screen1();
                }

                input = Console.ReadKey().Key; // input staat gelijk aan de toets die de gebruiker invoert
                if (input == ConsoleKey.RightArrow & pgmax != screen) // verhoog screenvariable
                {
                    screen++;
                }
                else if (input == ConsoleKey.LeftArrow & pgmax != 1 & screen > 1) // verlaag screenvariable
                {
                    screen -= 1;
                }
                else if (input == ConsoleKey.Escape) // terug naar hoofdmenu
                {
                    Menu.menukaart();
                    break;
                }
                else if (input == ConsoleKey.RightArrow & pgmax == screen) // als je na het laatste scherm naar rechts gaat dan gaat hij terug naar het eerste scherm
                {
                    screen = 1;
                }
            }
            void Screen1() // methode voor overschakelen naar de verschillende menu soorten
            {
                Console.Clear();
                Console.WriteLine("MENUKAART - DESSERTS\n\n");
                //Plaats hieronder de eerste 10 menuopties
                Console.WriteLine($"Dame Blanche, €9,-(ei/noten/melk)");
                Console.WriteLine($"Chocolade moulleux, €7,-(ei/noten/melk)");
                Console.WriteLine($"Gegrilde ananas, €6.50,(gluten/pinda's/noten/melk)");
                Console.WriteLine($"Scropinno Parfait, €9,-(gluten/ei/melk)");
                Console.WriteLine($"Tiramisu classico, €9.50,(ei/melk)");
                Console.WriteLine($"");
                Console.WriteLine($"");
                Console.WriteLine($"");
                Console.WriteLine($"");
                Console.WriteLine($"");
                Console.WriteLine($"Dit is pagina {screen}\n\nDruk op de pijltjestoetsen om van pagina te wisselen, \nDruk op Escape om terug te gaan.");
            }


        }
    }
}
