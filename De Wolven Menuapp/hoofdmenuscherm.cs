using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Wolven_Menuapp
{
    internal static class Hoofdmenuscherm
    {
        public static void SchermMedewerker()
        {
            Console.Clear();
            Console.WriteLine("Welkom medewerker\n\n");
            Console.WriteLine("Log in");
            Console.WriteLine("Druk op Enter");

            ConsoleKey optieMedewerker = Console.ReadKey().Key;
            if (optieMedewerker == ConsoleKey.Enter)
            {
                Loginfo.Loginfoscherm("Medewerker");
            }

        }

        public static void SchermAdmin()
        {
            Console.Clear();
            Console.WriteLine("Welkom Admin.\n\n");
            Console.WriteLine("Momenteel is deze admin pagina nog onder constructie.");

        }

        public static void SchermKlanten()
        {
            Console.Clear();
            Console.WriteLine("Welkom bij de wolven gast!");
            Console.WriteLine("[1] Reserveer een tafel");
            Console.WriteLine("[2] Bekijk de menukaart");
            Console.WriteLine("[3] Log in");
            Console.WriteLine("[4] Registreer");
            Console.WriteLine("[5] Over ons");
            Console.WriteLine("Voer 1, 2, 3, 4 of 5 in");
            Console.WriteLine("Druk op esc om terug te gaan");

            ConsoleKey optieklanten = Console.ReadKey().Key;
            if (optieklanten == ConsoleKey.D1)
            {
                Reservering.AddReservering();
                
            }

            else if (optieklanten == ConsoleKey.D2)
            {
                Console.Clear();
                Menu.Menukaart();
            }
            else if (optieklanten == ConsoleKey.D3)

            {
                Loginfo.Loginfoscherm("Klant");
            }

            else if (optieklanten == ConsoleKey.D4)
            {
                Console.Clear();
                Loginfo.CreateAccount();

            }
            else if (optieklanten == ConsoleKey.D5)
            {
                Console.Clear();
                Contact.Contactgegevens();

            }
            else if (optieklanten == ConsoleKey.Escape)
            {
                Beginscherm.Begin();
            }
        }
    }
}
