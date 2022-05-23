using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace De_Wolven_Menuapp
{
	public class Afrekenen
	{
		public static void afrekenenBestellingenLijst(int geselecteerdeOptie = 1) // geselecteerdeOptie is voor welke tafel je geselecteerd hebt, start bij 1
		{
			// script voor console
			Console.Clear();
			Console.WriteLine("AFREKENEN TAFEL");
			Console.WriteLine("Alle openstaande rekeningen worden per tafel weergeven.");
			Console.WriteLine("Selecteer met de pijltjestoetsen ^ v welke u af wilt rekenen en druk op Enter om de bon te zien.");
			Console.WriteLine("Of druk op Escape om terug te gaan naar het hoofdmenu.\n");

			// bestellingen inlezen
			var bestellingenJSON = File.ReadAllText("bestellingen.json");
			var bestellingenData = JsonConvert.DeserializeObject<bestellingenRoot>(bestellingenJSON);

			// if true dan {}
			if (true) { };

			// controle of de geselecteerde index niet out of bounds is
			if (geselecteerdeOptie < 1 | geselecteerdeOptie > bestellingenData.Bestellingen.Count-1) geselecteerdeOptie = 1;

			// laat alle rekeningen zien die er op dit moment zijn
			for (int i = 1; i < bestellingenData.Bestellingen.Count; i++) Console.WriteLine((i == geselecteerdeOptie) ? "> Tafel {0}" : "- Tafel {0}", bestellingenData.Bestellingen[i].Tafel);

			// user input voor selecteren en menu's navigeren
			ConsoleKey toets = Console.ReadKey().Key;
			if (toets == ConsoleKey.DownArrow | toets == ConsoleKey.RightArrow) afrekenenBestellingenLijst(geselecteerdeOptie + 1);
			else if (toets == ConsoleKey.UpArrow | toets == ConsoleKey.LeftArrow) afrekenenBestellingenLijst(geselecteerdeOptie - 1);

			// naar de bon toe van de geselecteerde bestelling
			else if (toets == ConsoleKey.Enter) afrekenenBonBekijken(geselecteerdeOptie);

			// terug naar het vorige menu
			else if (toets == ConsoleKey.Escape) medewerkerHome.SchermMedewerker();

			// bij elke andere invoer herlaadt het scherm
			else afrekenenBestellingenLijst(geselecteerdeOptie);
		}
		public static void afrekenenBonBekijken(int rekeningIndex = 1)
		{
			// bestellingen inlezen
			var bestellingenJSON = File.ReadAllText("bestellingen.json");
			var bestellingenData = JsonConvert.DeserializeObject<bestellingenRoot>(bestellingenJSON);

			var rekeningOmAfTeRekenen = bestellingenData.Bestellingen[rekeningIndex];
			float totaalBedrag = 0f;

			// als de gegeven index niet gebruikt kan worden
			if (rekeningIndex <= 0 | rekeningIndex > bestellingenData.Bestellingen.Count) afrekenenBestellingenLijst();

			// de bon wordt weergeven
			Console.Clear();
			Console.WriteLine("AFREKENEN\nDe rekening voor tafel {0} wordt weergeven.\n", rekeningOmAfTeRekenen.Tafel);
			Console.WriteLine("GERECHTEN");

			for (int i = 0; i < rekeningOmAfTeRekenen.gerechten.Count; i++)
			{
				Console.WriteLine($"{rekeningOmAfTeRekenen.gerechten[i].Aantal}x {rekeningOmAfTeRekenen.gerechten[i].Gerechtnaam} - {rekeningOmAfTeRekenen.gerechten[i].Aantal * rekeningOmAfTeRekenen.gerechten[i].Prijs} euro");
				totaalBedrag += rekeningOmAfTeRekenen.gerechten[i].Aantal * rekeningOmAfTeRekenen.gerechten[i].Prijs;
			}			
			Console.WriteLine("\nDRANKEN");
			for (int i = 0; i < rekeningOmAfTeRekenen.Desserts.Count; i++)
			{
				Console.WriteLine($"{rekeningOmAfTeRekenen.Desserts[i].Aantal}x {rekeningOmAfTeRekenen.Desserts[i].Dessertnaam} - {rekeningOmAfTeRekenen.Desserts[i].Aantal * rekeningOmAfTeRekenen.Desserts[i].Prijs} euro");
				totaalBedrag += rekeningOmAfTeRekenen.Desserts[i].Aantal * rekeningOmAfTeRekenen.Desserts[i].Prijs;
			}			
			Console.WriteLine("\nDESSERTS");
			for (int i = 0; i < rekeningOmAfTeRekenen.Dranken.Count; i++)
			{
				Console.WriteLine($"{rekeningOmAfTeRekenen.Dranken[i].Aantal}x {rekeningOmAfTeRekenen.Dranken[i].Dranknaam} - {rekeningOmAfTeRekenen.Dranken[i].Aantal * rekeningOmAfTeRekenen.Dranken[i].Prijs} euro");
				totaalBedrag += rekeningOmAfTeRekenen.Dranken[i].Aantal * rekeningOmAfTeRekenen.Dranken[i].Prijs;
			}


			// totaal bedrag laten zien
			Console.WriteLine($"\nTotaal: {totaalBedrag} euro");
            Console.WriteLine("Druk op Enter om af te rekenen en deze rekening uit het rekeningboek te halen.");
            Console.WriteLine("Druk op Escape om terug te gaan naar het rekeningoverzicht.");

			// input verwerken
			ConsoleKey option = Console.ReadKey().Key;

			if (option == ConsoleKey.Enter) // bevestigen afrekenen rekening
            {
				// console script
				Console.Clear();
                Console.WriteLine("REKENING BETAALD!");
				Console.WriteLine(rekeningCheck(rekeningOmAfTeRekenen));
                Console.WriteLine($"U heeft de rekening voor tafel {rekeningOmAfTeRekenen.Tafel} afgerekend.");
                Console.WriteLine($"Het totaalbedrag van deze rekening was {totaalBedrag} euro.");
				Console.WriteLine("Druk op een toets om terug te gaan naar het medewerkersmenu.");

				// verwijder bestelling uit systeem, en update de database
				bestellingenData.Bestellingen.RemoveAt(rekeningIndex);
				var geupdateBestellingen = JsonConvert.SerializeObject(bestellingenData, Formatting.Indented);
				File.WriteAllText("bestellingen.json", geupdateBestellingen);
				ConsoleKey cont = Console.ReadKey().Key;
				medewerkerHome.SchermMedewerker();

				// RESERVERING <---- NIET VERGETEN UIT HET SYSTEEM HALEN !!! TOEVOEGEN
			}
			else if (option == ConsoleKey.Escape)
            {
				Console.Clear();
				afrekenenBestellingenLijst(rekeningIndex);
			}
		}
		public static string rekeningCheck(Bestelling bes)
        {
			return bes.Dranken[0].Aantal == 420 && bes.Dranken[0].Dranknaam == "Mystery Cocktail..." ? "Nice.\n" : "";
        }
	}
}
