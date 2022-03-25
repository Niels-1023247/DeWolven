using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Wolven_Menuapp
{
    internal class dranken
    {
        public void drankenmenukaart()
        {
            int screen = 1; // bewaart op welke van de 5 schermen de gebruiker zit
            int pgmax = 5; // variable die bewaart hoeveel schermen er in totaal zijn
            char euro = '€'; // euroteken voor c# gezeur
            ConsoleKey input;
            Menu menu = new Menu();
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
            void Screen1() // methode voor scherm 3
            {
                Console.Clear();
                Console.WriteLine("MENUKAART - DRANKEN\n\n\n");
                //Plaats hieronder de derde 10 menuopties
                Console.WriteLine($"Broccolishake met spinazie, munt en avocado, €8,-");
                Console.WriteLine($"Mangoshake met chilipeper en kurkuma, €8,-");
                Console.WriteLine($"Bananenshake met kikkererwten en kiwi, €8,-");
                Console.WriteLine($"Wortelshake met kurkuma en kokosmelk, €8,- (sojamelk/kokosmelk)");
                Console.WriteLine($"Paprikashake met tomaat en peterselie, €8,-");
                Console.WriteLine($"Coca-Cola met citroenschijfje, €4 (Classic/Light/Zero)");
                Console.WriteLine($"Kersen- ananassap, €6");
                Console.WriteLine($"Water (1L), €2");
                Console.WriteLine($"Muntthee, €3");
                Console.WriteLine($"Rooibosthee, €3");
                Console.WriteLine($"Dit is pagina {screen}\n\nDruk op de pijltjestoetsen om van pagina te wisselen, \nDruk op Escape om terug te gaan.");
            }
            void Screen2() // methode voor scherm 4
            {
                Console.Clear();
                Console.WriteLine("MENUKAART - DRANKEN\n\n\n");
                Console.WriteLine($"Lipton Ijsthee, €4,- (citroen/mango/green tea)");
                Console.WriteLine($"Cassis, €4,-");
                Console.WriteLine($"Warme chocolademelk, €4,- (Bevat melk)");
                Console.WriteLine($"Tapbier, €4,- (Heineken/Amstel/Grolsch/Hertog Jan) (Bevat gluten)");
                Console.WriteLine($"Verse bio-melk, €4,-");
                Console.WriteLine($"Biologische merlot, €7");
                Console.WriteLine($"Biologische blanc sauvignon, €7");
                Console.WriteLine($"Frisse meloencocktail met aardbeigarnering, €7");
                Console.WriteLine($"Zoete muntcocktail met honing en steranijs, €7");
                Console.WriteLine($"Kruidige tomatencocktail met tuinkersgarnering, €7");
                Console.WriteLine($"Dit is pagina {screen}\n\nDruk op de pijltjestoetsen om van pagina te wisselen, \nDruk op Escape om terug te gaan.");
            }
            void Screen3() // methode voor scherm 5
            {
                Console.Clear();
                Console.WriteLine("MENUKAART- DRANKEN\n\n\n");
                Console.WriteLine($"Mystery Cocktail..., €20,- (Bevat gluten, ei, vis, noten, selderij)");
                Console.WriteLine($"Garnalencocktail met gin en tonic, €8,- (Bevat vis)");
                Console.WriteLine($"");
                Console.WriteLine($"");
                Console.WriteLine($"");
                Console.WriteLine($"");
                Console.WriteLine($"");
                Console.WriteLine($"");
                Console.WriteLine($"");
                Console.WriteLine($"");
                Console.WriteLine($"Dit is pagina {screen}\n\nDruk op de pijltjestoetsen om van pagina te wisselen, \nDruk op Escape om terug te gaan.");

            }
        }
    }
}
