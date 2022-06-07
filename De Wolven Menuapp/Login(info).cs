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

		public static void loginUnitTest() // unit test voor correct inloggen (testgevallen moeten nog worden toegevoegd)
		{
			Console.WriteLine("[UNIT TESTING] INLOGSYSTEEM TESTEN");
			Console.WriteLine(loginTestCase("Klant", "quinten", "frans"));
			Console.WriteLine(loginTestCase("Klant", "kevn", "kevnWW"));
			Console.WriteLine(loginTestCase("Klant", "quinten", "frans"));
			Console.WriteLine(loginTestCase("Klant", "quinten", "frans"));
		}
		public static string loginTestCase(string soortGebruiker, string enteredusername, string enteredpassword) //// EEN ENKEL TESTGEVAL
		{
			string dejsontekst = File.ReadAllText("accounts.JSON");
			AccountData alleAccounts = JsonConvert.DeserializeObject<AccountData>(dejsontekst);
			bool inlogStatus = false;

			Console.WriteLine($"[UNIT TESTING] Testen inloggen met invoer {enteredusername} | {enteredpassword} ...");
			if (soortGebruiker == "Klant")
			{
				for (int item = 0; item < alleAccounts.Accounts.Count(); item++)
				{
					if (alleAccounts.Accounts[item].Username == enteredusername && alleAccounts.Accounts[item].Password == enteredpassword)
					{
						inlogStatus = true;
						return $"[UNIT TESTING] {soortGebruiker} {enteredusername} met wachtwoord {enteredpassword} ingelogd - TEST CASE PASSED";
					}
				}
			}

			if (soortGebruiker == "Medewerker")
			{
				if (enteredusername == "admin" && enteredpassword == "feyenoord010") return $"{soortGebruiker} {enteredusername} with pw {enteredpassword} ingelogd - TEST CASE PASSED";
				else
				{
					for (int item = 0; item < alleAccounts.Accounts.Count(); item++)
					{
						if (alleAccounts.EmpAcc[item].Username == enteredusername && alleAccounts.EmpAcc[item].Password == enteredpassword)
						{
							inlogStatus = true;
							return $"[UNIT TESTING] {soortGebruiker} {enteredusername} met wachtwoord {enteredpassword} ingelogd - TEST CASE PASSED";
						}
					}
				}
			}



			// inlogStatus blijft op false staan wanneer er geen overeenkomend account is gevonden na de controles
			// scherm wordt daarna herladen met Reload_back



			return $"[UNIT TESTING] {soortGebruiker} {enteredusername} met wachtwoord {enteredpassword} niet ingelogd - TEST CASE FAILED";
		}

		public static void CreateAccount()
		{
			// script om invoer te vragen voor nieuw klantaccount
			Console.Clear();
			Console.WriteLine("Wat is uw naam?");
			string Name = Console.ReadLine();
			Console.WriteLine("Wat is uw E-mail");
			string Mail = Console.ReadLine();
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
				Level = "1"
			};
			// lees accounts.json in
			var JsonString = File.ReadAllText("accounts.json");
			var DeserialisedResult = JsonConvert.DeserializeObject<AccountData>(JsonString);
			Console.WriteLine(DeserialisedResult.Accounts[0]);

			// nieuwe lijst maken, alle accounts daarnaar kopiëren en daarna het nieuwe account daar aan toevoegen
			var NewAccount = new AccountData { Accounts = new List<Account>(), EmpAcc = DeserialisedResult.EmpAcc };
			for (int i = 0; i < DeserialisedResult.Accounts.Count; i++)
			{
				NewAccount.Accounts.Add(DeserialisedResult.Accounts[i]);
			}
			NewAccount.Accounts.Add(NewCusAcc);

			// terug naar de json schrijven
			var WrJsonString = JsonConvert.SerializeObject(NewAccount, Formatting.Indented);
			File.WriteAllText("accounts.json", WrJsonString);
		}


		public static void CreateEmployeeAccount()
		{
			// script om invoer te vragen voor nieuw medewerkerAccount
			Console.Clear();
			Console.WriteLine("Wat is de naam van de medewerker?");
			string Name = Console.ReadLine();
			Console.WriteLine("Wat is de E-mail van de medewerker?");
			string Mail = Console.ReadLine();
			Console.WriteLine("Kies een Gebruikersnaam voor de medewerker.");
			string Username = Console.ReadLine();
			Console.WriteLine("Kies een wachtwoord voor de medewerker.");
			string Password = Console.ReadLine();

			// nieuw 'Account' object aanmaken en waarden assignen
			var NewEmpAcc = new Account
			{
				Name = Name,
				Email = Mail,
				Password = Password,
				Username = Username,
				Code = null,
				Level = "2"
			};
			// lees accounts.json in
			var JsonString = File.ReadAllText("accounts.json");
			var DeserialisedResult = JsonConvert.DeserializeObject<AccountData>(JsonString);
			Console.WriteLine(DeserialisedResult.EmpAcc[0]);

			// nieuwe lijst maken, alle accounts daarnaar kopiëren en daarna het nieuwe account daar aan toevoegen
			var NewAccount = new AccountData
			{
				Accounts = DeserialisedResult.Accounts,
				EmpAcc = DeserialisedResult.EmpAcc
			};
			for (int i = 0; i < DeserialisedResult.EmpAcc.Count; i++)
			{
				NewAccount.Accounts.Add(DeserialisedResult.EmpAcc[i]);
			}
			NewAccount.EmpAcc.Add(NewEmpAcc);

			// terug naar de json schrijven
			var WrJsonString = JsonConvert.SerializeObject(NewAccount, Formatting.Indented);
			File.WriteAllText("accounts.json", WrJsonString);
			Console.WriteLine("Medewerker account aangemaakt!");
			Hoofdmenuscherm.SchermAdmin();
		}

		public static void LoginAccount(string soortGebruiker)
		{
			// json inlezen voor later
			string dejsontekst = File.ReadAllText("accounts.JSON");
			AccountData alleAccounts = JsonConvert.DeserializeObject<AccountData>(dejsontekst);

			// script voor invoer login
			Console.Clear();
			bool inlogStatus = false;
			Console.WriteLine($"Welkom {soortGebruiker}");
			Console.WriteLine("Voer uw Gebruikersnaam in:");
			string enteredusername = Console.ReadLine();
			Console.WriteLine("Voer uw Wachtwoord in:");
			string enteredpassword = Console.ReadLine();

			if (soortGebruiker == "Klant")
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

							Hoofdmenuscherm.SchermKlanten();


						}


						break;
					}
				}
			}
			if (soortGebruiker == "Medewerker")
			{   // naar adminscherm sturen wanneer het de admin login is
				if (enteredusername == "admin" && enteredpassword == "feyenoord010") Hoofdmenuscherm.SchermAdmin();

				// elk bestaand medewerkersaccount afgaan en controleren of de login overeenkomt
				else
				{
					for (int item = 0; item < alleAccounts.EmpAcc.Count(); item++)
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
			if (inlogStatus == false) Reload_back(soortGebruiker, "Verkeerde gegevens.");



		}
		// herladen inlogscherm wanneer de inloginfo nergens mee overeenkomt
		public static void Reload_back(string soortGebruiker, string message)
		{
			Console.Clear();
			Console.WriteLine(message + "\n");
			Console.WriteLine("Druk op de escape toets om opnieuw in te loggen.");
			while (true)
			{
				ConsoleKey invoerterug = Console.ReadKey().Key;

				if (invoerterug == ConsoleKey.Escape) // terug naar hoofdmenu
				{
					Loginfo.LoginAccount(soortGebruiker);
					break;
				}
				else // verkeerde input
				{
					Console.WriteLine("Verkeerde toets!");
				}
			}


		}
	}
}