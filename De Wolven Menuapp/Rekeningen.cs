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
			while (true)
			{
				// script voor console
				Console.Clear();
				Console.WriteLine("OPENSTAANDE REKENINGEN");
				Console.WriteLine("Alle openstaande rekeningen worden per naam weergeven.\n");

				// bestellingen inlezen
				var rekeningenJSON = File.ReadAllText(GetFilePath.RekeningenPath);
				var rekeningenData = JsonConvert.DeserializeObject<bestellingenRoot>(rekeningenJSON);

				// reserveringen ophalen
				var reserveringJson = File.ReadAllText("reserveringenbestand.json");
				reserveringenRoot reserveringsData = JsonConvert.DeserializeObject<reserveringenRoot>(reserveringJson);

				string suffix;

				// controle of de geselecteerde index niet out of bounds is
				if (geselecteerdeOptie < 1 | geselecteerdeOptie > rekeningenData.Bestellingen.Count - 1) geselecteerdeOptie = 1;

				// laat alle rekeningen zien die er op dit moment zijn
				for (int i = 1; i < rekeningenData.Bestellingen.Count; i++)
				{
					suffix = legeRekeningCheck(rekeningenData.Bestellingen[i]) ? " [Leeg]" : "";
					Console.WriteLine((i == geselecteerdeOptie) ? "> Rekening van {0}{1}" : "- Rekening van {0}{1}", rekeningenData.Bestellingen[i].Tafel, suffix);
				}

				if (rekeningenData.Bestellingen.Count == 1) Console.WriteLine("Er zijn geen openstaande rekeningen om te laten zien.\n"); // er zit altijd een lege dummy bestelling in
				else
				{
					// rest van het script
					Console.WriteLine("\n[^] [v] Selecteer een rekening");
					Console.WriteLine("[Enter] Bekijk rekening");
				}
				Console.WriteLine("[Escape] Terug naar het hoofdmenu\n");


				// user input voor selecteren en menu's navigeren
				ConsoleKey toets = Console.ReadKey().Key;
				if (toets == ConsoleKey.DownArrow | toets == ConsoleKey.RightArrow) rekeningenLijst(geselecteerdeOptie + 1);
				else if (toets == ConsoleKey.UpArrow | toets == ConsoleKey.LeftArrow) rekeningenLijst(geselecteerdeOptie - 1);

				// naar de bon toe van de geselecteerde bestelling
				else if (toets == ConsoleKey.Enter) rekeningBekijken(geselecteerdeOptie, !legeRekeningCheck(rekeningenData.Bestellingen[geselecteerdeOptie]));

				// terug naar het vorige menu
				else if (toets == ConsoleKey.Escape) break;

				// bij elke andere invoer herlaadt het scherm
				else rekeningenLijst(geselecteerdeOptie);
			}
		}
		public static void rekeningBekijken(int rekeningIndex = 1, bool magAanpassen = true, string melding = "")
		{
			while (true)
			{

				// bestellingen inlezen
				var rekeningenJSON = File.ReadAllText(GetFilePath.RekeningenPath);
				var rekeningenData = JsonConvert.DeserializeObject<bestellingenRoot>(rekeningenJSON);

				// shorthand voor de te bekijken rekening
				var geselecteerdeRekening = rekeningenData.Bestellingen[rekeningIndex];
				float totaalBedrag = 0f;

				// lege rekening melding
				if (legeRekeningCheck(geselecteerdeRekening))
				{
					melding = "[!] U kunt deze bestelling niet bewerken omdat deze leeg is.\n";
					magAanpassen = false;
				}

				// als de gegeven index niet gebruikt kan worden
				if (rekeningIndex <= 0 | rekeningIndex > rekeningenData.Bestellingen.Count) break;

				// de rekening wordt weergeven
				Console.Clear();
				Console.WriteLine("REKENING BEKIJKEN\nDe rekening voor tafel {0} wordt weergeven.", geselecteerdeRekening.Tafel);
				Console.WriteLine(melding);
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

				// voorwaardelijke opties (is de bestelling niet leeg?)
				if (magAanpassen)
				{
					Console.WriteLine("[Enter] Deze rekening afrekenen en uit het overzicht halen");
					Console.WriteLine("[B] Rekening bewerken");
				}
				Console.WriteLine("[X] Rekening weggooien zonder af te rekenen");
				Console.WriteLine("[Escape] Terug naar rekeningoverzicht");

				// input verwerken
				ConsoleKey option = Console.ReadKey().Key;

				// optie bevestigen afrekenen rekening
				if (option == ConsoleKey.Enter)
				{
					// controle lege rekening
					if (legeRekeningCheck(geselecteerdeRekening)) rekeningBekijken(rekeningIndex, false, "[!] U kunt deze bestelling niet bewerken omdat deze leeg is.");

					// console script
					Console.Clear();
					Console.WriteLine("REKENING BETAALD!");
					Console.WriteLine(rekeningCheck(geselecteerdeRekening));
					Console.WriteLine($"U heeft de rekening voor tafel {geselecteerdeRekening.Tafel} afgerekend.");
					Console.WriteLine($"Het totaalbedrag van deze rekening was {totaalBedrag} euro.");
					Console.WriteLine("Druk op een toets om terug te gaan naar het medewerkersmenu.");

					// verwijder bestelling uit systeem, en update de database
					rekeningenData.Bestellingen.RemoveAt(rekeningIndex);
					var geupdateRekeningen = JsonConvert.SerializeObject(rekeningenData, Formatting.Indented);
					File.WriteAllText(GetFilePath.RekeningenPath, geupdateRekeningen);
					ConsoleKey cont = Console.ReadKey().Key;
					medewerkerHome.SchermMedewerker();

					// RESERVERING <---- NIET VERGETEN UIT HET SYSTEEM HALEN !!! TOEVOEGEN
				}

				// optie rekening verwijderen zonder af te rekenen
				else if (option == ConsoleKey.X)
				{
					Console.Clear();
					if (rekeningVerwijderen(geselecteerdeRekening))
					{
						string verwijderdeTafelNaam = geselecteerdeRekening.Tafel;
						rekeningenData.Bestellingen.RemoveAt(rekeningIndex);
						var geupdateBestellingen = JsonConvert.SerializeObject(rekeningenData, Formatting.Indented);
						File.WriteAllText(GetFilePath.RekeningenPath, geupdateBestellingen);
						medewerkerHome.SchermMedewerker($"[!] De rekening van tafel {verwijderdeTafelNaam} is uit de openstaande rekeningen gehaald.");
					}
				}

				// optie aanpassen items op rekening
				else if (option == ConsoleKey.B)
				{
					if (legeRekeningCheck(geselecteerdeRekening))
					{
						magAanpassen = false;
						melding = "[!] U kunt deze bestelling niet bewerken omdat deze leeg is.\n";
					}
					else rekeningAanpassen(geselecteerdeRekening, rekeningIndex);
				}

				// optie terug naar lijst met rekeningen
				else if (option == ConsoleKey.Escape) break;

			}
		}
		public static void rekeningAanpassen(Bestelling geselecteerdeRekening, int rekeningIndex = 1, int geselecteerdeCategorie = 0, int geselecteerdeIndex = 0, bool changesMade = false)
		{
			while (true)
			{
				// veranderingen toepassen indien je terugkomt van items aanpassen
				if (changesMade)
				{
					// inlezen
					var rekeningenJSON = File.ReadAllText(GetFilePath.RekeningenPath);
					var rekeningenData = JsonConvert.DeserializeObject<bestellingenRoot>(rekeningenJSON);
					rekeningenData.Bestellingen[rekeningIndex] = geselecteerdeRekening;

					// terugschrijven
					var geupdateRekeningen = JsonConvert.SerializeObject(rekeningenData, Formatting.Indented);
					File.WriteAllText(GetFilePath.RekeningenPath, geupdateRekeningen);
				}

				// check of de bestelling leeg is
				if (legeRekeningCheck(geselecteerdeRekening))
                {
					break;
                }

				// init variables
				string prefix = "- ";

				// script voor weergave items bij bewerken menu
				Console.Clear();
				Console.WriteLine("REKENING BEWERKEN");
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
				Console.WriteLine("\n[^] [v] Selecteer een item");
				Console.WriteLine("[Enter] Item bewerken");
				Console.WriteLine("[X] Item verwijderen");
				Console.WriteLine("[Escape] Terug naar inzien rekening\n");

				// hoeveelheid items per categorie van de bestelling berekenen
				// dit moet helaas omdat we 3 categorieën hebben en de gebruiker moet kunnen schakelen tussen de items alsof het 1 categorie is.
				int[] hoeveelPerCategorie = new int[3]; // voorbeeld van abstractie?? voor documentatie

				for (int i = 0; i < 3; i++)
				{
					if (i == 0) hoeveelPerCategorie[i] = geselecteerdeRekening.gerechten.Count != 0 ? geselecteerdeRekening.gerechten.Count : 0;
					if (i == 1) hoeveelPerCategorie[i] = geselecteerdeRekening.Desserts.Count != 0 ? geselecteerdeRekening.Desserts.Count : 0;
					if (i == 2) hoeveelPerCategorie[i] = geselecteerdeRekening.Dranken.Count != 0 ? geselecteerdeRekening.Dranken.Count : 0;
				}

				// input verwerken
				ConsoleKey input = Console.ReadKey().Key;
				if (input == ConsoleKey.DownArrow || input == ConsoleKey.RightArrow)
				{
					geselecteerdeIndex += 1;

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
				}
				else if (input == ConsoleKey.UpArrow || input == ConsoleKey.LeftArrow)
				{
					geselecteerdeIndex -= 1;

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
				}
				// optie terug naar rekening bekijken
				else if (input == ConsoleKey.Escape) break;

				// optie item selecteren
				else if (input == ConsoleKey.Enter)
				{
					// item aanpassen of verwijderen
					string currentItemName = "";
					int currentItemQuant = 0;

					// per categorie een if statement
					if (geselecteerdeCategorie == 0) // aanpassen hvh item als het een gerecht is
					{
						currentItemName = geselecteerdeRekening.gerechten[geselecteerdeIndex].Gerechtnaam;
						currentItemQuant = geselecteerdeRekening.gerechten[geselecteerdeIndex].Aantal;

						int newQuant = itemAanpassen(currentItemName, currentItemQuant);
						geselecteerdeRekening.gerechten[geselecteerdeIndex].Aantal = newQuant != -1 ? newQuant : currentItemQuant;
						Console.WriteLine();
					}
					if (geselecteerdeCategorie == 1) // aanpassen hvh item als het een dessert is
					{
						currentItemName = geselecteerdeRekening.Desserts[geselecteerdeIndex].Dessertnaam;
						currentItemQuant = geselecteerdeRekening.Desserts[geselecteerdeIndex].Aantal;

						int newQuant = itemAanpassen(currentItemName, currentItemQuant);
						geselecteerdeRekening.Desserts[geselecteerdeIndex].Aantal = newQuant != -1 ? newQuant : currentItemQuant;

					}
					if (geselecteerdeCategorie == 2) // aanpassen hvh item als het een drank is
					{
						currentItemName = geselecteerdeRekening.Dranken[geselecteerdeIndex].Dranknaam;
						currentItemQuant = geselecteerdeRekening.Dranken[geselecteerdeIndex].Aantal;

						int newQuant = itemAanpassen(currentItemName, currentItemQuant);
						geselecteerdeRekening.Dranken[geselecteerdeIndex].Aantal = newQuant != -1 ? newQuant : currentItemQuant;
					}

					changesMade = true;

				}

				// optie item verwijderen
				else if (input == ConsoleKey.X)
				{
					string currentItemName = "";
					if (geselecteerdeCategorie == 0)
					{
						currentItemName = geselecteerdeRekening.gerechten[geselecteerdeIndex].Gerechtnaam;
						if (itemVerwijderen(currentItemName)) geselecteerdeRekening.gerechten.RemoveAt(geselecteerdeIndex);
					}

					if (geselecteerdeCategorie == 1)
					{
						currentItemName = geselecteerdeRekening.Desserts[geselecteerdeIndex].Dessertnaam;
						if (itemVerwijderen(currentItemName)) geselecteerdeRekening.Desserts.RemoveAt(geselecteerdeIndex);
					}

					if (geselecteerdeCategorie == 2)
					{
						currentItemName = geselecteerdeRekening.Dranken[geselecteerdeIndex].Dranknaam;
						if (itemVerwijderen(currentItemName)) geselecteerdeRekening.Dranken.RemoveAt(geselecteerdeIndex);
					}

					geselecteerdeCategorie = 0;
					geselecteerdeIndex = 0;
					changesMade = true;

				}
			}
		}
		public static int itemAanpassen(string naamItem, int huidigeHoeveelheid)
		{
			while (true)
            {
				// script voor console
				Console.Clear();
				Console.WriteLine("BEWERKEN ITEM OP REKENING\n");
				Console.WriteLine("  ^");
				Console.WriteLine($"  {huidigeHoeveelheid} x {naamItem}");
				Console.WriteLine("  V\n");

				Console.WriteLine("[^] [v] Pas hoeveelheid item aan");
				Console.WriteLine("[Enter] Wijziging opslaan");
				Console.WriteLine("[Escape] Terug naar menu en wijziging niet opslaan");

				// input lezen
				ConsoleKey hvh = Console.ReadKey().Key;
				
				// hoger
				if (hvh == ConsoleKey.UpArrow)
				{
					huidigeHoeveelheid += 1;
				}
				// lager
				else if (hvh == ConsoleKey.DownArrow && huidigeHoeveelheid > 1)
				{
					huidigeHoeveelheid -= 1;
				}
				// toepassen
				else if (hvh == ConsoleKey.Enter)
				{
					return huidigeHoeveelheid;
				}
				// annuleren
				else if (hvh == ConsoleKey.Escape)
                {
					return -1;
                }
			}

		}
		public static bool itemVerwijderen(string naamItem)
        {
			Console.Clear();
			Console.WriteLine("ITEM VERWIJDEREN VAN REKENING");
            Console.WriteLine($"\nWeet u zeker dat u {naamItem} wilt verwijderen van deze rekening?");
            Console.WriteLine("\n[Enter] Bevestigen");
            Console.WriteLine("[Andere toets] Annuleren");

			ConsoleKey consoleKey = Console.ReadKey().Key;
            return consoleKey == ConsoleKey.Enter;
        }
		public static bool rekeningVerwijderen(Bestelling rekeningOmAfTeRekenen)
		{
			Console.Clear();
			Console.WriteLine("REKENING VERWIJDEREN\nU staat op het moment om de rekening van tafel {0} uit het systeem te halen.\n", rekeningOmAfTeRekenen.Tafel);
			Console.WriteLine("[!] Weet u zeker dat u deze rekening wilt verwijderen?");
			Console.WriteLine("[Enter] Rekening definitief verwijderen");
			Console.WriteLine("[Andere toets] Annuleren");

			ConsoleKey option = Console.ReadKey().Key;
			return (option == ConsoleKey.Enter);
		}
		public static bool legeRekeningCheck(Bestelling deRekening)
        {
			return (deRekening.gerechten.Count == 0 && deRekening.Desserts.Count == 0 && deRekening.Dranken.Count == 0);
        }
		public static string rekeningCheck(Bestelling bes)
		{
			//nice.
			if (bes.Dranken.Count != 0) return bes.Dranken[0].Aantal == 69 && bes.Dranken[0].Dranknaam == "Mystery Cocktail..." && bes.Tafel == "SIGMA" ? "Nice.\n" : "";
			else return "";
		}
	}
}
