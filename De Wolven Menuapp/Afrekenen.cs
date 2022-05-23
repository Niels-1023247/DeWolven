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

			// controle of de geselecteerde index niet out of bounds is
			if (geselecteerdeOptie < 1 | geselecteerdeOptie > bestellingenData.Bestellingen.Count-1) geselecteerdeOptie = 1;

			// laat alle rekeningen zien die er op dit moment zijn
			for (int i = 1; i < bestellingenData.Bestellingen.Count; i++) Console.WriteLine((i == geselecteerdeOptie) ? $"> Tafel {0}" : $"- Tafel {0}", bestellingenData.Bestellingen[i].Tafel);

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
		public static void afrekenenBonBekijken(int bestellingIndex = 0)
		{
			// bestellingen inlezen
			var bestellingenJSON = File.ReadAllText("bestellingen.json");
			var bestellingenData = JsonConvert.DeserializeObject<bestellingenRoot>(bestellingenJSON);

			Bestelling bestellingOmAfTeRekenen = bestellingenData.Bestellingen[bestellingIndex];

			// als de gegeven index niet gebruikt kan worden
			if (bestellingIndex == 0 | bestellingIndex > bestellingenData.Bestellingen.Count) afrekenenBestellingenLijst();

			// de bon wordt weergeven
			Console.Clear();
			Console.WriteLine($"AFREKENEN\nDe bon voor tafel {0} wordt weergeven.\n", bestellingOmAfTeRekenen.Tafel);
            Console.WriteLine("GERECHTEN");
			for (int i = 0; i < bestellingOmAfTeRekenen.gerechten.Count; i++) Console.WriteLine($"{bestellingOmAfTeRekenen.gerechten[i].Aantal}x {bestellingOmAfTeRekenen.gerechten[i].Gerechtnaam}");
			for (int i = 0; i < bestellingOmAfTeRekenen.Desserts.Count; i++) Console.WriteLine($"{bestellingOmAfTeRekenen.Desserts[i].Aantal}x {bestellingOmAfTeRekenen.Desserts[i].Dessertnaam}");
			for (int i = 0; i < bestellingOmAfTeRekenen.Dranken.Count; i++) Console.WriteLine($"{bestellingOmAfTeRekenen.Dranken[i].Aantal}x {bestellingOmAfTeRekenen.Dranken[i].Dranknaam}");

		}
	}
}
