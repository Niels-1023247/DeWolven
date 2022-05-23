using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace De_Wolven_Menuapp
{
	internal class Reservering
	{
		// beschikbaarheid tafel controleren, moet nog gemaakt worden
		public static void Availability_Check() 
		{
			int x = 1;
			if (x == 1)
			{
				Console.WriteLine("ökay then");
			}
		}

		public static bool dubbeleReserveringsCodeGevonden(int nieuweCode) // return true of false
		{
			// reserveringen ophalen
			var reserveringJson = File.ReadAllText("reserveringenbestand.json");
			reserveringenRoot reserveringsData = JsonConvert.DeserializeObject<reserveringenRoot>(reserveringJson);

			// ga elke reservering af
			for (int i = 0; i < reserveringsData.Reserveringen.Count; i++) 
			{
				if (reserveringsData.Reserveringen[i].Code == nieuweCode) // komt de nieuwe code overeen? return true
				{
					return true;
				}
			}
			return false; // anders returnt hij false
		}

		/*public static Information jsonNaarObject(string fileName, class jsonStructuur)
		{
			string opgehaaldeTekst = File.ReadAllText(fileName);

		}
		*/
		public static void AddReserveringUnitTest()
        {
            Console.WriteLine("[UNIT TESTING] Het toevoegen van nieuwe reserveringen wordt nu getest.");
            Console.WriteLine("[UNIT TESTING] De functie zal door middel van de test cases die in de code gespecificeerd zijn getest worden.");


        }

		public static void AddReservering(bool unitTesting = false, Tuple<string[], int> unitTestCase = null)
		{
			// check for login, zoja vul de naam in
			bool inlogstatus = Program.LoginCheck();

			// nieuwe lege vars voor de nieuwe reservering
			Console.Clear();
			string newName;
			string newDate;
			string newTime;
			int newCountofPeople;

			// nieuwe var voor de json
			string reserveringenJson;
			reserveringenRoot reserveringenDataBase;

			// wordt er getest? dan worden de values van de unit test ingevuld
			if (unitTesting & unitTestCase != null)
			{
				newName = unitTestCase.Item1[0];
				newDate = unitTestCase.Item1[1];
				newTime = unitTestCase.Item1[2];
				newCountofPeople = unitTestCase.Item2;
                Console.WriteLine($"[UNIT TESTING] Testen nieuwe reservering aanmaken met invoer {newName} | {newDate} | {newTime} | {newCountofPeople}...");
			}
			// script voor gebruiker om nieuwe reservering te maken
			else 
			{
				Console.WriteLine("Vul de onderstaande gegevens in om een reservering te maken");
				if (inlogstatus) newName = Program.ActiefAccountValues("Name");
				else
				{
					Console.WriteLine("Op welke naam staat de reservering?\n");
					newName = Console.ReadLine();
				}
				Console.WriteLine("Op welke datum is de reservering?\n");
				newDate = Console.ReadLine();

				Console.WriteLine($"Hoelaat is de reservering?\n");
				newTime = Console.ReadLine();

				Console.WriteLine($"Met hoeveel mensen wilt u komen op {newDate}?");
				newCountofPeople = Convert.ToInt32(Console.ReadLine());
			}

			// genereer nieuwe code en controleer of deze niet eerder gebruikt is, met dubbeleReserveringsCodeGevonden()
			int newNum = new Random().Next(10000, 99999);
			while (dubbeleReserveringsCodeGevonden(newNum) == true) // als die code al gebruikt is 
			{
				newNum = new Random().Next(10000, 99999);
			}

            if (unitTesting) Console.WriteLine($"[UNIT TESTING] Gegenereerde code is {newNum}...");

			// maak nieuwe reservering aan met de gegeven informatie
			EnkeleReservering newReservation = new()
				{
					Name = newName,
					Date = newDate,
					Time = newTime,
					Code = newNum,
					CountofPeople = newCountofPeople
				};

			// haal json op en voeg reservering toe, zet daarna terug in json
			reserveringenJson = File.ReadAllText("reserveringenbestand.json");
			reserveringenDataBase = JsonConvert.DeserializeObject<reserveringenRoot>(reserveringenJson);

			// voeg reservering toe aan de lijst van reserveringen, en schrijf geüpdate lijst terug naar json
			reserveringenDataBase.Reserveringen.Add(newReservation);
			var updatedReservations = JsonConvert.SerializeObject(reserveringenDataBase, Formatting.Indented);
			File.WriteAllText("reserveringenbestand.json", updatedReservations);



			// terug naar juiste menu
			if (unitTesting)
			{
				reserveringenJson = File.ReadAllText("reserveringenbestand.json");
				reserveringenDataBase = JsonConvert.DeserializeObject<reserveringenRoot>(reserveringenJson);
                Console.WriteLine("[UNIT TESTING] Reservering proberen terug te vinden in JSON...");
				bool reserveringSuccesvolTeruggevonden = false;
				for (int i = 0; i < reserveringenDataBase.Reserveringen.Count; i++)
                {
					if (reserveringenDataBase.Reserveringen[i].Code == newNum)
                    {
						Console.WriteLine($"[UNIT TESTING] Reservering gevonden met code {newNum}. Testen of informatie overeen komt met invoer...");
						if (newName == reserveringenDataBase.Reserveringen[i].Name &
							newDate == reserveringenDataBase.Reserveringen[i].Date &
							newTime == reserveringenDataBase.Reserveringen[i].Time &
							newCountofPeople == reserveringenDataBase.Reserveringen[i].CountofPeople)
                        {
                            Console.WriteLine("[UNIT TESTING] Succesvol de reservering terug gevonden! TEST CASE PASSED");
							reserveringSuccesvolTeruggevonden = true;
                        }
                    }
                }
                Console.WriteLine("");
			}
			else
			{
				ConsoleKey eenToets = Console.ReadKey().Key;
				Console.WriteLine("U heeft succesvol de volgende reservering toegevoegd!");
				Console.WriteLine($"Naam: {newName}\nDate: {newDate}\nTime: {newTime}\nAantal plaatsen: {newCountofPeople}");
				Console.WriteLine($"\nUw reserveringscode is: {newNum}\nBewaar deze code goed, hiermee kunt u de reservering aanpassen of verwijderen!");
				Console.WriteLine("Druk op een toets om terug te gaan naar het menu.");
				if (Program.ActiefAccountValues("Level") == "Klant") Hoofdmenuscherm.SchermKlanten();
				else Hoofdmenuscherm.SchermMedewerker();
			}





				//DateTime d1 = DateTime.Now; // datum nu
				//DateTime d2 = Convert.ToDateTime(datum); // datum die ingevoerd is

				//while (DateTime.Compare(d1, d2) == -1) // voer datum opnieuw in als ie fout is
				//{
				//	Console.WriteLine("Datum is verkeerd");
				// Console.WriteLine("Voer een nieuwe datum in: ");
				//	datum = Console.ReadLine();
				//}
				//Console.WriteLine("hij is door alle controles heeeen gekomen!!!!");

				//bool datumklopt = false; // nieuwe boolean, die false is als de datum niet klopt
				//while (datumklopt == false)
				//{ // hij gaat alleen de loop in als de datum niet klopt, of als hij voor het eerst vraagt om een datum
				//	datumklopt = true; // we gaan er van uit dat de datum klopt, en we verzetten hem naar false als ie niet door een van de controle komt
				//	Console.WriteLine("Kies datum DD-MM (bijv. 19/04/2022):");
				//	datum = Console.ReadLine();
				//	if (DateTime.Compare(d1, d2) < 0)
				//	{
				//		datumklopt = false; // hij gaat naar false als de datum voor vandaag is
				//		Console.WriteLine("Datum is vandaag of al geweest, voer opnieuw in");
				//	}
				//}

		}
		public static int Compare(DateTime d1, DateTime d2)
		{

			if (d1 == d2)
			{
				return 0;
				
			}

			else if (d1 < d2)
			{
				return 1;
				
			}

			else if (d1 > d2)
			{
				return -1;
			}

			else
			{
				return 2;
			}

		}
		public static void LinkResv()
		{

		}
	}
}
