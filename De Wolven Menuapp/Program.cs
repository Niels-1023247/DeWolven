using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace De_Wolven_Menuapp
{ 

    public class Program
    {
        public static ActiveUser ActiefAccount = new ActiveUser();
        
        public static void Main()
        {

            /*            ActiefAccount.Name = "Freddy";
                        ActiefAccount.IsIngelogd = true;
                        Console.WriteLine(ActiefAccountValues("Name"));
                        Console.WriteLine(LoginCheck());*/
            MenuschermTest.BadInputTest();

        }
        public static string ActiefAccountValues(string keuze)
        {
            if (keuze == "Name")
            {
                return(ActiefAccount.Name);
            }
            else if (keuze == "Username")
            {
                return(ActiefAccount.Username);
            }
            else if (keuze == "Password")
            {
                return (ActiefAccount.Password);
            }
            else if (keuze == "Level")
            {
                return (ActiefAccount.Level);
            }
            return null;
            
        }

        public static bool LoginCheck()
        {
            return ActiefAccount.IsIngelogd;
        }
        public static void SetLoginValues(string name, string username, string password, string email, List<string> code, string level, bool inlogstatus)
        {
            ActiefAccount.Name = name;
            ActiefAccount.Username = username;
            ActiefAccount.Password = password;
            ActiefAccount.Email = email;
            ActiefAccount.Level = level;
            ActiefAccount.Code = code;
            ActiefAccount.Level = level;
            ActiefAccount.IsIngelogd = inlogstatus;
        }


    }


}
