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
		public static void afrekenenBestellingenLijst(int geselecteerdeOptie = 0) // geselecteerdeOptie is voor welke tafel je geselecteerd hebt, start bij 0
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
			if (geselecteerdeOptie < 0 | geselecteerdeOptie > bestellingenData.Bestellingen.Count-1) geselecteerdeOptie = 0;

			// laat alle rekeningen zien die er op dit moment zijn
			for (int i = 0; i < bestellingenData.Bestellingen.Count; i++) Console.WriteLine((i == geselecteerdeOptie) ? "> Tafel {0}" : "- Tafel {0}", bestellingenData.Bestellingen[i].Tafel);

			// user input voor selecteren en menu's navigeren
			ConsoleKey toets = Console.ReadKey().Key;
			if (toets == ConsoleKey.DownArrow | toets == ConsoleKey.RightArrow) afrekenenBestellingenLijst(geselecteerdeOptie + 1);
			else if (toets == ConsoleKey.UpArrow | toets == ConsoleKey.LeftArrow) afrekenenBestellingenLijst(geselecteerdeOptie - 1);
			else if (toets == ConsoleKey.Enter) { } // pass
			else if (toets == ConsoleKey.Escape) medewerkerHome.SchermMedewerker();
			else afrekenenBestellingenLijst(geselecteerdeOptie);
		}
	}
}
