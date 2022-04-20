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
            Console.WriteLine("[1]Login met een bestaand account.\n[2]Check uw reservatie met een code.\n");
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
        public static void LoginAccount()
        {
            string dejsontekst = File.ReadAllText("accounts.JSON");
            AccountData alleAccounts = JsonConvert.DeserializeObject<AccountData>(dejsontekst);

            Console.Clear();
            Console.WriteLine("Voer uw Gebruikersnaam in:");

            bool inlogStatus = false;

            string enteredusername = Console.ReadLine();
            Console.WriteLine("Voer uw Wachtwoord in:");
            string enteredpassword = Console.ReadLine();

            // SCHRIJF HIER CODE OM ACCOUNTS TE VERGELIJKEN UIT DE JSON //
            
            for (int item = 0; item < alleAccounts.Accounts.Count(); item++)
            {
                //Console.WriteLine(alleAccounts.Accounts[item].Username);

                if (alleAccounts.Accounts[item].Username == enteredusername && alleAccounts.Accounts[item].Password == enteredpassword)
                {
                    Console.WriteLine("Ingelogd");
                    inlogStatus = true;
                    
                    break;
                }
            }


            if (inlogStatus == false) Reload_back("Verkeerde gegevens.");



        }
        public static void LoginCode()
        {
            string dejsontekst = File.ReadAllText("accounts.JSON");
            AccountData alleAccounts = JsonConvert.DeserializeObject<AccountData>(dejsontekst);

            bool inlogStatus = false;

            Console.Clear();
            Console.WriteLine("Voer hier de code in die u heeft ontvangen:");
            string enteredcode = Console.ReadLine();

            for (int item = 0; item < alleAccounts.Accounts.Count(); item++)
            {
                //Console.WriteLine(alleAccounts.Accounts[item].Username);

                if (alleAccounts.Accounts[item].Code == enteredcode)
                {
                    Console.WriteLine("Ingelogd");
                    inlogStatus = true;

                    break;
                }
            }

            if (inlogStatus == false) Reload_back("Foute code");
            ///ValidateCode(code);
        }

        public static void Reload_back(string message)
        {
            Console.Clear();
            Console.WriteLine(message + "\n");
            Console.WriteLine("Druk op de escape toets om opnieuw in te loggen.");
            ConsoleKey invoerterug = Console.ReadKey().Key;
            while (true)
            {
                if (invoerterug == ConsoleKey.Escape) // terug naar hoofdmenu
                {
                    Loginfo.Loginfoscherm();
                    break;
                }
                else 
                {
                    Reload_back("Verkeerde toets!");
                }
            }
        }
    }
}
