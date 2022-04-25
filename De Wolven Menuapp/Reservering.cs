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

		public static bool dubbeleReserveringsCodeGevonden(int nieuweCode) // returnt true of false
        {
			// reserveringen ophalen
			var reserveringJson = File.ReadAllText("reserveringenbestand.json"); 
			Information reserveringsData = JsonConvert.DeserializeObject<Information>(reserveringJson);

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

		public static void AddReservering()
		{
			Console.Clear();
			string newName;
			string newDate;
			string newTime;
			string newCountofPeople;
			
			// nieuwe var voor de json
			string reserveringJson;
			Information reserveringsData;

			// script voor gebruiker om nieuwe reservering te maken
			Console.WriteLine("Vul de onderstaande gegevens in om een reservering te maken");

			Console.WriteLine("Op welke naam is de reservering?\n");
			newName = Console.ReadLine();

			Console.WriteLine("Op welke datum is de reservering?\n");
			newDate = Console.ReadLine();

			Console.WriteLine($"Hoelaat is de reservering?\n");
			newTime = Console.ReadLine();

			Console.WriteLine($"Met hoeveel mensen wilt u komen op {newDate}");
			newCountofPeople = Console.ReadLine();

			// genereer nieuwe code en controleer of deze niet eerder gebruikt is, met dubbeleReserveringsCodeGevonden()
			int newNum = new Random().Next(10000, 99999);
			while (dubbeleReserveringsCodeGevonden(newNum) == true) // als die code al gebruikt is 
            {
				newNum = new Random().Next(10000, 99999);
			}

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
			reserveringJson = File.ReadAllText("reserveringenbestand.json");
			reserveringsData = JsonConvert.DeserializeObject<Information>(reserveringJson);

			// voeg reservering toe aan de lijst van reserveringen, en schrijf geüpdate lijst terug naar json
			reserveringsData.Reserveringen.Add(newReservation);
			var updatedReservations = JsonConvert.SerializeObject(reserveringsData, Formatting.Indented);
			File.WriteAllText("reserveringenbestand.json", updatedReservations);
            Console.WriteLine(updatedReservations);


			//DateTime d1 = DateTime.Now; // datum nu
			//DateTime d2 = Convert.ToDateTime(datum); // datum die ingevoerd is

			//while (DateTime.Compare(d1, d2) == -1) // voer datum opnieuw in als ie fout is
			//{
			//	Console.WriteLine("Datum is verkeerd");
   //             Console.WriteLine("Voer een nieuwe datum in: ");
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
