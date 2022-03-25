using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Wolven_Menuapp
{
    internal class gerechten
    {
        public void gerechtenmenukaart()
        {
            int screen = 1; // bewaart op welke van de 5 schermen de gebruiker zit
            int pgmax = 5; // variable die bewaart hoeveel schermen er in totaal zijn
            char euro = '€'; // euroteken voor c# gezeur
            ConsoleKey input;
            Menu menu = new Menu();
            while (true)
            {
                if (screen == 1) // activeert de juiste method voor het juiste scherm mbv de screen-variable
                {
                    Screen1();
                }
                else if (screen == 2) // etc..
                {
                    Screen2();
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
                Console.WriteLine("MENUKAART - GERECHTEN\n\n");
                //Plaats hieronder de eerste 10 menuopties
                Console.WriteLine($"Erwtensoep, €8,-(selderij)");
                Console.WriteLine($"Tomatensoep, €7.60,(soja/selderij)");
                Console.WriteLine($"Seldersoep, €8.50,(soja/selderij)");
                Console.WriteLine($"Spaghetti Bolognese, €16.99,(gluten/ei/selderij)");
                Console.WriteLine($"Pizza margherita, €13,-(gluten/melk)");
                Console.WriteLine($"Broodje jonge kaas, €7.30 (gluten/melk)");
                Console.WriteLine($"Broodje eiersalade, €7.40 (gluten/ei)");
                Console.WriteLine($"Groenten quiche broccoli-spek, €13,-(gluten/ei/melk)");
                Console.WriteLine($"Groenten quiche kastanje-vegi, €12.50,(gluten/ei/melk/selderij)");
                Console.WriteLine($"Spaghetti Carbonara, €17.30, (gluten/ei/melk)");
                Console.WriteLine($"Dit is pagina {screen}\n\nDruk op de pijltjestoetsen om van pagina te wisselen, \nDruk op Escape om terug te gaan.");
            }
            void Screen2() // methode voor scherm 2
            {
                Console.Clear();
                Console.WriteLine("MENUKAART - GERECHTEN\n\n");
                //Plaats hieronder de tweede 10 menuopties
                Console.WriteLine($"Lasagne Bolognese, €13,-(gluten/ei/selderij)");
                Console.WriteLine($"Penne pesto kip, €16,-(gluten/ei/pinda's)");
                Console.WriteLine($"Ringatone tomaat, €13,-(gluten/ei)");
                Console.WriteLine($"");
                Console.WriteLine($"");
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
