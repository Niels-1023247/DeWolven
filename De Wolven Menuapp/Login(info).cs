using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace De_Wolven_Menuapp
{
    public class Loginfo
    {

        
        public static void Loginfoscherm(string soortGebruiker)
        {
            
            Console.Clear();
            Console.WriteLine("LOGIN\n\n");

            Console.WriteLine("Hallo " + soortGebruiker + ".\n");

            Console.WriteLine("[1]Login met een bestaand account.\n[2]Login met uw code.\n");
            Console.WriteLine("Voer 1 of 2 in");

            

            ConsoleKey LoginType = Console.ReadKey().Key;
            if (LoginType == ConsoleKey.D1)
            {
                LoginAccount(soortGebruiker);
            }
            else if (LoginType == ConsoleKey.D2)
            {
                LoginCode(soortGebruiker);
            }
            else if (LoginType == ConsoleKey.Escape)
            {
                if(soortGebruiker == "Klant") Hoofdmenuscherm.SchermKlanten();
                if (soortGebruiker == "Medewerker") Hoofdmenuscherm.SchermMedewerker();
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
        public static void LoginAccount(string soortGebruiker)
        {
            string dejsontekst = File.ReadAllText("accounts.JSON");
            AccountData alleAccounts = JsonConvert.DeserializeObject<AccountData>(dejsontekst);

            Console.Clear();
            Console.WriteLine("Voer uw Gebruikersnaam in:");

            bool inlogStatus = false;

            string enteredusername = Console.ReadLine();
            Console.WriteLine("Voer uw Wachtwoord in:");
            string enteredpassword = Console.ReadLine();

            // CODE WAT CHECKED OF HET DE ADMIN IS

            if (enteredusername == "admin" && enteredpassword == "feyenoord010") Hoofdmenuscherm.SchermAdmin();


            // SCHRIJF HIER CODE OM ACCOUNTS TE VERGELIJKEN UIT DE JSON //

            for (int item = 0; item < alleAccounts.Accounts.Count(); item++)
            {
                //Console.WriteLine(alleAccounts.Accounts[item].Username);

                if (alleAccounts.Accounts[item].Username == enteredusername && alleAccounts.Accounts[item].Password == enteredpassword)
                {
                    Console.WriteLine("Ingelogd");
                    inlogStatus = true;

                    if (alleAccounts.Accounts[item].Level == "1") Hoofdmenuscherm.SchermKlanten();
                    if (alleAccounts.Accounts[item].Level == "2") medewerkerHome.SchermMedewerker();
                    break;
                }
            }


            if (inlogStatus == false) Reload_back(soortGebruiker,"Verkeerde gegevens.");



        }
        public static void LoginCode(string soortGebruiker)
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

                    if (soortGebruiker == "Klant") Hoofdmenuscherm.SchermKlanten();
                    if (soortGebruiker == "Medewerker") Hoofdmenuscherm.SchermMedewerker();

                    break;
                }
            }

            if (inlogStatus == false) Reload_back(soortGebruiker, "Foute code");
            ///ValidateCode(code);
        }

        public static void Reload_back(string soortGebruiker, string message)
        {
            Console.Clear();
            Console.WriteLine(message + "\n");
            Console.WriteLine("Druk op de escape toets om opnieuw in te loggen.");
            ConsoleKey invoerterug = Console.ReadKey().Key;
            while (true)
            {
                if (invoerterug == ConsoleKey.Escape) // terug naar hoofdmenu
                {
                    Loginfo.Loginfoscherm(soortGebruiker);
                    break;
                }
                else 
                {
                    Reload_back(soortGebruiker,"Verkeerde toets!");
                }
            }
        }
    }
}
