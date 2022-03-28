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
                    Menu.Menukaart();
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
                Console.WriteLine(hetgehelemenu.gerechten[0].Gerechtnaam + ", " + hetgehelemenu.gerechten[0].Prijs + " euro, " + hetgehelemenu.gerechten[0].Allergenen);
                Console.WriteLine(hetgehelemenu.gerechten[1].Gerechtnaam + ", " + hetgehelemenu.gerechten[1].Prijs + " euro, " + hetgehelemenu.gerechten[1].Allergenen);
                Console.WriteLine(hetgehelemenu.gerechten[2].Gerechtnaam + ", " + hetgehelemenu.gerechten[2].Prijs + " euro, " + hetgehelemenu.gerechten[2].Allergenen);
                Console.WriteLine(hetgehelemenu.gerechten[3].Gerechtnaam + ", " + hetgehelemenu.gerechten[3].Prijs + " euro, " + hetgehelemenu.gerechten[3].Allergenen);
                Console.WriteLine(hetgehelemenu.gerechten[4].Gerechtnaam + ", " + hetgehelemenu.gerechten[4].Prijs + " euro, " + hetgehelemenu.gerechten[4].Allergenen);
                Console.WriteLine(hetgehelemenu.gerechten[5].Gerechtnaam + ", " + hetgehelemenu.gerechten[5].Prijs + " euro, " + hetgehelemenu.gerechten[5].Allergenen);
                Console.WriteLine(hetgehelemenu.gerechten[6].Gerechtnaam + ", " + hetgehelemenu.gerechten[6].Prijs + " euro, " + hetgehelemenu.gerechten[6].Allergenen);
                Console.WriteLine(hetgehelemenu.gerechten[7].Gerechtnaam + ", " + hetgehelemenu.gerechten[7].Prijs + " euro, " + hetgehelemenu.gerechten[7].Allergenen);
                Console.WriteLine(hetgehelemenu.gerechten[8].Gerechtnaam + ", " + hetgehelemenu.gerechten[8].Prijs + " euro, " + hetgehelemenu.gerechten[8].Allergenen);
                Console.WriteLine(hetgehelemenu.gerechten[9].Gerechtnaam + ", " + hetgehelemenu.gerechten[9].Prijs + " euro, " + hetgehelemenu.gerechten[9].Allergenen);
                
                Console.WriteLine($"Dit is pagina {screen}\n\nDruk op de pijltjestoetsen om van pagina te wisselen, \nDruk op Escape om terug te gaan.");
                Console.WriteLine($"[1] [2]\n ^");
            }
            void Screen2() // methode voor scherm 2
            {
                Console.Clear();
                Console.WriteLine("MENUKAART - GERECHTEN\n\n");
                Console.WriteLine(hetgehelemenu.gerechten[10].Gerechtnaam + ", " + hetgehelemenu.gerechten[10].Prijs + " euro, " + hetgehelemenu.gerechten[10].Allergenen);
                Console.WriteLine(hetgehelemenu.gerechten[11].Gerechtnaam + ", " + hetgehelemenu.gerechten[11].Prijs + " euro, " + hetgehelemenu.gerechten[11].Allergenen);
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
