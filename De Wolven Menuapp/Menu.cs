using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Wolven_Menuapp
{
    internal class Menu
    {
        public static void menukaart() // method om het menu tevoorschijn te *. toveren *'.
        {
            int screen = 1; // bewaart op welke van de 5 schermen de gebruiker zit
            int pgmax = 5; // variable die bewaart hoeveel schermen er in totaal zijn
            ConsoleKey input;
            // hoofdmenu = new Hoofdmenuscherm();
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
                    Hoofdmenuscherm.SchermKlanten();
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
                Console.WriteLine("Welkom bij het menu-scherm van De Wolven!");
                Console.WriteLine("[1] Bekijk onze Gerechten");
                Console.WriteLine("[2] Bekijk onze Desserts");
                Console.WriteLine("[3] Bekijk onze Dranken");
                Console.WriteLine("Voer 1, 2 of 3 in");
                ConsoleKey optieklanten = Console.ReadKey().Key;
                if (optieklanten == ConsoleKey.D1)
                {
                    Console.Clear();
                    gerechten opengerechten = new gerechten();
                    opengerechten.gerechtenmenukaart();
                }

                else if (optieklanten == ConsoleKey.D2)
                {
                    Console.Clear();
                    desserts opendesserts = new desserts();
                    opendesserts.dessertsmenukaart();
                }
                else if (optieklanten == ConsoleKey.D3)

                {
                    Console.Clear();
                    dranken opendranken = new dranken();
                    opendranken.drankenmenukaart();
                }
            }
        }
    }
}
