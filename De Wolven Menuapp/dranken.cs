using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace De_Wolven_Menuapp
{
    internal class Dranken
    {
        public static void Drankenmenukaart()
        {
            string MenuJString = File.ReadAllText("Menukaart.JSON");
            var MenuCompleet = JsonConvert.DeserializeObject<Menukaart>(MenuJString);
            int screen = 0;
            int max = MenuCompleet.Dranken.Count;
            int pgmax = (max % 10 == 0) ? (MenuCompleet.Dranken.Count / 10 - 1) : (MenuCompleet.Dranken.Count / 10);
            
            while (true)
            {
                Console.WriteLine("MENUKAART - DRANKEN\n\n");
                for (int i = 10 * screen; (10 * (screen + 1)) > max ? i < max : i < 10 * (screen + 1); i++)
                {
                    Console.WriteLine(MenuCompleet.Dranken[i].Dranknaam + ", " + MenuCompleet.Dranken[i].Prijs + " euro, " + MenuCompleet.Dranken[i].Allergenen);
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
                    break;
                }
                else if (input == ConsoleKey.RightArrow & pgmax == screen) // als je na het laatste scherm naar rechts gaat dan gaat hij terug naar het eerste scherm
                {
                    screen = 0;
                }
            }
            Menu.Menukaart();
        }
        public static void DrankenmenukaartOLD()
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
                else if (screen == 3)
                {
                    Screen3();
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
            void Screen1()
            {
                Console.Clear();
                Console.WriteLine("MENUKAART - DRANKEN\n\n\n");
                //Plaats hieronder de derde 10 menuopties
                Console.WriteLine(hetgehelemenu.Dranken[0].Dranknaam + ", " + hetgehelemenu.Dranken[0].Prijs + " euro, " + hetgehelemenu.Dranken[0].Allergenen);
                Console.WriteLine(hetgehelemenu.Dranken[1].Dranknaam + ", " + hetgehelemenu.Dranken[1].Prijs + " euro, " + hetgehelemenu.Dranken[1].Allergenen);
                Console.WriteLine(hetgehelemenu.Dranken[2].Dranknaam + ", " + hetgehelemenu.Dranken[2].Prijs + " euro, " + hetgehelemenu.Dranken[2].Allergenen);
                Console.WriteLine(hetgehelemenu.Dranken[3].Dranknaam + ", " + hetgehelemenu.Dranken[3].Prijs + " euro, " + hetgehelemenu.Dranken[3].Allergenen);
                Console.WriteLine(hetgehelemenu.Dranken[4].Dranknaam + ", " + hetgehelemenu.Dranken[4].Prijs + " euro, " + hetgehelemenu.Dranken[4].Allergenen);
                Console.WriteLine(hetgehelemenu.Dranken[5].Dranknaam + ", " + hetgehelemenu.Dranken[5].Prijs + " euro, " + hetgehelemenu.Dranken[5].Allergenen);
                Console.WriteLine(hetgehelemenu.Dranken[6].Dranknaam + ", " + hetgehelemenu.Dranken[6].Prijs + " euro, " + hetgehelemenu.Dranken[6].Allergenen);
                Console.WriteLine(hetgehelemenu.Dranken[7].Dranknaam + ", " + hetgehelemenu.Dranken[7].Prijs + " euro, " + hetgehelemenu.Dranken[7].Allergenen);
                Console.WriteLine(hetgehelemenu.Dranken[8].Dranknaam + ", " + hetgehelemenu.Dranken[8].Prijs + " euro, " + hetgehelemenu.Dranken[8].Allergenen);
                Console.WriteLine(hetgehelemenu.Dranken[9].Dranknaam + ", " + hetgehelemenu.Dranken[9].Prijs + " euro, " + hetgehelemenu.Dranken[9].Allergenen);
                Console.WriteLine($"Dit is pagina {screen}\n\nDruk op de pijltjestoetsen om van pagina te wisselen, \nDruk op Escape om terug te gaan.");
                Console.WriteLine($"[1] [2] [3]\n ^");
            }
            void Screen2()
            {
                Console.Clear();
                Console.WriteLine("MENUKAART - DRANKEN\n\n\n");
                Console.WriteLine(hetgehelemenu.Dranken[10].Dranknaam + ", " + hetgehelemenu.Dranken[10].Prijs + " euro, " + hetgehelemenu.Dranken[10].Allergenen);
                Console.WriteLine(hetgehelemenu.Dranken[11].Dranknaam + ", " + hetgehelemenu.Dranken[11].Prijs + " euro, " + hetgehelemenu.Dranken[11].Allergenen);
                Console.WriteLine(hetgehelemenu.Dranken[12].Dranknaam + ", " + hetgehelemenu.Dranken[12].Prijs + " euro, " + hetgehelemenu.Dranken[12].Allergenen);
                Console.WriteLine(hetgehelemenu.Dranken[13].Dranknaam + ", " + hetgehelemenu.Dranken[13].Prijs + " euro, " + hetgehelemenu.Dranken[13].Allergenen);
                Console.WriteLine(hetgehelemenu.Dranken[14].Dranknaam + ", " + hetgehelemenu.Dranken[14].Prijs + " euro, " + hetgehelemenu.Dranken[14].Allergenen);
                Console.WriteLine(hetgehelemenu.Dranken[15].Dranknaam + ", " + hetgehelemenu.Dranken[15].Prijs + " euro, " + hetgehelemenu.Dranken[15].Allergenen);
                Console.WriteLine(hetgehelemenu.Dranken[16].Dranknaam + ", " + hetgehelemenu.Dranken[16].Prijs + " euro, " + hetgehelemenu.Dranken[16].Allergenen);
                Console.WriteLine(hetgehelemenu.Dranken[17].Dranknaam + ", " + hetgehelemenu.Dranken[17].Prijs + " euro, " + hetgehelemenu.Dranken[17].Allergenen);
                Console.WriteLine(hetgehelemenu.Dranken[18].Dranknaam + ", " + hetgehelemenu.Dranken[18].Prijs + " euro, " + hetgehelemenu.Dranken[18].Allergenen);
                Console.WriteLine(hetgehelemenu.Dranken[19].Dranknaam + ", " + hetgehelemenu.Dranken[19].Prijs + " euro, " + hetgehelemenu.Dranken[19].Allergenen);
                Console.WriteLine($"Dit is pagina {screen}\n\nDruk op de pijltjestoetsen om van pagina te wisselen, \nDruk op Escape om terug te gaan.");
                Console.WriteLine($"[1] [2] [3]\n     ^");
            }
            void Screen3()
            {
                Console.Clear();
                Console.WriteLine("MENUKAART- DRANKEN\n\n\n");
                Console.WriteLine(hetgehelemenu.Dranken[20].Dranknaam + ", " + hetgehelemenu.Dranken[0].Prijs + " euro, " + hetgehelemenu.Dranken[20].Allergenen);
                Console.WriteLine(hetgehelemenu.Dranken[21].Dranknaam + ", " + hetgehelemenu.Dranken[1].Prijs + " euro, " + hetgehelemenu.Dranken[21].Allergenen);
                Console.WriteLine($"");
                Console.WriteLine($"");
                Console.WriteLine($"");
                Console.WriteLine($"");
                Console.WriteLine($"");
                Console.WriteLine($"");
                Console.WriteLine($"");
                Console.WriteLine($"");
                Console.WriteLine($"Dit is pagina {screen}\n\nDruk op de pijltjestoetsen om van pagina te wisselen, \nDruk op Escape om terug te gaan.");
                Console.WriteLine($"[1] [2] [3]\n         ^");

            }
        }
    }
}
