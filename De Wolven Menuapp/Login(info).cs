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
            Console.WriteLine("[1]Login met een bestaand account\n[2]Creeër nieuw account\n");
            Console.WriteLine("Voer 1 of 2 in");
            Account acc = new Account();
            ConsoleKey LoginType = Console.ReadKey().Key;
            while (1 == 1)
            {
                if (LoginType == ConsoleKey.D1)
                {
                    LoginAccount();
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

        }
        public static void CreateAccount()
        {

            Console.WriteLine($"\n");
            var newacc = new List<Account>
            {
                new Account
                {
                    Username = Console.ReadLine(),
                    Password = Console.ReadLine(),
                }
            };
            var newAccJson = JsonConvert.SerializeObject(newacc);
            Console.WriteLine(newAccJson);
            Console.ReadKey();
            File.WriteAllText(@"accounts.json", newAccJson);
            Console.WriteLine("Signed up");
        }
        public static void LoginAccount()
        {
            Console.Clear();
            Console.WriteLine("Voer uw Gebruikersnaam in:");
            
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
