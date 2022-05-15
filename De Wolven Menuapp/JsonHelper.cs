using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace De_Wolven_Menuapp
{
    public class Tafels
    {
        public string[] GebrTafels { get; set; }
    }
    public class InGebruik
    {
        public Dictionary<string,Dictionary<string,Tafels>> Tafel { get; set; }
    }
    public class ContactInfo
    {
        public string Address { get; set; }
        public string Phone { get; set; }
        public OpeningsTijden OpenT { get; set; }
    }
    public class OpeningsTijden
    {
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }
        public string Sunday { get; set; }
    }
    public class Account
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<string> Code { get; set; }
        public string Level { get; set; }
    }
    public class EmpAcc
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Level { get; set; }
    }
    
    public class ActiveUser
    {
        public string Name;
        public string Username;
        public string Password;
        public string Email;
        public List<string> Code;
        public string Level;
        public bool IsIngelogd;
        
    }
    
        public class AccountData
	{
		public List<Account> Accounts { get; set; }
        public List<Account> EmpAcc { get; set; }
	}

    public class Information
    {
        public List<EnkeleReservering> Reserveringen { get; set; }
    }

    public class EnkeleReservering
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int Code { get; set; }
        public string CountofPeople { get; set; }
    }

    public class Menukaart
    {
        public Gerechten[] gerechten;
        public MenuDranken[] Dranken { get; set; }
        public MenuDesserts[] Desserts { get; set; }

    }

    public class Gerechten
    {
        public string Gerechtnaam { get; set; }
        public string Prijs { get; set; }
        public string Allergenen { get; set; }
    }

    public class MenuDranken
    {
        public string Dranknaam { get; set; }
        public string Prijs { get; set; }
        public string Allergenen { get; set; }
    }
    public class MenuDesserts
    {
        public string Dessertnaam { get; set; }
        public string Prijs { get; set; }
        public string Allergenen { get; set; }
    }


}