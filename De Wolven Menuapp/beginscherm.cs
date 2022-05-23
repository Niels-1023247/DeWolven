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
            else if (soortgebruiker == ConsoleKey.D9) // verborgen unit test menu
            {
                unitTestingMain();
            }
            if (soortgebruiker == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
        }
        public static void unitTestingMain()
        {
            Console.Clear();
            Console.WriteLine("[UNIT TESTING] Kies de unit test die u wilt doen.");
            Console.WriteLine("[1] Alle tests");
            Console.WriteLine("[2] Testen nieuwe reserveringen toevoegen");
            Console.WriteLine("[3] Testen inloggen met bestaande en niet-bestaande accounts");

            ConsoleKey unitTestKeuzeKey = Console.ReadKey().Key;
            if (unitTestKeuzeKey == ConsoleKey.D1)
            {
                Reservering.AddReserveringUnitTest();
                Loginfo.loginUnitTest();
            }
            else if (unitTestKeuzeKey == ConsoleKey.Escape) Begin();

            else if (unitTestKeuzeKey == ConsoleKey.D2)
            {
                Reservering.AddReserveringUnitTest();
                unitTestingMain();
            }
            else if (unitTestKeuzeKey == ConsoleKey.D3) Loginfo.loginUnitTest();

        }
    }
}
