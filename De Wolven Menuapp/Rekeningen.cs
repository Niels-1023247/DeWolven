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
			var geselecteerdeRekening = bestellingenData.Bestellingen[rekeningIndex];
			float totaalBedrag = 0f;

			// als de gegeven index niet gebruikt kan worden
			if (rekeningIndex <= 0 | rekeningIndex > bestellingenData.Bestellingen.Count) rekeningenLijst();

			// de rekening wordt weergeven
			Console.Clear();
			Console.WriteLine("REKENING BEKIJKEN\nDe rekening voor tafel {0} wordt weergeven.\n", geselecteerdeRekening.Tafel);
			Console.WriteLine("GERECHTEN");

			for (int i = 0; i < geselecteerdeRekening.gerechten.Count; i++)
			{
				Console.WriteLine($"- {geselecteerdeRekening.gerechten[i].Aantal}x {geselecteerdeRekening.gerechten[i].Gerechtnaam} - {geselecteerdeRekening.gerechten[i].Aantal * geselecteerdeRekening.gerechten[i].Prijs} euro");
				totaalBedrag += geselecteerdeRekening.gerechten[i].Aantal * geselecteerdeRekening.gerechten[i].Prijs;
			}			
			Console.WriteLine("\nDESSERTS");
			for (int i = 0; i < geselecteerdeRekening.Desserts.Count; i++)
			{
				Console.WriteLine($"- {geselecteerdeRekening.Desserts[i].Aantal}x {geselecteerdeRekening.Desserts[i].Dessertnaam} - {geselecteerdeRekening.Desserts[i].Aantal * geselecteerdeRekening.Desserts[i].Prijs} euro");
				totaalBedrag += geselecteerdeRekening.Desserts[i].Aantal * geselecteerdeRekening.Desserts[i].Prijs;
			}			
			Console.WriteLine("\nDRANKEN");
			for (int i = 0; i < geselecteerdeRekening.Dranken.Count; i++)
			{
				Console.WriteLine($"- {geselecteerdeRekening.Dranken[i].Aantal}x {geselecteerdeRekening.Dranken[i].Dranknaam} - {geselecteerdeRekening.Dranken[i].Aantal * geselecteerdeRekening.Dranken[i].Prijs} euro");
				totaalBedrag += geselecteerdeRekening.Dranken[i].Aantal * geselecteerdeRekening.Dranken[i].Prijs;
			}

			// totaal bedrag laten zien en script
			Console.WriteLine($"\nTotaal: {totaalBedrag} euro\n");
			Console.WriteLine("Druk op [Enter] om af te rekenen en deze rekening uit het rekeningboek te halen.");
			Console.WriteLine("Druk op [X] om deze rekening weg te gooien zonder af te rekenen.");
			Console.WriteLine("Druk op [B] om items van deze rekening te bewerken/verwijderen.");
			Console.WriteLine("Druk op [Escape] om terug te gaan naar het rekeningoverzicht.");

			// input verwerken
			ConsoleKey option = Console.ReadKey().Key;

			// optie bevestigen afrekenen rekening
			if (option == ConsoleKey.Enter) 
			{
				// console script
				Console.Clear();
				Console.WriteLine("REKENING BETAALD!");
				Console.WriteLine(rekeningCheck(geselecteerdeRekening));
				Console.WriteLine($"U heeft de rekening voor tafel {geselecteerdeRekening.Tafel} afgerekend.");
				Console.WriteLine($"Het totaalbedrag van deze rekening was {totaalBedrag} euro.");
				Console.WriteLine("Druk op een toets om terug te gaan naar het medewerkersmenu.");

				// verwijder bestelling uit systeem, en update de database
				bestellingenData.Bestellingen.RemoveAt(rekeningIndex);
				var geupdateBestellingen = JsonConvert.SerializeObject(bestellingenData, Formatting.Indented);
				File.WriteAllText("bestellingen.json", geupdateBestellingen);
				ConsoleKey cont = Console.ReadKey().Key;
				medewerkerHome.SchermMedewerker();

				// vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv
				// RESERVERING <---- NIET VERGETEN UIT HET SYSTEEM HALEN !!! TOEVOEGEN
				// ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
			}

			// optie rekening verwijderen zonder af te rekenen
			else if (option == ConsoleKey.X)
			{
				Console.Clear();
				if (rekeningVerwijderen(geselecteerdeRekening))
				{
					string verwijderdeTafelNaam = geselecteerdeRekening.Tafel;
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

			// optie aanpassen rekening [B] (WIP)
			else if (option == ConsoleKey.B)
			{
				rekeningAanpassen(geselecteerdeRekening, rekeningIndex);
			}

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
		public static void rekeningAanpassen(Bestelling geselecteerdeRekening, int rekeningIndex = 1, int geselecteerdeCategorie = 0, int geselecteerdeIndex = 0)
		{
			// init variables
			string prefix = "- ";
			
			// script voor weergave items bij bewerken menu
			Console.Clear();
			Console.WriteLine("REKENING AANPASSEN");
			Console.WriteLine("U kunt hier het aantal van een item aanpassen of verwijderen.\n");
			Console.WriteLine("GERECHTEN");

			for (int i = 0; i < geselecteerdeRekening.gerechten.Count; i++)
			{
				prefix = (geselecteerdeIndex == i && geselecteerdeCategorie == 0) ? "> " : "- "; // juiste prefix voor uitprinten item bepalen (item geselecteerd of niet?)
				Console.WriteLine($"{prefix}{geselecteerdeRekening.gerechten[i].Aantal}x {geselecteerdeRekening.gerechten[i].Gerechtnaam} - {geselecteerdeRekening.gerechten[i].Aantal * geselecteerdeRekening.gerechten[i].Prijs} euro");
			}
			Console.WriteLine("\nDESSERTS");
			for (int i = 0; i < geselecteerdeRekening.Desserts.Count; i++)
			{
				prefix = (geselecteerdeIndex == i && geselecteerdeCategorie == 1) ? "> " : "- ";
				Console.WriteLine($"{prefix}{geselecteerdeRekening.Desserts[i].Aantal}x {geselecteerdeRekening.Desserts[i].Dessertnaam} - {geselecteerdeRekening.Desserts[i].Aantal * geselecteerdeRekening.Desserts[i].Prijs} euro");
			}
			Console.WriteLine("\nDRANKEN");
			for (int i = 0; i < geselecteerdeRekening.Dranken.Count; i++)
			{
				prefix = (geselecteerdeIndex == i && geselecteerdeCategorie == 2) ? "> " : "- ";
				Console.WriteLine($"{prefix}{geselecteerdeRekening.Dranken[i].Aantal}x {geselecteerdeRekening.Dranken[i].Dranknaam} - {geselecteerdeRekening.Dranken[i].Aantal * geselecteerdeRekening.Dranken[i].Prijs} euro");
			}

			// script voor console
			Console.WriteLine("\nSelecteer met de pijltjestoetsen [^] [v] een item en druk dan op [Enter].");
			Console.WriteLine("Of druk op [Escape] om terug te gaan naar het inzien van de rekening.\n");
            Console.WriteLine($"{geselecteerdeIndex} - {geselecteerdeCategorie}");

			// hoeveelheid items per categorie van de bestelling berekenen
			// dit moet helaas omdat we 3 categorieën hebben en de gebruiker moet kunnen schakelen tussen de items alsof het 1 categorie is.
			int[] hoeveelPerCategorie = new int[3]; // voorbeeld van abstractie?? voor documentatie

			for (int i = 0; i < 3; i++)
			{
				if (i == 0) hoeveelPerCategorie[i] = geselecteerdeRekening.gerechten.Count != 0 ? geselecteerdeRekening.gerechten.Count : 0;
				if (i == 1) hoeveelPerCategorie[i] = geselecteerdeRekening.Desserts.Count != 0 ? geselecteerdeRekening.Desserts.Count : 0;
				if (i == 2) hoeveelPerCategorie[i] = geselecteerdeRekening.Dranken.Count != 0 ? geselecteerdeRekening.Dranken.Count : 0;
			}

			// input verwerken deel 1
			ConsoleKey input = Console.ReadKey().Key;
			if (input == ConsoleKey.DownArrow || input == ConsoleKey.RightArrow)
            {
				geselecteerdeIndex += 1;
            }
			else if (input == ConsoleKey.UpArrow || input == ConsoleKey.LeftArrow)
            {
				geselecteerdeIndex -= 1;
			}
			else if (input == ConsoleKey.Escape)
            {
				Console.Clear();
				rekeningBekijken(rekeningIndex);
            }

			// index te hoog?
			if (geselecteerdeIndex >= hoeveelPerCategorie[geselecteerdeCategorie]) 
            {
				geselecteerdeCategorie += 1;
				// categorie te hoog?
				if (geselecteerdeCategorie > 2)
				{
					geselecteerdeCategorie = 0;
					geselecteerdeIndex = 0;
				}
				if (hoeveelPerCategorie[geselecteerdeCategorie] == 0) geselecteerdeCategorie += 1;
				geselecteerdeIndex = 0;
			}

			// index te laag?
			if (geselecteerdeIndex < 0)
            {
				geselecteerdeCategorie -= 1;
				// categorie te laag?
				if (geselecteerdeCategorie < 0)
				{
					geselecteerdeCategorie = 2;
					geselecteerdeIndex = hoeveelPerCategorie[geselecteerdeCategorie] - 1;
				}
				if (hoeveelPerCategorie[geselecteerdeCategorie] == 0) geselecteerdeCategorie -= 1;
				geselecteerdeIndex = hoeveelPerCategorie[geselecteerdeCategorie] - 1;
			}

			// reload
			rekeningAanpassen(geselecteerdeRekening, rekeningIndex, geselecteerdeCategorie, geselecteerdeIndex);

		}
		public static void itemAanpassen()
		{
			Console.WriteLine("Druk op de pijltjestoetsen [^] [v] om het aantal aan te passen");
			Console.WriteLine("Druk op [X] om een item te verwijderen.");
			Console.WriteLine("Of druk op [A] om het aantal aan te passen.");
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
