using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace De_Wolven_Menuapp
{
	public class Rekeningen
	{
		public static void rekeningenLijst(int geselecteerdeOptie = 1) // geselecteerdeOptie is voor welke tafel je geselecteerd hebt, start bij 1 (default is 1)
		{
			// script voor console
			Console.Clear();
			Console.WriteLine("OPENSTAANDE REKENINGEN");
			Console.WriteLine("Alle openstaande rekeningen worden per tafel weergeven.");
			Console.WriteLine("Selecteer met de pijltjestoetsen [^] [v] om een bon te bekijken en druk op Enter voor meer opties.");
			Console.WriteLine("Of druk op [Escape] om terug te gaan naar het hoofdmenu.\n");

			// bestellingen inlezen
			var bestellingenJSON = File.ReadAllText("bestellingen.json");
			var bestellingenData = JsonConvert.DeserializeObject<bestellingenRoot>(bestellingenJSON);

			// if true dan {}
			if (true) { }; // lol

			// controle of de geselecteerde index niet out of bounds is
			if (geselecteerdeOptie < 1 | geselecteerdeOptie > bestellingenData.Bestellingen.Count-1) geselecteerdeOptie = 1;

			// laat alle rekeningen zien die er op dit moment zijn
			for (int i = 1; i < bestellingenData.Bestellingen.Count; i++) Console.WriteLine((i == geselecteerdeOptie) ? "> Tafel {0}" : "- Tafel {0}", bestellingenData.Bestellingen[i].Tafel);

			// user input voor selecteren en menu's navigeren
			ConsoleKey toets = Console.ReadKey().Key;
			if (toets == ConsoleKey.DownArrow | toets == ConsoleKey.RightArrow) rekeningenLijst(geselecteerdeOptie + 1);
			else if (toets == ConsoleKey.UpArrow | toets == ConsoleKey.LeftArrow) rekeningenLijst(geselecteerdeOptie - 1);

			// naar de bon toe van de geselecteerde bestelling
			else if (toets == ConsoleKey.Enter) rekeningBekijken(geselecteerdeOptie);

			// terug naar het vorige menu
			else if (toets == ConsoleKey.Escape) medewerkerHome.SchermMedewerker();

			// bij elke andere invoer herlaadt het scherm
			else rekeningenLijst(geselecteerdeOptie);
		}
		public static void rekeningBekijken(int rekeningIndex = 1)
		{
			// bestellingen inlezen
			var bestellingenJSON = File.ReadAllText("bestellingen.json");
			var bestellingenData = JsonConvert.DeserializeObject<bestellingenRoot>(bestellingenJSON);

			// shorthand voor de te bekijken rekening
			var rekeningOmAfTeRekenen = bestellingenData.Bestellingen[rekeningIndex];
			float totaalBedrag = 0f;

			// als de gegeven index niet gebruikt kan worden
			if (rekeningIndex <= 0 | rekeningIndex > bestellingenData.Bestellingen.Count) rekeningenLijst();

			// de rekening wordt weergeven
			Console.Clear();
			Console.WriteLine("REKENING BEKIJKEN\nDe rekening voor tafel {0} wordt weergeven.\n", rekeningOmAfTeRekenen.Tafel);
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

			// totaal bedrag laten zien en script
			Console.WriteLine($"\nTotaal: {totaalBedrag} euro\n");
			Console.WriteLine("Druk op [Enter] om af te rekenen en deze rekening uit het rekeningboek te halen.");
			Console.WriteLine("Druk op [X] om deze rekening weg te gooien zonder af te rekenen.");
			Console.WriteLine("Druk op [Escape] om terug te gaan naar het rekeningoverzicht.");

			// input verwerken
			ConsoleKey option = Console.ReadKey().Key;

			// optie bevestigen afrekenen rekening
			if (option == ConsoleKey.Enter) 
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

			// optie rekening verwijderen zonder af te rekenen
			else if (option == ConsoleKey.X)
			{
				Console.Clear();
				if (rekeningVerwijderen(rekeningOmAfTeRekenen))
				{
					string verwijderdeTafelNaam = rekeningOmAfTeRekenen.Tafel;
					bestellingenData.Bestellingen.RemoveAt(rekeningIndex);
					var geupdateBestellingen = JsonConvert.SerializeObject(bestellingenData, Formatting.Indented);
					File.WriteAllText("bestellingen.json", geupdateBestellingen);
					medewerkerHome.SchermMedewerker($"[!] De rekening van tafel {verwijderdeTafelNaam} is uit de openstaande rekeningen gehaald.");
				}
				else
				{
					rekeningBekijken(rekeningIndex);
				}
			}

			// optie aanpassen rekening [E] (WIP)

			// optie terug naar lijst met rekeningen
			else if (option == ConsoleKey.Escape) 
			{
				Console.Clear();
				rekeningenLijst(rekeningIndex);
			}

			// optie onverwachte input
			else
			{
				rekeningBekijken(rekeningIndex);
			}

		}
		public static bool rekeningVerwijderen(Bestelling rekeningOmAfTeRekenen)
		{
			Console.Clear();
			Console.WriteLine("REKENING VERWIJDEREN\nU staat op het moment om de rekening van tafel {0} uit het systeem te halen.\n", rekeningOmAfTeRekenen.Tafel);
			Console.WriteLine("[!] Weet u zeker dat u deze rekening wilt verwijderen?");
			Console.WriteLine("Druk op [Enter] om de rekening definitief te verwijderen. Dit kan niet ongedaan worden gemaakt.");
			Console.WriteLine("Druk op een andere toets om terug te gaan.");

			ConsoleKey option = Console.ReadKey().Key;
			return (option == ConsoleKey.Enter);
		}
		public static string rekeningCheck(Bestelling bes)
		{
			if (bes.Dranken.Count != 0) return bes.Dranken[0].Aantal == 69 && bes.Dranken[0].Dranknaam == "Mystery Cocktail..." && bes.Tafel == "SIGMA" ? "Nice.\n" : "";
			else return "";
		}
	}
}
