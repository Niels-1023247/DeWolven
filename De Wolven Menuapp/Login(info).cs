﻿using System;
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
            
            Console.Clear();
            Console.WriteLine("LOGIN\n\n");

            Console.WriteLine("Hallo " + soortGebruiker + ".\n");

            Console.WriteLine("[1]Login met een bestaand account.\n");
            Console.WriteLine("Druk op 1 om je gegevens in te voeren.!\n");
            Console.WriteLine("Druk op Escape om terug te gaan!\n");

            

            ConsoleKey LoginType = Console.ReadKey().Key;
            while (1 == 1)
            {
                if (LoginType == ConsoleKey.D1)
                {
                    LoginAccount("Klant");
                }
                else if (LoginType == ConsoleKey.D2)
                {
                    CreateAccount();
                    Console.WriteLine("Vul uw gebruikersnaam in, druk daarna op enter en vul uw wachtwoord in: ");
                }
                else if (LoginType == ConsoleKey.Escape)
                {
                    Hoofdmenuscherm.SchermKlanten();
                }
            }
            if (LoginType == ConsoleKey.D1 || LoginType == ConsoleKey.Enter)
            {
                
               LoginAccount(soortGebruiker);
                

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
            Console.WriteLine("Wat is uw naam?");
            string Name=Console.ReadLine();
            Console.WriteLine("Wat is uw E-mail");
            string Mail=Console.ReadLine();
            Console.WriteLine("Kies een Gebruikersnaam");
            string Username = Console.ReadLine();
            Console.WriteLine("Kies een wachtwoord");
            string Password = Console.ReadLine();
            var NewCusAcc = new Account
            {
                Name = Name,
                Email = Mail,
                Password = Password,
                Username = Username,
                Code = new List<string>(),
                Level="1"
            };
            var JsonString = File.ReadAllText("accounts.json");
            var DeserialisedResult = JsonConvert.DeserializeObject<AccountData>(JsonString);
            Console.WriteLine(DeserialisedResult.Accounts[0]);
            var NewAccount = new AccountData { Accounts= new List<Account>(), EmpAcc=DeserialisedResult.EmpAcc};
            for (int i = 0; i < DeserialisedResult.Accounts.Count; i++)
            {
                NewAccount.Accounts.Add(DeserialisedResult.Accounts[i]);
            }
            NewAccount.Accounts.Add(NewCusAcc);
            var WrJsonString = JsonConvert.SerializeObject(NewAccount, Formatting.Indented);
            File.WriteAllText("accounts.json", WrJsonString);
        }
        public static void LoginAccount(string soortGebruiker)
        {
            string dejsontekst = File.ReadAllText("accounts.JSON");
            AccountData alleAccounts = JsonConvert.DeserializeObject<AccountData>(dejsontekst);

            Console.Clear();
            bool inlogStatus = false;
            Console.WriteLine("Voer uw Gebruikersnaam in:");
            string enteredusername = Console.ReadLine();
            Console.WriteLine("Voer uw Wachtwoord in:");
            string enteredpassword = Console.ReadLine();

            // CODE WAT CHECKED OF HET DE ADMIN IS
            
            if(soortGebruiker == "Klant")
            {
                for (int item = 0; item < alleAccounts.Accounts.Count(); item++)
                {
                    //Console.WriteLine(alleAccounts.Accounts[item].Username);

                    if (alleAccounts.Accounts[item].Username == enteredusername && alleAccounts.Accounts[item].Password == enteredpassword)
                    {
                        Console.WriteLine("Ingelogd");
                        inlogStatus = true;

                        if (alleAccounts.Accounts[item].Level == "1") Hoofdmenuscherm.SchermKlanten();
                        
                        break;
                    }
                }
            }
            if(soortGebruiker == "Medewerker")
            {
                if (enteredusername == "admin" && enteredpassword == "feyenoord010") Hoofdmenuscherm.SchermAdmin();
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
            


            if (inlogStatus == false) Reload_back(soortGebruiker,"Verkeerde gegevens.");



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
            string username = Console.ReadLine();
            Console.WriteLine("Voer uw Wachtwoord in:");
            string password = Console.ReadLine();
            ConsoleKey input;

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
