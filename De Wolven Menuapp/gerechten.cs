using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace De_Wolven_Menuapp
{
    public class Gerechtenkaart
    {
        public static void Gerechtenmenukaart()
        {
            int screen = 1; // bewaart op welke van de 5 schermen de gebruiker zit
            int pgmax = 5; // variable die bewaart hoeveel schermen er in totaal zijn
            ConsoleKey input;
            string dejsontekst = File.ReadAllText("Menukaart.JSON");
            var hetgehelemenu = JsonConvert.DeserializeObject<Menukaart>(dejsontekst);
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
                Console.WriteLine(hetgehelemenu.gerechten[0].gerechtnaam + ", " + hetgehelemenu.gerechten[0].prijs + " euro, " + hetgehelemenu.gerechten[0].allergenen);
                Console.WriteLine(hetgehelemenu.gerechten[1].gerechtnaam + ", " + hetgehelemenu.gerechten[1].prijs + " euro, " + hetgehelemenu.gerechten[1].allergenen);
                Console.WriteLine(hetgehelemenu.gerechten[2].gerechtnaam + ", " + hetgehelemenu.gerechten[2].prijs + " euro, " + hetgehelemenu.gerechten[2].allergenen);
                Console.WriteLine(hetgehelemenu.gerechten[3].gerechtnaam + ", " + hetgehelemenu.gerechten[3].prijs + " euro, " + hetgehelemenu.gerechten[3].allergenen);
                Console.WriteLine(hetgehelemenu.gerechten[4].gerechtnaam + ", " + hetgehelemenu.gerechten[4].prijs + " euro, " + hetgehelemenu.gerechten[4].allergenen);
                Console.WriteLine(hetgehelemenu.gerechten[5].gerechtnaam + ", " + hetgehelemenu.gerechten[5].prijs + " euro, " + hetgehelemenu.gerechten[5].allergenen);
                Console.WriteLine(hetgehelemenu.gerechten[6].gerechtnaam + ", " + hetgehelemenu.gerechten[6].prijs + " euro, " + hetgehelemenu.gerechten[6].allergenen);
                Console.WriteLine(hetgehelemenu.gerechten[7].gerechtnaam + ", " + hetgehelemenu.gerechten[7].prijs + " euro, " + hetgehelemenu.gerechten[7].allergenen);
                Console.WriteLine(hetgehelemenu.gerechten[8].gerechtnaam + ", " + hetgehelemenu.gerechten[8].prijs + " euro, " + hetgehelemenu.gerechten[8].allergenen);
                Console.WriteLine(hetgehelemenu.gerechten[9].gerechtnaam + ", " + hetgehelemenu.gerechten[9].prijs + " euro, " + hetgehelemenu.gerechten[9].allergenen);
                
                Console.WriteLine($"Dit is pagina {screen}\n\nDruk op de pijltjestoetsen om van pagina te wisselen, \nDruk op Escape om terug te gaan.");
                Console.WriteLine($"[1] [2]\n ^");
            }
            void Screen2() // methode voor scherm 2
            {
                Console.Clear();
                Console.WriteLine("MENUKAART - GERECHTEN\n\n");
                Console.WriteLine(hetgehelemenu.gerechten[10].gerechtnaam + ", " + hetgehelemenu.gerechten[10].prijs + " euro, " + hetgehelemenu.gerechten[10].allergenen);
                Console.WriteLine(hetgehelemenu.gerechten[11].gerechtnaam + ", " + hetgehelemenu.gerechten[11].prijs + " euro, " + hetgehelemenu.gerechten[11].allergenen);
                Console.WriteLine($"");
                Console.WriteLine($"");
                Console.WriteLine($"");
                Console.WriteLine($"");

                Console.WriteLine($"Dit is pagina {screen}\n\nDruk op de pijltjestoetsen om van pagina te wisselen, \nDruk op Escape om terug te gaan.");
                Console.WriteLine($"[1] [2]\n     ^");
            }

        }
    }
}
