using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Wolven_Menuapp
{
	internal class medewerkerHome
	{
		public static void SchermMedewerker(string melding = "")
		{
			while (true)
			{
				Console.Clear();
				Console.WriteLine("WELKOM MEDEWERKER");
				Console.WriteLine("[1] Mijn opgeslagen reserveringen");
				Console.WriteLine("[2] Nieuwe reservering aanmaken");
				Console.WriteLine("[3] Neem een bestelling op/nieuwe rekening openen");
				Console.WriteLine("[4] Openstaande rekeningen bekijken");
				Console.WriteLine("[5] Contactinformatie aanpassen");
				Console.WriteLine("Voer [1], [2], [3], [4] of [5] in");

				if (melding != "") Console.WriteLine("\n" + melding);

				ConsoleKey optieMedewerker = Console.ReadKey().Key;
				if (optieMedewerker == ConsoleKey.D1)
				{
					Console.Clear();
					Verander.DisplayReserveringen();
				}

				else if (optieMedewerker == ConsoleKey.D2)
				{
					Console.Clear();
					Reservering.AddReservering();
				}

				else if (optieMedewerker == ConsoleKey.D3)
				{
					Bestellingopnemen.nieuweBestelling();
				}

				else if (optieMedewerker == ConsoleKey.D4)
				{
					Rekeningen.rekeningenLijst();
				}

				else if (optieMedewerker == ConsoleKey.D5)
				{
					Contact.ChangeInfoMenu();
				}
				// uitloggen met enter toevoegen !!!
				
			}
		}
	}
}
