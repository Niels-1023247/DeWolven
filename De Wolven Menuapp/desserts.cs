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
            string MenuJString = File.ReadAllText("Menukaart.JSON");
            var MenuCompleet = JsonConvert.DeserializeObject<Menukaart>(MenuJString);
            int screen = 0;
            int max = MenuCompleet.Desserts.Count;
            int pgmax = (max % 10 == 0) ? (MenuCompleet.Desserts.Count / 10 - 1) : (MenuCompleet.Desserts.Count / 10);

            bool active = true;
            while (active)
            {
                Console.WriteLine("MENUKAART - DESSERTS\n\n");
                for (int i = 10 * screen; (10 * (screen + 1)) > max ? i < max : i < 10 * (screen + 1); i++)
                {
                    Console.WriteLine(MenuCompleet.Desserts[i].Dessertnaam + ", " + MenuCompleet.Desserts[i].Prijs + " euro, " + MenuCompleet.Desserts[i].Allergenen);
                }
                Console.WriteLine($"\nDit is pagina {screen + 1}\n\nDruk op de pijltjestoetsen om van pagina te wisselen, \nDruk op Escape om terug te gaan.");


                ConsoleKey input;
                input = Console.ReadKey().Key; // input staat gelijk aan de toets die de gebruiker invoert
                Console.Clear();
                if (input == ConsoleKey.RightArrow & pgmax != screen) // verhoog screenvariable
                {
                    screen++;
                }
                else if (input == ConsoleKey.LeftArrow & pgmax != 0 & screen > 0) // verlaag screenvariable
                {
                    screen -= 1;
                }
                else if (input == ConsoleKey.Escape) // terug naar hoofdmenu
                {
                    active = false;
                }
                else if (input == ConsoleKey.RightArrow & pgmax == screen) // als je na het laatste scherm naar rechts gaat dan gaat hij terug naar het eerste scherm
                {
                    screen = 0;
                }
            }
            Menu.Menukaart();
        }
        public static void DessertsmenukaartOLD()
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
                Console.WriteLine("MENUKAART - DESSERTS\n\n");
                //Plaats hieronder de eerste 10 menuopties
                Console.WriteLine(hetgehelemenu.Desserts[0].Dessertnaam + ", " + hetgehelemenu.Desserts[0].Prijs + " euro, " + hetgehelemenu.Desserts[0].Allergenen);
                Console.WriteLine(hetgehelemenu.Desserts[1].Dessertnaam + ", " + hetgehelemenu.Desserts[1].Prijs + " euro, " + hetgehelemenu.Desserts[1].Allergenen);
                Console.WriteLine(hetgehelemenu.Desserts[2].Dessertnaam + ", " + hetgehelemenu.Desserts[2].Prijs + " euro, " + hetgehelemenu.Desserts[2].Allergenen);
                Console.WriteLine(hetgehelemenu.Desserts[3].Dessertnaam + ", " + hetgehelemenu.Desserts[3].Prijs + " euro, " + hetgehelemenu.Desserts[3].Allergenen);
                Console.WriteLine(hetgehelemenu.Desserts[4].Dessertnaam + ", " + hetgehelemenu.Desserts[4].Prijs + " euro, " + hetgehelemenu.Desserts[4].Allergenen);
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
