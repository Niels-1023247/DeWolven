using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Wolven_Menuapp
{
	internal class Reservering
	{

		public static void Availability_Check()
		{
			int x = 1;
			if (x ==
				1)
			{
				Console.WriteLine("ökay then");
			}
		}
		public static void Klantinfo()
		{
			Console.Clear();
			string vandaag = DateTime.Now.ToString("dd/MM/yyyy");
			char streepje = '-';
			Console.WriteLine("Vul de onderstaande gegevens in om een reservering te maken");
			Console.WriteLine("Voer uw naam in:");
			string naam = Console.ReadLine();
			Console.WriteLine("Vul de datum in dat u wilt komen in de vorm dd/MM/yyyy");
			string datum = Console.ReadLine();

			DateTime d1 = DateTime.Now;
			DateTime d2 = Convert.ToDateTime(datum);
		}
		public static int Compare(DateTime d1, DateTime d2)
		{

			if (d1 == d2)
			{
				return 0;
			}

			else if (d1 < d2)
			{
				return -1;
			}

			else if (d1 > d2)
			{
				return 1;
			}

			else
			{
				return 2;
			}


		}







		//	bool datumklopt = false; // nieuwe boolean, die false is als de datum niet klopt
		//	while (datumklopt == false) { // hij gaat alleen de loop in als de datum niet klopt, of als hij voor het eerst vraagt om een datum
		//		datumklopt = true; // we gaan er van uit dat de datum klopt, en we verzetten hem naar false als ie niet door een van de controle komt
		//		Console.WriteLine("Kies datum DD-MM (bijv. 19/04/2022):");
		//		datum = Console.ReadLine();
		//		if (IsDateBeforeOrToday(datum) == false) {
		//			datumklopt = false; // hij gaat naar false als de datum voor vandaag is
		//                  Console.WriteLine("Datum is vandaag of al geweest, voer opnieuw in");
		//		}


		//	}
		//          Console.WriteLine("hij is door alle controles heeeen gekomen!!!!");





		//	//while (datum[2] != streepje && datum.Length != 10)
		//	//{
		//	//	Console.WriteLine("Schrijf de datum in vorm DD/MM/yyyy");
		//	//	Console.WriteLine("Schrijf de datum dat u wilt komen in de vorm DD/MM/yyyy");
		//	//	datum = Console.ReadLine();

		//	//}


		//	//Console.WriteLine($"Met hoeveel mensen wilt u komen op {datum}");
		//	//string aantal_mensen = Console.ReadLine();


	}
}
