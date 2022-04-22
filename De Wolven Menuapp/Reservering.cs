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
		
		public static void Availability_Check() // beschikbaarheid tafel controleren, moet nog gemaakt worden.
		{
			int x = 1;
			if (x == 1)
			{
				Console.WriteLine("ökay then");
			}
		}
		public static void Klantinfo()
		{
			Console.Clear();

			Console.WriteLine("Vul de onderstaande gegevens in om een reservering te maken");

			Console.WriteLine("Voer uw naam in:");
			string newName = Console.ReadLine();

			Console.WriteLine("Vul de datum in dat u wilt komen in de vorm dd/MM/yyyy");
			string newDate = Console.ReadLine();

			Console.WriteLine($"Hoelaat wilt u komen op {newDate}?");
			string newTime = Console.ReadLine();

			Console.WriteLine($"Met hoeveel mensen wilt u komen op {newDate}");

			string newCountofPeople = Console.ReadLine();
			int num = new Random().Next(10000, 99999);
			
            {

            }

			EnkeleReservering newReservation = new()
				{
					Name = newName,
					Date = newDate,
					Time = newTime,
					Code = num,
					CountofPeople = newCountofPeople
				};



			// als er een nieuwe reservering wordt toegevoegd, DAN pas haalt ie de json van reserveringenbestand op en zet ie em er bij
			var reserveringJson = File.ReadAllText("reserveringenbestand.json");
			Information reserveringsData = JsonConvert.DeserializeObject<Information>(reserveringJson);
			reserveringsData.Reserveringen.Add(newReservation);
			var updatedReservations = JsonConvert.SerializeObject(reserveringsData, Formatting.Indented);
			File.WriteAllText("reserveringenbestand.json", updatedReservations);
            Console.WriteLine(updatedReservations);

			Console.ReadLine();
			Console.WriteLine(reserveringsData);



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
	}
}
