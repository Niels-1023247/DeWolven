using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Wolven_Menuapp
{
    internal class medewerkerHome
    {
        public static void SchermMedewerker()
        {
            Console.Clear();
            Console.WriteLine("Welkom medewerker");
            Console.WriteLine("[1] Bekijk de reserveringen");
            Console.WriteLine("[2] Neem een bestelling op");
            Console.WriteLine("[3] Maak een nieuwe reservering");
            Console.WriteLine("[4] Bekijk besteltotaal per tafel");
            Console.WriteLine("[5] Verander menukaart");
            Console.WriteLine("Voer 1, 2, 3, 4 of 5 in");

            ConsoleKey optieMedewerker = Console.ReadKey().Key;
            if (optieMedewerker == ConsoleKey.D1)
            {
                Console.WriteLine("Nog niks");
            }

            else if (optieMedewerker == ConsoleKey.D2)
            {
                Console.WriteLine("Nog niks");
            }
            else if (optieMedewerker == ConsoleKey.D3)
            {

                Console.WriteLine("Nog niks");
            }

            else if (optieMedewerker == ConsoleKey.D4)
            {
                Console.WriteLine("Nog niks");

            }

            else if (optieMedewerker == ConsoleKey.D5)
            {
                Console.WriteLine("Nog niks");

            }
        }
    }
}
