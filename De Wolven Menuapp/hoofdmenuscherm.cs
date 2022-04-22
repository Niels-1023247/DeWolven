﻿using System;
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
            Console.WriteLine("Voer 1, 2, 3, 4 of 5");


            ConsoleKey optiemedewerker = Console.ReadKey().Key;
            if (optiemedewerker == ConsoleKey.D1)
            {
                Verander.DisplayReserveringen();
            }
            else if (optiemedewerker == ConsoleKey.D2)
            {
                Console.Clear();
                Bestellingopnemen.Bestelling();

            }
            else if (optiemedewerker == ConsoleKey.D3)
            {
                Console.Clear();
                Reservering.AddReservering();

            }
            else if (optiemedewerker == ConsoleKey.D4)
            {
                Console.Clear();
                Console.WriteLine("ook nog niks");
            }
            else if (optiemedewerker == ConsoleKey.D5)
            {
                Console.Clear();
                Verander.VeranderenReservering();
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
