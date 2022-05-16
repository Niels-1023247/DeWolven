using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace De_Wolven_Menuapp
{
    internal class Bestellingopnemen
    {
        public static void Bestelling()
        {
            Console.WriteLine("Voor welke tafel moet er een bestelling opgenomen worden?");
            Console.WriteLine("Type het tafelnummer");
            ConsoleKey tafelnummer = Console.ReadKey().Key;
            Console.Clear();
            Console.WriteLine($"Bestelling voor tafel {tafelnummer}");
            Menu.Menukaart();
            Console.ReadLine();

        }
        //hetzelfde als menu.screen1
        public static void MenuSplit()
        {
            Console.Clear();
            Console.WriteLine("Welkom bij het menu-scherm van De Wolven!");
            Console.WriteLine("[1] Bekijk onze Gerechten");
            Console.WriteLine("[2] Bekijk onze Desserts");
            Console.WriteLine("[3] Bekijk onze Dranken");
            Console.WriteLine("Voer 1, 2 of 3 in");
            ConsoleKey Bestellen = Console.ReadKey().Key;
            if (Bestellen == ConsoleKey.D1)
            {
                Console.Clear();
                Gerechtenkaart.GerechtenBestellen();
            }

            else if (Bestellen == ConsoleKey.D2)
            {
                Console.Clear();
                DessertsBestellen();
            }
            else if (Bestellen == ConsoleKey.D3)

            {
                Console.Clear();
                DrankenBestellen();
            }
            else if (Bestellen == ConsoleKey.Escape) // terug naar hoofdmenu
            {
                Console.Clear();
                
            }
            else
            {
                Console.Clear();
                MenuSplit();
            }
        }
        //Onderstaande method leest de menukaart in
        public static void GerechtenBestellen()
        {

            string MenuJString = File.ReadAllText("Menukaart.JSON");
            var MenuCompleet = JsonConvert.DeserializeObject<Menukaart>(MenuJString);
            int screen = 0;
            int max = MenuCompleet.gerechten.Length;
            int pgmax = (max % 10 == 0) ? (MenuCompleet.gerechten.Length / 10 - 1) : (MenuCompleet.gerechten.Length / 10);
            Console.WriteLine(max);
            Console.WriteLine(pgmax);
            bool active = true;
            while (active)
            {
                Console.WriteLine(screen);
                Console.WriteLine(pgmax);
                Console.WriteLine("MENUKAART - GERECHTEN\n\n");
                for (int i = 10 * screen; (10 * screen) < max ? i < max : i < 10 * screen; i++)
                {
                    Console.WriteLine(MenuCompleet.gerechten[i].Gerechtnaam + ", " + MenuCompleet.gerechten[i].Prijs + " euro, " + MenuCompleet.gerechten[i].Allergenen);
                }
                Console.WriteLine($"Dit is pagina {screen + 1}\n\nDruk op de pijltjestoetsen om van pagina te wisselen, \nDruk op Escape om terug te gaan.");
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

        }
    }
}
