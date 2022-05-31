using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Wolven_Menuapp
{
    public class Beginscherm
    {
        public static void Begin()
        {
            while (true)
            {
                Console.Clear();
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
                    Loginfo.LoginAccount("Medewerker");
                }
                else if (soortgebruiker == ConsoleKey.U) // verborgen unit test menu [U]
                {
                    unitTestingMain();
                }
                if (soortgebruiker == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }
        public static void unitTestingMain()
        {
            int errors = 0;

            Console.Clear();
            Console.WriteLine("[UNIT TESTING] Kies de unit test die u wilt doen.");
            Console.WriteLine("[1] Alle tests");
            Console.WriteLine("[2] Testen nieuwe reserveringen toevoegen");
            Console.WriteLine("[3] Testen inloggen met bestaande en niet-bestaande accounts");

            ConsoleKey unitTestKeuzeKey = Console.ReadKey().Key;
            Console.WriteLine("");
            if (unitTestKeuzeKey == ConsoleKey.D1)
            {
                Reservering.AddReserveringUnitTest();
                Console.WriteLine("");
                Loginfo.loginUnitTest();
                Console.WriteLine("");

                Console.WriteLine("[UNIT TESTING] Alle tests uitgevoerd!");
                Console.WriteLine("[UNIT TESTING] Er zijn {0} testen gefaald.", errors);

                // error message als het niet gelukt is
                ConsoleKey cont = Console.ReadKey().Key;
                if (true) unitTestingMain();

            }
            else if (unitTestKeuzeKey == ConsoleKey.Escape)
            {
                Console.Clear();
                Begin();
            }

            else if (unitTestKeuzeKey == ConsoleKey.D2)
            {
                Reservering.AddReserveringUnitTest();
                ConsoleKey cont = Console.ReadKey().Key;
                if (true) unitTestingMain();
            }
            else if (unitTestKeuzeKey == ConsoleKey.D3)
            {
                Loginfo.loginUnitTest();
                ConsoleKey cont = Console.ReadKey().Key;
                if (true) unitTestingMain();
            }
        }
    }
}
