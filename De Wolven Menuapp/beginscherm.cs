using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Wolven_Menuapp
{
    internal class Beginscherm
    {
        public static void Begin()
        {
            ConsoleKey soortgebruiker;
            Console.WriteLine("Welkom bij de wolven!");
            Console.WriteLine("Bent u gast of medewerker?");
            Console.WriteLine("[1] Gast");
            Console.WriteLine("[2] Medewerker");
            Console.WriteLine("Voer 1 of 2 in");
            Console.WriteLine("\nDruk op Esc om te stoppen");
            soortgebruiker = Console.ReadKey().Key;
            if (soortgebruiker == ConsoleKey.D1)
            {

                Hoofdmenuscherm.SchermKlanten();
            }
            else if (soortgebruiker == ConsoleKey.D2)
            {

                Hoofdmenuscherm.SchermMedewerker();
            }
            else if (soortgebruiker == ConsoleKey.D9) // backdoor voor snel naar bestellen
            {

                Console.Clear();
                Bestellingopnemen.nieuweBestelling();
            }
            
            if (soortgebruiker == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
        }
    }
}
