using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Wolven_Menuapp
{
    internal class Contact
    {
        public static void Contactgegevens()
        {
        
            ConsoleKey input;
            Console.Clear();
            Console.WriteLine("De Wolven");
            Console.WriteLine("CONTACT");
            Console.WriteLine("Adres: Wijnhaven 60,");
            Console.WriteLine("       3011WM Rotterdam");
            Console.WriteLine("Telefoonnumer: 010-2189034");
            Console.WriteLine("\n\nOPENINGSTIJDEN");
            Console.WriteLine("Openigtijden:\n");
            Console.WriteLine("Ma: 12:00-22:00");
            Console.WriteLine("Di: 12:00-22:00");
            Console.WriteLine("Wo: 12:00-22:00");
            Console.WriteLine("Do: 12:00-22:00");
            Console.WriteLine("Vr: 12:00-23:00");
            Console.WriteLine("Za: 10:00-23:00");
            Console.WriteLine("Zo: 10:00-23:00");
            Console.WriteLine("\nDrup op Esc om terug te gaan");
            input = Console.ReadKey().Key;
            if (input == ConsoleKey.Escape)
            {
                Hoofdmenuscherm.SchermKlanten();
            }
        }
    }
}
