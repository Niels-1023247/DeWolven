﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Wolven_Menuapp
{
    internal class contact
    {
        public void Contactgegevens()
        {
            hoofdmenuscherm b = new hoofdmenuscherm();
            ConsoleKey input;
            Console.Clear();
            Console.WriteLine("De Wolven");
            Console.WriteLine("CONTACT");
            Console.WriteLine("Adres: Wijnhaven 60,");
            Console.WriteLine("       3011WM Rotterdam");
            Console.WriteLine("Telefoonnumer: 010-2189034");
            Console.WriteLine("OPENINGSTIJDEN");
            Console.WriteLine("Maandag t/m zondag van 16:00 tot 21:30 uur");
            Console.WriteLine("\nDrup op Esc om terug te gaan");
            input = Console.ReadKey().Key;
            if (input == ConsoleKey.Escape)
            {
                b.SchermKlanten();
            }
        }
    }
}
