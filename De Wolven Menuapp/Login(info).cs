﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Wolven_Menuapp
{
    internal class Loginfo
    {
        public static void Loginfoscherm()
        {
            Console.Clear();
            Console.WriteLine("LOGIN\n\n");
            Console.WriteLine("[1]Login met een bestaand account\n[2]Check uw reservatie zonder account\n");
            Console.WriteLine("Voer 1 of 2 in");
            ConsoleKey LoginType = Console.ReadKey().Key;
            if (LoginType == ConsoleKey.D1)
            {
                LoginAccount();
            };

        }
        public static void LoginAccount()
        {
            Console.Clear();
            Console.WriteLine("Voer uw Gebruikersnaam in:");
            string username = Console.ReadLine();
            Console.WriteLine("Voer uw Wachtwoord in:");
            string password = Console.ReadLine();
            ///Validate(username, password);
        }
        public static void LoginCode()
        {
            Console.Clear();
            Console.WriteLine("Voer hier de code in die u heeft ontvangen:");
            string code = Console.ReadLine();
            ///ValidateCode(code);
        }
    }
}