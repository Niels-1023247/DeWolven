using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Wolven_Menuapp
{
    internal class Menu
    {
        public static void menukaart()
        {
            int screen = 1;
            int pgmax = 5;
            char euro = '€';
            ConsoleKey input;
            while (true)
            {
                if (screen == 1)
                {
                    Screen1();
                }
                else if (screen == 2)
                {
                    Screen2();
                }
                else if (screen == 3)
                {
                    Screen3();
                }                
                else if (screen == 4)
                {
                    Screen4();
                }                
                else if (screen == 5)
                {
                    Screen5();
                }
                input = Console.ReadKey().Key;
                if (input == ConsoleKey.RightArrow & pgmax != screen)
                {
                    screen++;
                }
                else if (input == ConsoleKey.LeftArrow & pgmax != 1)
                {
                    screen -= 1;
                }
                else if (input == ConsoleKey.Escape)
                {
                    break;
                }

            }
            void Screen1()
            {
                Console.Clear();
                Console.WriteLine("MENUKAART\n\n");
                //Plaats hieronder de eerste 10 menuopties
                Console.WriteLine($"menuoptie 1, prijsinformatie, allergeneninformatie");
                Console.WriteLine($"Dit is pagina {screen}\n\nDruk op de pijltjestoetsen om van pagina te wisselen, \nDruk ok Escape om terug te gaan.");
            }
            void Screen2()
            {
                Console.Clear();
                Console.WriteLine("MENUKAART\n\n");
                //Plaats hieronder de tweede 10 menuopties
                Console.WriteLine($"menuoptie 2, prijsinformatie, allergeneninformatie");
                Console.WriteLine($"Dit is pagina {screen}\n\nDruk op de pijltjestoetsen om van pagina te wisselen, \nDruk ok Escape om terug te gaan.");
            }
            void Screen3()
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
                Console.WriteLine($"Dit is pagina {screen}\n\nDruk op de pijltjestoetsen om van pagina te wisselen, \nDruk ok Escape om terug te gaan.");
            }            
            void Screen4()
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
                Console.WriteLine($"Dit is pagina {screen}\n\nDruk op de pijltjestoetsen om van pagina te wisselen, \nDruk ok Escape om terug te gaan.");
            }            
            void Screen5()
            {
                Console.Clear();
                Console.WriteLine("MENUKAART- DRANKEN\n\n\n");
                Console.WriteLine($"Mystery Cocktail..., €20,- (Bevat gluten, ei, vis, noten, selderij)");
                Console.WriteLine($"Dit is pagina {screen}\n\nDruk op de pijltjestoetsen om van pagina te wisselen, \nDruk ok Escape om terug te gaan.");
            }
        }
    }
}
