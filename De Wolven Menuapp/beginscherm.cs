using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Wolven_Menuapp
{
    internal class beginscherm
    {
        public void Begin()
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
                hoofdmenuscherm b = new hoofdmenuscherm();
                b.SchermKlanten();
            }
            else if (soortgebruiker == ConsoleKey.D2)
            {
                hoofdmenuscherm e = new hoofdmenuscherm();
                e.SchermMedewerker();
            }
            
            if (soortgebruiker == ConsoleKey.Escape)
            {
                
            }
        }
    }
}
