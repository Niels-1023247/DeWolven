using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace De_Wolven_Menuapp

{ 
    public class bestellingenRoot
    {
        public List<Bestelling> Bestellingen { get; set; } // root van rekeningen.json, een lijst van rekeningen met elk bestellingen
    }
    public class Bestelling : Menukaart
    {
        public string Tafel { get; set; } // tafel waar de desbetreffende bestelling aan gekoppeld is
    }
    class Datum
    {
        public Dictionary<string, Tijd> Data { get; set; }
    }
    class Tijd
    {
        public Dictionary<string, Tafels> Tijdblok { get; set; }
    }
    class Tafels
    {
        public int BeschTaf6 { get; set; }
        public int BeschTaf4 { get; set; }
        public int BeschTaf2 { get; set; }
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
    public class reserveringenRoot
    {
        public List<EnkeleReservering> Reserveringen { get; set; }
    }
    public class EnkeleReservering
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int Code { get; set; }
        public int CountofPeople { get; set; }
    }
    public class Menukaart
    {
        public List<Gerechten> gerechten { get; set; }
        public List<MenuDranken> Dranken { get; set; }
        public List<MenuDesserts> Desserts { get; set; }

    }
    public class Gerechten
    {
        public string Gerechtnaam { get; set; }
        public float Prijs { get; set; }
        public string Allergenen { get; set; }
        public int Aantal { get; set; } // alleen gebruikt voor bestellingen
    }
    public class MenuDranken
    {
        public string Dranknaam { get; set; }
        public float Prijs { get; set; }
        public string Allergenen { get; set; }
        public int Aantal { get; set; } // alleen gebruikt voor bestellingen
    }
    public class MenuDesserts
    {
        public string Dessertnaam { get; set; }
        public float Prijs { get; set; }
        public string Allergenen { get; set; }
        public int Aantal { get; set; } // alleen gebruikt voor bestellingen
    }

}