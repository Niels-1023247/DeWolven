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
                    Menu.menukaart();
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
                Console.WriteLine(hetgehelemenu.dranken[0].dranknaam + ", " + hetgehelemenu.dranken[0].prijs + " euro, " + hetgehelemenu.dranken[0].allergenen);
                Console.WriteLine(hetgehelemenu.dranken[1].dranknaam + ", " + hetgehelemenu.dranken[1].prijs + " euro, " + hetgehelemenu.dranken[1].allergenen);
                Console.WriteLine(hetgehelemenu.dranken[2].dranknaam + ", " + hetgehelemenu.dranken[2].prijs + " euro, " + hetgehelemenu.dranken[2].allergenen);
                Console.WriteLine(hetgehelemenu.dranken[3].dranknaam + ", " + hetgehelemenu.dranken[3].prijs + " euro, " + hetgehelemenu.dranken[3].allergenen);
                Console.WriteLine(hetgehelemenu.dranken[4].dranknaam + ", " + hetgehelemenu.dranken[4].prijs + " euro, " + hetgehelemenu.dranken[4].allergenen);
                Console.WriteLine(hetgehelemenu.dranken[5].dranknaam + ", " + hetgehelemenu.dranken[5].prijs + " euro, " + hetgehelemenu.dranken[5].allergenen);
                Console.WriteLine(hetgehelemenu.dranken[6].dranknaam + ", " + hetgehelemenu.dranken[6].prijs + " euro, " + hetgehelemenu.dranken[6].allergenen);
                Console.WriteLine(hetgehelemenu.dranken[7].dranknaam + ", " + hetgehelemenu.dranken[7].prijs + " euro, " + hetgehelemenu.dranken[7].allergenen);
                Console.WriteLine(hetgehelemenu.dranken[8].dranknaam + ", " + hetgehelemenu.dranken[8].prijs + " euro, " + hetgehelemenu.dranken[8].allergenen);
                Console.WriteLine(hetgehelemenu.dranken[9].dranknaam + ", " + hetgehelemenu.dranken[9].prijs + " euro, " + hetgehelemenu.dranken[9].allergenen);
                Console.WriteLine($"Dit is pagina {screen}\n\nDruk op de pijltjestoetsen om van pagina te wisselen, \nDruk op Escape om terug te gaan.");
                Console.WriteLine($"[1] [2] [3]\n ^");
            }
            void Screen2()
            {
                Console.Clear();
                Console.WriteLine("MENUKAART - DRANKEN\n\n\n");
                Console.WriteLine(hetgehelemenu.dranken[10].dranknaam + ", " + hetgehelemenu.dranken[10].prijs + " euro, " + hetgehelemenu.dranken[10].allergenen);
                Console.WriteLine(hetgehelemenu.dranken[11].dranknaam + ", " + hetgehelemenu.dranken[11].prijs + " euro, " + hetgehelemenu.dranken[11].allergenen);
                Console.WriteLine(hetgehelemenu.dranken[12].dranknaam + ", " + hetgehelemenu.dranken[12].prijs + " euro, " + hetgehelemenu.dranken[12].allergenen);
                Console.WriteLine(hetgehelemenu.dranken[13].dranknaam + ", " + hetgehelemenu.dranken[13].prijs + " euro, " + hetgehelemenu.dranken[13].allergenen);
                Console.WriteLine(hetgehelemenu.dranken[14].dranknaam + ", " + hetgehelemenu.dranken[14].prijs + " euro, " + hetgehelemenu.dranken[14].allergenen);
                Console.WriteLine(hetgehelemenu.dranken[15].dranknaam + ", " + hetgehelemenu.dranken[15].prijs + " euro, " + hetgehelemenu.dranken[15].allergenen);
                Console.WriteLine(hetgehelemenu.dranken[16].dranknaam + ", " + hetgehelemenu.dranken[16].prijs + " euro, " + hetgehelemenu.dranken[16].allergenen);
                Console.WriteLine(hetgehelemenu.dranken[17].dranknaam + ", " + hetgehelemenu.dranken[17].prijs + " euro, " + hetgehelemenu.dranken[17].allergenen);
                Console.WriteLine(hetgehelemenu.dranken[18].dranknaam + ", " + hetgehelemenu.dranken[18].prijs + " euro, " + hetgehelemenu.dranken[18].allergenen);
                Console.WriteLine(hetgehelemenu.dranken[19].dranknaam + ", " + hetgehelemenu.dranken[19].prijs + " euro, " + hetgehelemenu.dranken[19].allergenen);
                Console.WriteLine($"Dit is pagina {screen}\n\nDruk op de pijltjestoetsen om van pagina te wisselen, \nDruk op Escape om terug te gaan.");
                Console.WriteLine($"[1] [2] [3]\n     ^");
            }
            void Screen3()
            {
                Console.Clear();
                Console.WriteLine("MENUKAART- DRANKEN\n\n\n");
                Console.WriteLine(hetgehelemenu.dranken[20].dranknaam + ", " + hetgehelemenu.dranken[0].prijs + " euro, " + hetgehelemenu.dranken[20].allergenen);
                Console.WriteLine(hetgehelemenu.dranken[21].dranknaam + ", " + hetgehelemenu.dranken[1].prijs + " euro, " + hetgehelemenu.dranken[21].allergenen);
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
