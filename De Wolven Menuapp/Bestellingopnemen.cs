using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace De_Wolven_Menuapp
{
	internal class Bestellingopnemen
	{
		public static void nieuweBestelling()
		{
			// inlezen json
			var reserveringJson = File.ReadAllText(GetFilePath.Dir("reserveringenbestand.json"));
			reserveringenRoot reserveringsData = JsonConvert.DeserializeObject<reserveringenRoot>(reserveringJson);
			string prefix;
			int geselecteerdeIndex = 1;

			while (true)
            {
				// script nieuwe bestelling
				Console.Clear();
				Console.WriteLine("NIEUWE BESTELLING/REKENING AANMAKEN\n");
				Console.WriteLine("Voor welke klant moet er een bestelling opgenomen worden?");

				for (int i = 1; i < reserveringsData.Reserveringen.Count; i++)
				{
					prefix = (geselecteerdeIndex == i) ? "> " : "- ";
					Console.WriteLine($"{prefix}{reserveringsData.Reserveringen[i].Code} op naam {reserveringsData.Reserveringen[i].Name}");
				}

				ConsoleKey input = Console.ReadKey().Key;
				if (input == ConsoleKey.DownArrow && geselecteerdeIndex < reserveringsData.Reserveringen.Count-1)
				{
					geselecteerdeIndex++;
					if (geselecteerdeIndex >= reserveringsData.Reserveringen.Count) geselecteerdeIndex = 0;
				}
				else if (input == ConsoleKey.UpArrow && geselecteerdeIndex > 1) geselecteerdeIndex--;
				if (input == ConsoleKey.Enter)
				{

					Console.Clear();

					// nieuwe lege bestelling maken, modelleren naar lege menukaart
					Bestelling nieuweBestelling = new()
					{
						Tafel = reserveringsData.Reserveringen[geselecteerdeIndex].Name,
						Code = reserveringsData.Reserveringen[geselecteerdeIndex].Code,
						gerechten = new List<Gerechten>(),
						Desserts = new List<MenuDesserts>(),
						Dranken = new List<MenuDranken>()
					};

					// lege bestelling meenemen naar selectiemenu
					bestellenInWelkeCategorie(nieuweBestelling, reserveringsData.Reserveringen[geselecteerdeIndex]);
				}
				

			}


		}
		public static void bestellenInWelkeCategorie(Bestelling huidigeBestelling, EnkeleReservering gekozenReservering)
		{
			while (true)
			{
				Console.Clear();
				Console.WriteLine($"NIEUWE BESTELLING(EN) TOEVOEGEN VOOR TAFEL VAN {gekozenReservering.Name}\n");
				Console.WriteLine("Kies van welke categorie u wilt bestellen...");
				Console.WriteLine("[1] Gerechten");
				Console.WriteLine("[2] Desserts");
				Console.WriteLine("[3] Dranken");
				Console.WriteLine("Voer [1], [2] of [3] in");

				if (huidigeBestelling.gerechten.Count != 0 | huidigeBestelling.Desserts.Count != 0 | huidigeBestelling.Dranken.Count != 0)
				{
					Console.WriteLine($"\nAan het toevoegen voor tafel van {gekozenReservering.Name}:");

					for (int i = 0; i < huidigeBestelling.gerechten.Count; i++) Console.WriteLine($"{huidigeBestelling.gerechten[i].Aantal}x {huidigeBestelling.gerechten[i].Gerechtnaam}");
					for (int i = 0; i < huidigeBestelling.Desserts.Count; i++) Console.WriteLine($"{huidigeBestelling.Desserts[i].Aantal}x {huidigeBestelling.Desserts[i].Dessertnaam}");
					for (int i = 0; i < huidigeBestelling.Dranken.Count; i++) Console.WriteLine($"{huidigeBestelling.Dranken[i].Aantal}x {huidigeBestelling.Dranken[i].Dranknaam}");

					Console.WriteLine();
					Console.WriteLine("[Enter] Items definitief aan de rekening toevoegen.");
					Console.WriteLine("[Escape] Teruggaan naar het hoofdmenu en items weggooien.");
				}

				ConsoleKey Bestellen = Console.ReadKey().Key;
				if (Bestellen == ConsoleKey.D1)
				{
					Console.Clear();
					bestellingKiesOptie(1, huidigeBestelling);
				}

				else if (Bestellen == ConsoleKey.D2)
				{
					Console.Clear();
					bestellingKiesOptie(2, huidigeBestelling);
				}

				else if (Bestellen == ConsoleKey.D3)
				{
					Console.Clear();
					bestellingKiesOptie(3, huidigeBestelling);
				}
				else if (Bestellen == ConsoleKey.Escape) // terug naar hoofdmenu
				{
					Console.Clear();
					Console.WriteLine($"[!] De voorgenoemde items van tafel {gekozenReservering.Name} zijn weggegooid."); break;
				}
				else if (Bestellen == ConsoleKey.Enter)
				{
					Console.Clear();
					bestellingDoorvoeren(huidigeBestelling, gekozenReservering);
				}
			}
		}
		public static void bestellingKiesOptie(int categorie, Bestelling nieuweItems, string laatsteItem = "", int laatsteItemHvh = 0)
		{
			
			// laadbericht
			Console.Clear();
			Console.WriteLine("Menu inlezen...");
			 
			// menukaart inlezen
			string menukaartJson = File.ReadAllText(GetFilePath.Dir("Menukaart.JSON"));
			var menuData = JsonConvert.DeserializeObject<Menukaart>(menukaartJson);

			// bestellingen inlezen
			try
			{
				string bestellingenJson = File.ReadAllText(GetFilePath.Dir("rekeningen.json"));
				var bestellingenData = JsonConvert.DeserializeObject<bestellingenRoot>(bestellingenJson);
			}
            catch (FileNotFoundException)
            {
				string a = "";
				File.WriteAllText(GetFilePath.Dir("rekeningen.json"), a);
            };

			Console.Clear();
			// initialiseren variables
			int screen = 0;
			int max = 0;
			int pgmax = 0;
			int hoeveelheidOpties = 0;
			string categorienaam = "";

			if (categorie == 1)
			{
				max = menuData.gerechten.Count; // totaal aantal gerechten
				pgmax = (max % 8 == 0) ? (menuData.gerechten.Count / 8 - 1) : (menuData.gerechten.Count / 8); // max aantal pagina's voor gerechten
				categorienaam = "gerecht";
			}
			else if (categorie == 2)
			{
				max = menuData.Desserts.Count; // totaal aantal desserts
				pgmax = (max % 8 == 0) ? (menuData.Desserts.Count / 8 - 1) : (menuData.Desserts.Count / 8); // max aantal pagina's vpor desserts
				categorienaam = "dessert";
			}
			else if (categorie == 3)
			{
				max = menuData.Dranken.Count; // totaal aantal dranken
				pgmax = (max % 8 == 0) ? (menuData.Dranken.Count / 8 - 1) : (menuData.Dranken.Count / 8); // max aantal pagina's voor dranken
				categorienaam = "drank";
			}

			while (true)
			{
				Console.Clear();
				// weergeven menu items van juiste pagina
				Console.WriteLine($"MENUKAART - {categorienaam.ToUpper()} KIEZEN");
				Console.WriteLine($"Pagina {screen+1} van {pgmax+1}");
				Console.WriteLine(laatsteItemHvh != 0 ? $"[!] {laatsteItemHvh}x {laatsteItem} toegevoegd aan rekening voor tafel [1A]" : ""); // optioneel laatst toegevoegd item
				Console.WriteLine("\n\n");

				// c is het nummer voor de bestellingoptie [c] weergeven, i is de index van het gerecht in de json
				for (int i = 8 * screen, c = 1; c <= 8 | i <= max-1; i++, c++) 
					if (i < max && c < 9)
					{
						if (categorie == 1) Console.WriteLine("[" + c + "] " + menuData.gerechten[i].Gerechtnaam + ", " + menuData.gerechten[i].Prijs + " euro, " + menuData.gerechten[i].Allergenen);
						else if (categorie == 2) Console.WriteLine("[" + c + "] " + menuData.Desserts[i].Dessertnaam + ", " + menuData.Desserts[i].Prijs + " euro, " + menuData.Desserts[i].Allergenen);
						else if (categorie == 3) Console.WriteLine("[" + c + "] " + menuData.Dranken[i].Dranknaam + ", " + menuData.Dranken[i].Prijs + " euro, " + menuData.Dranken[i].Allergenen);
						hoeveelheidOpties = c; // mogelijk aantal inputs bepalen
					}

				// moeilijk doen over hoofdletters
				TextInfo Cap = new CultureInfo("en-US", false).TextInfo; 
				Console.WriteLine($"\n\n[1] - [8] {Cap.ToTitleCase(categorienaam)} selecteren\n[<] [>] Pagina wisselen\n[Enter] of [Escape] Terug naar categorieselectie");
				
				// bepalen welke ConsoleKeys gebruikt mogen worden voor de input (1-8)
				var opties = mogelijkeInputs(hoeveelheidOpties);

				// keuzes die de gebruiker kan maken
				ConsoleKey input;
				input = Console.ReadKey().Key; // input staat gelijk aan de toets die de gebruiker invoert
				if (input == ConsoleKey.RightArrow & pgmax != screen) // verhoog screenvariable
				{
					screen++;
					Console.Clear();
				}
				else if (input == ConsoleKey.LeftArrow & pgmax != 0 & screen > 0) // verlaag screenvariable
				{
					screen -= 1;
					Console.Clear();
				}
				else if (input == ConsoleKey.Escape || input == ConsoleKey.Enter) // terug naar hoofdmenu
				{
					Console.Clear();
					break;
				}
				else if (input == ConsoleKey.RightArrow & pgmax == screen) // als je na het laatste scherm naar rechts gaat dan gaat hij terug naar het eerste scherm
				{
					screen = 0;
					Console.Clear();
				}
				
				else if (opties.Contains(input)) // als de gebruiker een item van het menu kiest...
				{
					Console.Clear();

					// init variabelen
					string recentItemNaam = "";
					int recentItemHvh = 0;

					// nieuw item toevoegen aan lege bestelling dummy
					if (categorie == 1) 
					{
						int rightIndex = menuItemIndexOphalen(screen, input); // juiste index van menukaart ophalen
						nieuweItems.gerechten.Add(menuData.gerechten[rightIndex]); // menu item aan lege dummy bestelling toevoegen
						Console.WriteLine($"U heeft gekozen voor {menuData.gerechten[menuItemIndexOphalen(screen, input)].Gerechtnaam}."); // console
						nieuweItems.gerechten[nieuweItems.gerechten.Count-1].Aantal = kiesHoeveelheidKeuze(); // prompt hoeveel er besteld moeten worden

						// voor wanneer je uit het menu gaat
						recentItemNaam = nieuweItems.gerechten[nieuweItems.gerechten.Count - 1].Gerechtnaam; 
						recentItemHvh = nieuweItems.gerechten[nieuweItems.gerechten.Count - 1].Aantal;

					}
					// etc voor elke categorie
					else if (categorie == 2)
					{
						int rightIndex = menuItemIndexOphalen(screen, input);
						nieuweItems.Desserts.Add(menuData.Desserts[rightIndex]);
						Console.WriteLine($"U heeft gekozen voor {menuData.Desserts[menuItemIndexOphalen(screen, input)].Dessertnaam}.");
						nieuweItems.Desserts[nieuweItems.Desserts.Count - 1].Aantal = kiesHoeveelheidKeuze();

						recentItemNaam = nieuweItems.Desserts[nieuweItems.Desserts.Count - 1].Dessertnaam;
						recentItemHvh = nieuweItems.Desserts[nieuweItems.Desserts.Count - 1].Aantal;

					}
					else if (categorie == 3)
					{
						int rightIndex = menuItemIndexOphalen(screen, input);
						nieuweItems.Dranken.Add(menuData.Dranken[rightIndex]);
						Console.WriteLine($"U heeft gekozen voor {menuData.Dranken[menuItemIndexOphalen(screen, input)].Dranknaam}.");
						nieuweItems.Dranken[nieuweItems.Dranken.Count - 1].Aantal = kiesHoeveelheidKeuze();

						recentItemNaam = nieuweItems.Dranken[nieuweItems.Dranken.Count - 1].Dranknaam;
						recentItemHvh = nieuweItems.Dranken[nieuweItems.Dranken.Count - 1].Aantal;

					}
					Console.Clear();
				}
			}

		}
		public static List<ConsoleKey> mogelijkeInputs(int num) // geeft lijst van mogelijke numeral consolekeys op basis van int input
		{
			List<ConsoleKey> result = new List<ConsoleKey>();
			if (1 <= num) result.Add(ConsoleKey.D1);
			if (2 <= num) result.Add(ConsoleKey.D2);
			if (3 <= num) result.Add(ConsoleKey.D3);
			if (4 <= num) result.Add(ConsoleKey.D4);
			if (5 <= num) result.Add(ConsoleKey.D5);
			if (6 <= num) result.Add(ConsoleKey.D6);
			if (7 <= num) result.Add(ConsoleKey.D7);
			if (8 <= num) result.Add(ConsoleKey.D8);
			return result;
		}
		public static int menuItemIndexOphalen(int welkScherm, ConsoleKey key) // geeft op basis van een scherm en een consolekey de juiste index in de menukaart JSON
		{
			int c = 0;
			if (key == ConsoleKey.D1) c = 1;
			if (key == ConsoleKey.D2) c = 2;
			if (key == ConsoleKey.D3) c = 3;
			if (key == ConsoleKey.D4) c = 4;
			if (key == ConsoleKey.D5) c = 5;
			if (key == ConsoleKey.D6) c = 6;
			if (key == ConsoleKey.D7) c = 7;
			if (key == ConsoleKey.D8) c = 8;

			int juisteIndex = (welkScherm * 8) + (c - 1); // de magische formule die de juiste index geeft
			return juisteIndex;
		}
		public static int kiesHoeveelheidKeuze()
		{
			// prompt voor kiezen hoeveelheid van item plus invoercontrole
			Console.WriteLine("\nGeef aan hoeveel er u voor deze tafel wilt bestellen. Voer een getal in en druk op [Enter].");

			int aantalVanOptie;
			while (!Int32.TryParse(Console.ReadLine(), out aantalVanOptie))
            {
				Console.Clear();
				Console.WriteLine("[!] Geen geldige input. Voer een getal in.\n");
				Console.WriteLine("Geef aan hoeveel er u voor deze tafel wilt bestellen. Voer een getal in en druk op [Enter].");
			}
			return aantalVanOptie;
		}
		public static void bestellingDoorvoeren(Bestelling nieuweToevoeging, EnkeleReservering gekozenReservering)
		{
			// bestellingen inlezen
			string rekeningenJson = File.ReadAllText(GetFilePath.Dir("rekeningen.json"));
			var rekeningenData = JsonConvert.DeserializeObject<bestellingenRoot>(rekeningenJson);

			bool maakNieuweRekening = true; // gaat naar false als de rekening is gevonden. anders maakt hij een nieuwe rekening aan

			// appenden aan bestaande rekening
			for (int i = 0; i < rekeningenData.Bestellingen.Count(); i++) // ga alle bestaande rekeningen af...
			{
				if (rekeningenData.Bestellingen[i].Code == gekozenReservering.Code) // is de tafel gevonden?
				{
					maakNieuweRekening = false; // er hoeft geen nieuwe rekening aangemaakt te worden als dit bereikt wordt

					for (int j = 0; j < nieuweToevoeging.gerechten.Count; j++) // voor elk gerecht wat nieuw toegevoegd wordt ..
                    {
						if (rekeningenData.Bestellingen[i].gerechten[j].Gerechtnaam == nieuweToevoeging.gerechten[j].Gerechtnaam) // komt de naam overeen met iets wat al op de rekening staat?
							rekeningenData.Bestellingen[i].gerechten[j].Aantal += nieuweToevoeging.gerechten[j].Aantal;// .. dan niet toevoegen als nieuwe order, maar aantal optellen bij wat al besteld is
						else rekeningenData.Bestellingen[i].gerechten.Add(nieuweToevoeging.gerechten[j]);// .. en anders gewoon toevoegen
					}
					for (int j = 0; j < nieuweToevoeging.Desserts.Count; j++) // hetzelfde voor alle nieuwe desserts ..
                    {
						if (rekeningenData.Bestellingen[i].Desserts[j].Dessertnaam == nieuweToevoeging.Desserts[j].Dessertnaam)
						if (rekeningenData.Bestellingen[i].Desserts[j].Dessertnaam == nieuweToevoeging.Desserts[j].Dessertnaam)
								rekeningenData.Bestellingen[i].Desserts[j].Aantal += nieuweToevoeging.Desserts[j].Aantal; 
						else rekeningenData.Bestellingen[i].Desserts.Add(nieuweToevoeging.Desserts[j]);
					}
					for (int j = 0; j < nieuweToevoeging.Dranken.Count; j++) // .. en nieuwe dranken
                    {
						if (rekeningenData.Bestellingen[i].Dranken[j].Dranknaam == nieuweToevoeging.Dranken[j].Dranknaam)
							rekeningenData.Bestellingen[i].Dranken[j].Aantal += nieuweToevoeging.Dranken[j].Aantal;
						else rekeningenData.Bestellingen[i].Dranken.Add(nieuweToevoeging.Dranken[j]);
					}
				}
			}
			
			// nieuwe rekening aanmaken ipv aan bestaande toevoegen
			if (maakNieuweRekening) rekeningenData.Bestellingen.Add(nieuweToevoeging);

			// bestellingen met geupdate items naar disk schrijven
			var geupdateBestellingen = JsonConvert.SerializeObject(rekeningenData, Formatting.Indented);
			File.WriteAllText(GetFilePath.Dir("rekeningen.json"), geupdateBestellingen);

			// terug naar hoofdmenu
			medewerkerHome.SchermMedewerker($"[!] Succesvol de nieuwe bestelling(en) aan de rekening van tafel voor {gekozenReservering.Name} toegevoegd!");

		}

	}
}
