using System;
using System.Globalization;
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

		public static void AddReserveringUnitTest()
		{
			Console.WriteLine("[UNIT TESTING] TOEVOEGEN NIEUWE RESERVERINGEN TESTEN");
			Console.WriteLine("[UNIT TESTING] De functie zal door middel van de test cases die in de code gespecificeerd zijn getest worden.");

			AddReservering(true, Tuple.Create<string[], int>(new string[] {"Quinten", "12-juni-2001", "19.00"}, 12));
			AddReservering(true, Tuple.Create<string[], int>(new string[] {"Kevin", "1 januari 2022", "19.00"}, 2));
			AddReservering(true, Tuple.Create<string[], int>(new string[] {"Niels", "3 april 2022", "19.00"}, 4));
			AddReservering(true, Tuple.Create<string[], int>(new string[] {"Frans", "8 mei 2023", "19.00"}, 6));

			var reserveringenJson = File.ReadAllText("reserveringenbestand.json");
			var reserveringenDataBase = JsonConvert.DeserializeObject<reserveringenRoot>(reserveringenJson);

			reserveringenDataBase.Reserveringen.RemoveAt(reserveringenDataBase.Reserveringen.Count - 1);
			reserveringenDataBase.Reserveringen.RemoveAt(reserveringenDataBase.Reserveringen.Count - 1);
			reserveringenDataBase.Reserveringen.RemoveAt(reserveringenDataBase.Reserveringen.Count - 1);
			reserveringenDataBase.Reserveringen.RemoveAt(reserveringenDataBase.Reserveringen.Count - 1);
            Console.WriteLine("[UNIT TESTING] Test reserveringen zijn succesvol verwijderd.");

			var updatedReservations = JsonConvert.SerializeObject(reserveringenDataBase, Formatting.Indented);
			File.WriteAllText("reserveringenbestand.json", updatedReservations);
		}

		public static void AddReservering(bool unitTesting = false, Tuple<string[], int> unitTestCase = null)
		{
			// check for login, zoja vul de naam in
			bool inlogstatus = Program.LoginCheck();

			// nieuwe lege vars voor de nieuwe reservering

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
				Console.WriteLine($"[UNIT TESTING] Testen nieuwe reservering aanmaken met invoer {newName} | {newDate} | {newTime} | {newCountofPeople} ...");
			}
			// script voor gebruiker om nieuwe reservering te maken
			else
			{
				Console.Clear();
				Console.WriteLine("Vul de onderstaande gegevens in om een reservering te maken");
				if (inlogstatus) newName = Program.ActiefAccountValues("Name");
				else
				{
					Console.WriteLine("Op welke naam staat de reservering?\n");
					newName = Console.ReadLine();
				}
				


				Console.WriteLine("Op welke datum is de reservering? \n(Het gewenste formaat is: dag/maand/jaar (voorbeeld: 24/05/2022).)\n");
				string tijdelijkeDatum;
				tijdelijkeDatum = Console.ReadLine();


				// variabel om te checken of de reservering op dezelfde dag valt dat je reserveert. Zodat je bij de tijdsvalidatie een extra check kan uitvoeren.
				bool zelfdeDag = false;

				// functie die bekijkt of de ingevoerde datum wel overeenkomt met het gewenste formaat en of de ingevoerde datum niet eerder is dan de huidige datum.
				bool funcDatumValidatie(string dateToValidate)
				{
					DateTime d;
					bool dateValidatie = DateTime.TryParseExact(
					dateToValidate,
					"dd/MM/yyyy",
					CultureInfo.InvariantCulture,
					DateTimeStyles.None,
					out d);

					try
					{
						// check of de reservering op dezelfde dag valt dat je reserveert. Ik vraag bij de tijds check deze waarde.
						if (DateTime.ParseExact(dateToValidate, "dd/MM/yyyy", CultureInfo.InvariantCulture) == DateTime.Now.Date)
						{
							zelfdeDag = true;
						}
					}
					catch (Exception ex)
					{
						dateValidatie = false;
						return dateValidatie;

					}


					// als de ingevoerde datum eerder is dan de huidige datum dan zet hij de validatie bool op false.
					if (DateTime.ParseExact(dateToValidate, "dd/MM/yyyy", CultureInfo.InvariantCulture) < DateTime.Now.Date)
					{
						dateValidatie = false;
					}

					return dateValidatie;

				}

				// Check of de datum wel voldoet na de Validatie Functie, zo niet vraagt ie om een nieuwe input en doet hij de validatie opnieuw
				bool dateVoldoet = funcDatumValidatie(tijdelijkeDatum);
				while (!dateVoldoet)
				{
					Console.WriteLine("Sorry uw datum ( " + tijdelijkeDatum + " ) voldoet niet aan ons formaat of is al geweest.\n Dit is het gewenste formaat: dag/maand/jaar (voorbeeld: 24/05/2022).\n Voer uw datum nogmaals in.");
					tijdelijkeDatum = Console.ReadLine();
					dateVoldoet = funcDatumValidatie(tijdelijkeDatum);

				}

				newDate = tijdelijkeDatum;

				DateTime weekDag = DateTime.ParseExact(newDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
				string dagVanDeWeek = (weekDag.ToString("dddd",
								  new CultureInfo("nl-NL"))).ToString();



				Console.WriteLine($"Hoelaat is de reservering? (Voer de tijd in volgens het volgende formaat, Vul alleen hele uren in alstublieft: 15:00 )\n");

				string tijdelijkeTijd = Console.ReadLine();


				// valideert of de ingevulde tijd wel klopt qua formaat en als je voor vandaag reserveert dat die tijd niet al geweest is
				bool funcTijdValidatie(string time, string format = "HH:00")
				{
					DateTime outTime;

					// Checkt als je reserveert voor vandaag en checkt dan of die tijd niet al geweest is.



					if (zelfdeDag && (DateTime.ParseExact(time, format, CultureInfo.InvariantCulture, DateTimeStyles.None) < DateTime.Now)) { return false; }

					TimeSpan start = TimeSpan.Parse("12:00"); 
					TimeSpan end = TimeSpan.Parse("22:00"); 
					
					try
                    {
						TimeSpan now = DateTime.ParseExact(time, format, CultureInfo.InvariantCulture, DateTimeStyles.None).TimeOfDay;

						if (start <= end)
						{
							// start and stop times are in the same day
							if (now >= start && now < end)
							{
								// current time is between start and stop

							}
							else
							{
								return false;
							}
						}
						else
						{
							return false;
							// start and stop times are in different days
							if (now >= start || now <= end)
							{
								// current time is between start and stop
							}
						}

						return DateTime.TryParseExact(time, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out outTime);
					}
					
					catch (Exception ex)
					{
						return false;
					}
					
				}

				bool tijdVoldoet = funcTijdValidatie(tijdelijkeTijd);

				// Check of de tijd wel voldoet na de Validatie Functie, zo niet vraagt ie om een nieuwe input en doet hij de validatie opnieuw
				while (!tijdVoldoet)
				{
					Console.WriteLine("Sorry uw tijd ( " + tijdelijkeTijd + " ) voldoet niet aan ons formaat of is al geweest (als u voor vandaag reserveert).\n Dit is het gewenste formaat: 15:00 \n Voer uw tijd nogmaals in.");
					tijdelijkeTijd = Console.ReadLine();
					tijdVoldoet = funcTijdValidatie(tijdelijkeTijd);

				}

				newTime = tijdelijkeTijd;
				//newTime = Console.ReadLine();
				
				EnkeleReservering newReservation;
				// genereer nieuwe code en controleer of deze niet eerder gebruikt is, met dubbeleReserveringsCodeGevonden()
				int newNum = new Random().Next(10000, 99999);
				while (dubbeleReserveringsCodeGevonden(newNum) == true) // als die code al gebruikt is 
				{
					newNum = new Random().Next(10000, 99999);
				}

				if (unitTesting) { Console.WriteLine($"[UNIT TESTING] Gegenereerde code is {newNum}..."); }
				
				Console.WriteLine($"Met hoeveel mensen wilt u komen op {newDate}?");
				newCountofPeople = Convert.ToInt32(Console.ReadLine());

				// maak nieuwe reservering aan met de gegeven informatie
				newReservation = new()
				{
					Name = newName,
					Date = newDate,
					Time = newTime,
					Code = newNum,
					CountofPeople = Convert.ToInt32(newCountofPeople)
				};
				bool IsTable = OurTable.AddTable(newReservation);
				while (!IsTable)
				{
					Console.WriteLine("Er zijn niet genoeg plaatsen over in het restaurant, of u heeft een onjuist getal ingevuld, probeer het nog een keer");
				    newReservation.CountofPeople= Convert.ToInt32(Console.ReadLine());
					IsTable = OurTable.AddTable(newReservation);
				}
				// haal json op en converteer naar c#
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
							Console.WriteLine($"[UNIT TESTING] Reservering gevonden met code {newNum}. Testen of informatie overeen komt met test case...");
							if (newName == reserveringenDataBase.Reserveringen[i].Name &
								newDate == reserveringenDataBase.Reserveringen[i].Date &
								newTime == reserveringenDataBase.Reserveringen[i].Time &
								newCountofPeople == reserveringenDataBase.Reserveringen[i].CountofPeople)
							{
								Console.WriteLine($"[UNIT TESTING] Gevonden informatie: {reserveringenDataBase.Reserveringen[i].Name} | " +
									$"{reserveringenDataBase.Reserveringen[i].Date} | " +
									$"{reserveringenDataBase.Reserveringen[i].Time} | " +
									$"{reserveringenDataBase.Reserveringen[i].CountofPeople}");
								Console.WriteLine("[UNIT TESTING] Succesvol de reservering terug gevonden! TEST CASE PASSED");
								reserveringSuccesvolTeruggevonden = true;
							}
						}
					}
				}
				else
				{

					Console.WriteLine("\nU heeft succesvol de volgende reservering toegevoegd!");
					Console.WriteLine($"Naam: {newName}\nDate: {newDate}\nTime: {newTime}\nAantal plaatsen: {newCountofPeople}");
					Console.WriteLine($"\nUw reserveringscode is: {newNum}\nBewaar deze code goed, hiermee kunt u de reservering aanpassen of verwijderen!");
					Console.WriteLine("Druk op een toets om terug te gaan naar het menu.");
					ConsoleKey eenToets = Console.ReadKey().Key;
					
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
