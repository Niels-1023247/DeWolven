using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace De_Wolven_Menuapp
{
    internal class NEWmenuscherm
    {
        public static void Gerechtenmenukaart()
        {
            string MenuJString = File.ReadAllText("Menukaart.JSON");
            var MenuCompleet = JsonConvert.DeserializeObject<Menukaart>(MenuJString);
            int screen=0;
            int max = MenuCompleet.gerechten.Count;
            int pgmax = (max%10 == 0)?(MenuCompleet.gerechten.Count / 10-1): (MenuCompleet.gerechten.Count / 10);
            Console.WriteLine(max);
            Console.WriteLine(pgmax);
            bool active = true;
            while (active){
                Console.WriteLine(screen);
                Console.WriteLine(pgmax);
                Console.WriteLine("MENUKAART - GERECHTEN\n\n");
                for (int i = 10*screen; (10*screen)<max?i<max: i < 10*screen; i++)
                {
                    Console.WriteLine(MenuCompleet.gerechten[i].Gerechtnaam + ", " + MenuCompleet.gerechten[i].Prijs + " euro, " + MenuCompleet.gerechten[i].Allergenen);
                }
                Console.WriteLine($"Dit is pagina {screen+1}\n\nDruk op de pijltjestoetsen om van pagina te wisselen, \nDruk op Escape om terug te gaan.");
                Console.WriteLine($"[1] [2]\n ^");

                ConsoleKey input;
                input = Console.ReadKey().Key; // input staat gelijk aan de toets die de gebruiker invoert
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
    }
}
