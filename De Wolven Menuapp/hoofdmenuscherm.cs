﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Wolven_Menuapp
{
    // hoofdmenu's voor elke soort gebruiker
    internal static class Hoofdmenuscherm
    {
        //Void
        /*
        public static void SchermMedewerker()
        {
            Console.Clear();
            Console.WriteLine("Welkom medewerker\n\n");
            Console.WriteLine("Log in");
            Console.WriteLine("Druk op Enter");

            ConsoleKey optieMedewerker = Console.ReadKey().Key;
            if (optieMedewerker == ConsoleKey.Enter)
            {
                Console.Clear();
                Loginfo.LoginAccount("Medewerker");
            }
            else if (optieMedewerker == ConsoleKey.Escape)
            {
                Console.Clear();
                Beginscherm.Begin();
            }

        }*/

        public static void SchermAdmin()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welkom admin bij De Wolven");
                Console.WriteLine("[1] Contactinformatie aanpassen");
                Console.WriteLine("[2] Reservering aanpassen");
                Console.WriteLine("[3] Maak medewerkers account");
                Console.WriteLine("\nGeadvanceerde opties");
                Console.WriteLine("[4] Unit testing menu");
                Console.WriteLine("Voer [1], [2], [3] of [4] in...");
                Console.WriteLine("Druk op [Escape] om terug te gaan");

                ConsoleKey optieadmin = Console.ReadKey().Key;
                if (optieadmin == ConsoleKey.D1)
                {
                    Console.Clear();
                    Contact.ChangeInfoMenu();
                }

                else if (optieadmin == ConsoleKey.D2)
                {
                    Console.Clear();
                    Verander.VeranderenReservering();
                }
                else if (optieadmin == ConsoleKey.D3)
                {
                    Console.Clear();
                    Loginfo.CreateEmployeeAccount();

                }
                else if (optieadmin == ConsoleKey.D4)
                {
                    Console.Clear();
                    Beginscherm.unitTestingMain();
                }
                else if (optieadmin == ConsoleKey.Escape)
                {
                    Console.Clear();
                    break;
                }
            }
        }

        public static void SchermKlanten()
        {
            
            bool inlogstatus = Program.LoginCheck();
            while (true)
            {
                Console.Clear();
                if (inlogstatus) Console.WriteLine($" Welkom bij de wolven, {Program.ActiefAccountValues("Name")}!");
                else Console.WriteLine("Welkom bij de wolven!");
                Console.WriteLine("[1] Reserveer een tafel");
                Console.WriteLine("[2] Bekijk de menukaart");
                Console.WriteLine("[3] Over ons");
                if (!inlogstatus) Console.WriteLine("[4] Log in");
                if (!inlogstatus) Console.WriteLine("[5] Registreer");
                if (!inlogstatus) Console.WriteLine("[6] Reservering aanpassen");
                if (inlogstatus) Console.WriteLine("[4] Mijn reserveringen inzien");
                
                Console.WriteLine("Voer een van de zichtbare getallen in en druk op [Enter]");
                Console.WriteLine("Druk op esc om terug te gaan");

                ConsoleKey optieklanten = Console.ReadKey().Key;
                if (optieklanten == ConsoleKey.D1)
                {
                    Console.Clear();
                    Reservering.AddReservering();
                }

                else if (optieklanten == ConsoleKey.D2)
                {
                    Console.Clear();
                    Menu.Menukaart();
                }
                else if (optieklanten == ConsoleKey.D4 && !inlogstatus)

                {
                    Console.Clear();
                    Loginfo.LoginAccount("Klant");
                }

                else if (optieklanten == ConsoleKey.D5 && !inlogstatus)
                {
                    Console.Clear();
                    Loginfo.CreateAccount();

                }
                else if (optieklanten == ConsoleKey.D6)
                {
                    Console.Clear();
                    Verander.reserveringAanpassenKlant();

                }

                else if (optieklanten == ConsoleKey.D4 && inlogstatus)
                {
                    Console.Clear();
                    Reservering.mijnReserveringenInzien(Program.ActiefAccountValues("Name"));

                }
                else if (optieklanten == ConsoleKey.D3)
                {
                    Console.Clear();
                    Contact.Contactgegevens();

                }
                else if (optieklanten == ConsoleKey.Escape)
                {
                    Console.Clear();
                    Program.SetLoginValues(null, null, null, null, null, null, false);
                    break;
                }
            }
        }
    }
}
