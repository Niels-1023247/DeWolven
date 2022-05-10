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
        public static void Jsontest()
        {
            var JsonString = File.ReadAllText("accounts.json");
            var DeserialisedResult = JsonConvert.DeserializeObject<AccountData>(JsonString);
            Console.WriteLine(DeserialisedResult.Accounts[1].Username);
            
        }
        
        public static void Loginfoscherm(string soortGebruiker)
        {
            // algemeen inlogscherm laten zien
            Console.Clear();
            Console.WriteLine("LOGIN\n\n");

            Console.WriteLine("Hallo " + soortGebruiker + ".\n");

            Console.WriteLine("[1]Login met een bestaand account.\n");
            Console.WriteLine("Druk op 1 om je gegevens in te voeren.!\n");
            Console.WriteLine("Druk op Escape om terug te gaan!\n");

            
            // druk op 1 om in te loggen
            ConsoleKey LoginType = Console.ReadKey().Key;
            if (LoginType == ConsoleKey.D1 || LoginType == ConsoleKey.Enter)
            {
                
               LoginAccount(soortGebruiker);
                

            }
            // anders terug naar vorig scherm
            else if (LoginType == ConsoleKey.Escape)
            {
                if(soortGebruiker == "Klant") Hoofdmenuscherm.SchermKlanten();
                if (soortGebruiker == "Medewerker") Hoofdmenuscherm.SchermMedewerker();
            }

        }
        public static void CreateAccount()
        {
            // script om invoer te vragen voor nieuw klantaccount
            Console.Clear();
            Console.WriteLine("Wat is uw naam?");
            string Name=Console.ReadLine();
            Console.WriteLine("Wat is uw E-mail");
            string Mail=Console.ReadLine();
            Console.WriteLine("Kies een Gebruikersnaam");
            string Username = Console.ReadLine();
            Console.WriteLine("Kies een wachtwoord");
            string Password = Console.ReadLine();

            // nieuw 'Account' object aanmaken en waarden assignen
            var NewCusAcc = new Account
            {
                Name = Name,
                Email = Mail,
                Password = Password,
                Username = Username,
                Code = new List<string>(),
                Level="1"
            };
            // lees accounts.json in
            var JsonString = File.ReadAllText("accounts.json");
            var DeserialisedResult = JsonConvert.DeserializeObject<AccountData>(JsonString);
            Console.WriteLine(DeserialisedResult.Accounts[0]);

            // nieuwe lijst maken, alle accounts daarnaar kopiëren en daarna het nieuwe account daar aan toevoegen
            var NewAccount = new AccountData { Accounts= new List<Account>(), EmpAcc=DeserialisedResult.EmpAcc};
            for (int i = 0; i < DeserialisedResult.Accounts.Count; i++)
            {
                NewAccount.Accounts.Add(DeserialisedResult.Accounts[i]);
            }
            NewAccount.Accounts.Add(NewCusAcc);
            
            // terug naar de json schrijven
            var WrJsonString = JsonConvert.SerializeObject(NewAccount, Formatting.Indented);
            File.WriteAllText("accounts.json", WrJsonString);
        }
        public static void LoginAccount(string soortGebruiker)
        {
            // json inlezen voor later
            string dejsontekst = File.ReadAllText("accounts.JSON");
            AccountData alleAccounts = JsonConvert.DeserializeObject<AccountData>(dejsontekst);

            // script voor invoer login
            Console.Clear();
            bool inlogStatus = false;
            Console.WriteLine("Voer uw Gebruikersnaam in:");
            string enteredusername = Console.ReadLine();
            Console.WriteLine("Voer uw Wachtwoord in:");
            string enteredpassword = Console.ReadLine();
            
            if(soortGebruiker == "Klant")
            {
                // elk bestaand klantaccount afgaan en controleren of de login overeenkomt
                for (int item = 0; item < alleAccounts.Accounts.Count(); item++)
                {

                    if (alleAccounts.Accounts[item].Username == enteredusername && alleAccounts.Accounts[item].Password == enteredpassword)
                    {
                        
                        inlogStatus = true;

                        if (alleAccounts.Accounts[item].Level == "1")
                        {
                            Program.SetLoginValues(alleAccounts.Accounts[item].Username, alleAccounts.Accounts[item].Name, alleAccounts.Accounts[item].Password, 
                                alleAccounts.Accounts[item].Email, alleAccounts.Accounts[item].Code, alleAccounts.Accounts[item].Level, true);
                            Console.Clear();
                            Console.WriteLine("Gebruiker: " + alleAccounts.Accounts[item].Name);
                            Console.WriteLine("U bent ingelogd!");
                            Console.WriteLine("Druk op ENTER om verder te gaan.");
                            ConsoleKey invoerterug = Console.ReadKey().Key;
                            while (true)
                            {
                                if (invoerterug == ConsoleKey.Enter) // terug naar klanten menu
                                {
                                    Hoofdmenuscherm.SchermKlanten();
                                }
                                
                            }

                            
                        }
                            
                        
                        break;
                    }
                }
            }
            if(soortGebruiker == "Medewerker")
            {   // naar adminscherm sturen wanneer het de admin login is
                if (enteredusername == "admin" && enteredpassword == "feyenoord010") Hoofdmenuscherm.SchermAdmin();

                // elk bestaand medewerkersaccount afgaan en controleren of de login overeenkomt
                else
                {
                    for (int item = 0; item < alleAccounts.Accounts.Count(); item++)
                    {
                        if (alleAccounts.EmpAcc[item].Username == enteredusername && alleAccounts.EmpAcc[item].Password == enteredpassword)
                        {
                            Console.WriteLine("Ingelogd");
                            inlogStatus = true;

                            if (alleAccounts.EmpAcc[item].Level == "2") medewerkerHome.SchermMedewerker();
                            break;
                        }
                    }
                }
            }
            

            // inlogStatus blijft op false staan wanneer er geen overeenkomend account is gevonden na de controles
            // scherm wordt daarna herladen met Reload_back
            if (inlogStatus == false) Reload_back(soortGebruiker,"Verkeerde gegevens.");



        }
        // herladen inlogscherm wanneer de inloginfo nergens mee overeenkomt
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
                else // verkeerde input
                {
                    Reload_back(soortGebruiker,"Verkeerde toets!");
                }
            }
            

            //for (int i = 0; i != account.Username.length; i++)
            //{
            //    // vul hier het stukje wat het account uitleest inplaats van wat de gerechten uitleest
            //    if (username == account[i].Username & password == account[i].Password)
            //    {
            //        Console.WriteLine("Je bent ingelogd!");
            //        input = Console.ReadKey().Key;
            //    }
            //    else
            //    {
            //        Console.WriteLine("Sorry je Geberuikersnaam/ Wachtwoord combinatie klopt niet!");
            //        Console.WriteLine("Druk op Enter om je wachtwoord opnieuw in te voeren, of Esc om terug te gaan.");
            //        input = Console.ReadKey().Key;
            //        while (1 == 1)
            //        {
            //            if (input == ConsoleKey.Enter) // reset
            //            {
            //                Hoofdmenuscherm.SchermKlanten();
            //                break;
            //            }
            //            if (input == ConsoleKey.Escape) // terug naar hoofdmenu
            //            {
            //                Loginfo.Loginfoscherm();
            //                break;
            //            }
            //        }
            //    }
            //}
        }
        //public static void LoginCode()
        //{
        //    Console.Clear();
        //    Console.WriteLine("Voer hier de code in die u heeft ontvangen:");
        //    string code = Console.ReadLine();
        //    ///ValidateCode(code);
        //    
        //    // vul hier het stukje wat het account uitleest inplaats van wat de gerechten uitleest
        //    if (code == hetgehelemenu.account[f].code)
        //    {
        //        Console.WriteLine("Je bent ingelogd!");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Sorry je Code klopt niet!");
        //        
        //
        //    }
        //
        //}
    }
}
