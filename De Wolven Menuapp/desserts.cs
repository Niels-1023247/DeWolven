using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace De_Wolven_Menuapp
{
    public static class Desserts
    {
        public static void Dessertsmenukaart()
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
                Console.WriteLine(hetgehelemenu.desserts[0].dessertnaam + ", " + hetgehelemenu.desserts[0].prijs + " euro, " + hetgehelemenu.desserts[0].allergenen);
                Console.WriteLine(hetgehelemenu.desserts[1].dessertnaam + ", " + hetgehelemenu.desserts[1].prijs + " euro, " + hetgehelemenu.desserts[1].allergenen);
                Console.WriteLine(hetgehelemenu.desserts[2].dessertnaam + ", " + hetgehelemenu.desserts[2].prijs + " euro, " + hetgehelemenu.desserts[2].allergenen);
                Console.WriteLine(hetgehelemenu.desserts[3].dessertnaam + ", " + hetgehelemenu.desserts[3].prijs + " euro, " + hetgehelemenu.desserts[3].allergenen);
                Console.WriteLine(hetgehelemenu.desserts[4].dessertnaam + ", " + hetgehelemenu.desserts[4].prijs + " euro, " + hetgehelemenu.desserts[4].allergenen);
                Console.WriteLine($"");
                Console.WriteLine($"");
                Console.WriteLine($"");
                Console.WriteLine($"");
                Console.WriteLine($"");
                Console.WriteLine($"Dit is pagina {screen}\n\nDruk op de pijltjestoetsen om van pagina te wisselen, \nDruk op Escape om terug te gaan.");
                Console.WriteLine($"[1]\n ^");
            }


        }
    }
}
