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
            Console.WriteLine("Welkom alpha");
            Console.WriteLine("[1] Bekijk de reserveringen");
            Console.WriteLine("[2] Neem een bestelling op");
            Console.WriteLine("[3] Maak een nieuwe reservering");
            Console.WriteLine("[4] Bekijk besteltotaal per tafel");
            Console.WriteLine("[5] Verander menukaart");
            Console.WriteLine("[6] Verander Contactinformatie");
            Console.WriteLine("Voer 1, 2, 3, 4, 5 of 6");
            ConsoleKey optiemedewerker = Console.ReadKey().Key;
            if (optiemedewerker == ConsoleKey.D6)
            {
                Contact.ChangeInfoMenu();
            }
        }

        public static void SchermKlanten()
        {
            Console.Clear();
            Console.WriteLine("Welkom bij de wolven gast!");
            Console.WriteLine("[1] Reserveer een tafel");
            Console.WriteLine("[2] Bekijk de menukaart");
            Console.WriteLine("[3] Log in");
            Console.WriteLine("[4] Over ons");
            Console.WriteLine("Voer 1, 2, 3 of 4 in");
            Console.WriteLine("Druk op esc om terug te gaan");
            
            ConsoleKey optieklanten = Console.ReadKey().Key;
            if (optieklanten == ConsoleKey.D1)
            {
                Console.WriteLine("Nog niks");
            }

            else if (optieklanten == ConsoleKey.D2)
            {
                Console.Clear();
                Menu.Menukaart();
            }
            else if (optieklanten == ConsoleKey.D3)

            {
                Loginfo.Loginfoscherm();
            }

            else if (optieklanten == ConsoleKey.D4)
            {
                Console.Clear();
                Contact.Contactgegevens();

            }
            else if (optieklanten== ConsoleKey.Escape)
            {
                Beginscherm.Begin();
            }
        }
    }
}
