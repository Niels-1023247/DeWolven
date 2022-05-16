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
            Console.WriteLine("Kies van welke categorie u wilt bestellen...");
            Console.WriteLine("[1] Gerechten");
            Console.WriteLine("[2] Desserts");
            Console.WriteLine("[3] Dranken");
            Console.WriteLine("Voer 1, 2 of 3 in");
            ConsoleKey Bestellen = Console.ReadKey().Key;
            if (Bestellen == ConsoleKey.D1)
            {
                Console.Clear();
                GerechtenBestellen();
            }

            else if (Bestellen == ConsoleKey.D2)
            {
                Console.Clear();
                //DessertsBestellen();
            }
            else if (Bestellen == ConsoleKey.D3)

            {
                Console.Clear();
                //DrankenBestellen();
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
            int max = MenuCompleet.gerechten.Count;
            int pgmax = (max % 10 == 0) ? (MenuCompleet.gerechten.Count / 10 - 1) : (MenuCompleet.gerechten.Count / 10);
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
                    Console.WriteLine($"[{i}] {MenuCompleet.gerechten[i].Gerechtnaam}, {MenuCompleet.gerechten[i].Prijs} euro, {MenuCompleet.gerechten[i].Allergenen}");
                }
                Console.WriteLine($"Dit is pagina {screen + 1}\n\nDruk op de pijltjestoetsen om van pagina te wisselen, \nDruk op Escape om terug te gaan.\n\nGebruik de nummertoetsen om een menuoptie te kiezen");
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
                else
                {
                    var bestellingList=new List<Gerechten>();
                    ConsoleKey[] keys = { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.D6, ConsoleKey.D7, ConsoleKey.D, ConsoleKey.D9, ConsoleKey.D0, };
                    for (int i = 0; i < keys.Length; i++)
                    if (input == keys[i])
                    {
                        bestellingList.Add(MenuCompleet.gerechten[10 * screen + i]);
                    }
                    Console.WriteLine(bestellingList[0].Gerechtnaam);

                }
                Console.WriteLine("end");
            }

        }
    }
}
