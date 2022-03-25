﻿using System;
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
            Console.WriteLine("Voer 1, 2, 3, 4 of 5");
            string optiemedewerker = Console.ReadLine();
        }

        public static void SchermKlanten()
        {
            Console.Clear();
            Console.WriteLine("Welkom bij de wolven gast!");
            Console.WriteLine("[1] Reserveer een tafel");
            Console.WriteLine("[2] Bekijk ons menukaart");
            Console.WriteLine("[3] Log in");
            Console.WriteLine("[4] Over ons");
            Console.WriteLine("Voer 1, 2, 3 of 4 in");
            
            ConsoleKey optieklanten = Console.ReadKey().Key;
            if (optieklanten == ConsoleKey.D1)
            {
                Console.WriteLine("Nog niks");
            }

            else if (optieklanten == ConsoleKey.D2)
            {
                Console.Clear();
                Menu.menukaart();
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
        }
    }
}
