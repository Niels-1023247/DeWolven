using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        
      
    }
}
