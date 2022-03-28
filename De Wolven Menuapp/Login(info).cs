using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
            }
            else if (LoginType == ConsoleKey.D2)
            {
                LoginCode();
            }
            else if (LoginType == ConsoleKey.Escape)
            {
                Hoofdmenuscherm.SchermKlanten();
            }

        }
        public static void CreateAccount()
        {
            Console.Clear();
            Account newacc = new Account();

            newacc.Id = 1;
            Console.WriteLine("Voer uw Gebruikersnaam in:");
            newacc.Username = Console.ReadLine();
            Console.WriteLine("Voer uw Wachtwoord in:");
            newacc.Password = Console.ReadLine();
            string jsonData = JsonConvert.SerializeObject(newacc);
            Console.WriteLine(jsonData);
            string path = @"D:\DeWolven\De Wolven Menuapp\accounts.json";
            // append

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
