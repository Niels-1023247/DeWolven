using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Wolven_Menuapp
{
    internal class medewerkerHome
    {
        public static void SchermMedewerker(string melding = "")
        {
            Console.Clear();
            Console.WriteLine("Welkom medewerker");
            Console.WriteLine("[1] Bekijk de reserveringen");
            Console.WriteLine("[2] Neem een bestelling op");
            Console.WriteLine("[3] Maak een nieuwe reservering");
            Console.WriteLine("[4] Bekijk besteltotaal per tafel");
            Console.WriteLine("[5] Verander menukaart");
            Console.WriteLine("Voer 1, 2, 3, 4 of 5 in");

            if (melding != "") Console.WriteLine("\n" + melding);

            ConsoleKey optieMedewerker = Console.ReadKey().Key;
            if (optieMedewerker == ConsoleKey.D1)
            {
                Console.Clear();
                Verander.DisplayReserveringen();
            }

            else if (optieMedewerker == ConsoleKey.D2)
            {
                Bestellingopnemen.nieuweBestelling();
            }
            else if (optieMedewerker == ConsoleKey.D3)
            {
                Console.Clear();
                Reservering.AddReservering();
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
