using System;
using System.Collections.Generic;
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
            Console.WriteLine("Voor welke tafel moet er een bestelling opgenomen worden?");
            Console.WriteLine("Type het tafelnummer...");
            string tafelNummer = Console.ReadLine();
            Console.Clear();

            Bestelling nieuweBestelling = new()
            {
                Tafel = tafelNummer,
                gerechten = new List<Gerechten>(),
                Desserts = new List<MenuDesserts>(),
                Dranken = new List<MenuDranken>()
            };


            bestellenInWelkeCategorie(nieuweBestelling);

        }

        public static void bestellenInWelkeCategorie(Bestelling huidigeBestelling)
        {
            Console.Clear();
            Console.WriteLine($"NIEUWE BESTELLING TOEVOEGEN VOOR TAFEL {huidigeBestelling.Tafel}\n");
            Console.WriteLine("Kies van welke categorie u wilt bestellen...");
            Console.WriteLine("[1] Gerechten");
            Console.WriteLine("[2] Desserts");
            Console.WriteLine("[3] Dranken");
            Console.WriteLine("Voer 1, 2 of 3 in");

            if (huidigeBestelling.gerechten.Count != 0 | huidigeBestelling.Desserts.Count != 0 | huidigeBestelling.Dranken.Count != 0)
            {
                Console.WriteLine($"\nAan het toevoegen voor tafel {huidigeBestelling.Tafel}:");
                
                for (int i = 0; i < huidigeBestelling.gerechten.Count; i++) Console.WriteLine($"{huidigeBestelling.gerechten[i].Aantal}x {huidigeBestelling.gerechten[i].Gerechtnaam}");
                for (int i = 0; i < huidigeBestelling.Desserts.Count; i++) Console.WriteLine($"{huidigeBestelling.Desserts[i].Aantal}x {huidigeBestelling.Desserts[i].Dessertnaam}");
                for (int i = 0; i < huidigeBestelling.Dranken.Count; i++) Console.WriteLine($"{huidigeBestelling.Dranken[i].Aantal}x {huidigeBestelling.Dranken[i].Dranknaam}");
                
                Console.WriteLine();
                Console.WriteLine("Druk op Enter om bovenstaanden definitief aan de rekening toe te voegen.");
                Console.WriteLine("Druk op Escape om terug te gaan naar het hoofdmenu en deze items weg te gooien.");


            }



            ConsoleKey Bestellen = Console.ReadKey().Key;
            if (Bestellen == ConsoleKey.D1)
            {
                Console.Clear();
                bestellingKiesOptie(1, huidigeBestelling, "", 0);
            }

            else if (Bestellen == ConsoleKey.D2)
            {
                Console.Clear();
                bestellingKiesOptie(2, huidigeBestelling, "", 0);
            }

            else if (Bestellen == ConsoleKey.D3)
            {
                Console.Clear();
                bestellingKiesOptie(3, huidigeBestelling, "", 0);
            }
            else if (Bestellen == ConsoleKey.Escape) // terug naar hoofdmenu
            {
                Console.Clear();
                medewerkerHome.SchermMedewerker($"De voorgenoemde items van tafel {huidigeBestelling.Tafel} zijn weggegooid.");
            }
            else if (Bestellen == ConsoleKey.Enter)
            {
                Console.Clear();
                bestellingDoorvoeren(huidigeBestelling);
            }
            else
            {
                Console.Clear();
                bestellenInWelkeCategorie(huidigeBestelling);

            }
        }

        //onderstaande method leest de menukaart in
        public static void bestellingKiesOptie(int categorie, Bestelling nieuweItems, string laatsteItem = "", int laatsteItemHvh = 0)
        {
            // laadbericht
            Console.Clear();
            Console.WriteLine("Menu inlezen...");

            // menukaart inlezen
            string menukaartJson = File.ReadAllText("Menukaart.JSON");
            var menuData = JsonConvert.DeserializeObject<Menukaart>(menukaartJson);

            // bestellingen inlezen
            string bestellingenJson = File.ReadAllText("bestellingen.JSON");
            var bestellingenData = JsonConvert.DeserializeObject<bestellingenRoot>(bestellingenJson);

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
            
            bool active = true;

            while (active)
            {
                
                // weergeven menu items van juiste pagina
                Console.WriteLine($"MENUKAART - {categorienaam.ToUpper()} KIEZEN");
                Console.WriteLine($"Pagina {screen+1} van {pgmax+1}");
                Console.WriteLine(laatsteItemHvh != 0 ? $"[!] {laatsteItemHvh}x {laatsteItem} toegevoegd aan rekening voor tafel [1A]" : ""); // optioneel laatst toegevoegd item
                Console.WriteLine("\n\n");

                for (int i = 8 * screen, c = 1; c <= 8 | i <= max-1; i++, c++) // c is het nummer voor de bestelling [c] weergeven, i is de index van het gerecht in de json
                {
                    if (i < max && c < 9)
                    {
                        if (categorie == 1) Console.WriteLine("[" + c + "] " + menuData.gerechten[i].Gerechtnaam + ", " + menuData.gerechten[i].Prijs + " euro, " + menuData.gerechten[i].Allergenen);
                        else if (categorie == 2) Console.WriteLine("[" + c + "] " + menuData.Desserts[i].Dessertnaam + ", " + menuData.Desserts[i].Prijs + " euro, " + menuData.Desserts[i].Allergenen);
                        else if (categorie == 3) Console.WriteLine("[" + c + "] " + menuData.Dranken[i].Dranknaam + ", " + menuData.Dranken[i].Prijs + " euro, " + menuData.Dranken[i].Allergenen);

                        hoeveelheidOpties = c;

                    }
                }

                Console.WriteLine($"\n\nDruk op het gewenste nummer om uw {categorienaam} te selecteren.\nDruk op de pijltjestoetsen om van pagina te wisselen, \nDruk op Escape om terug te gaan.");
                
                //Console.WriteLine(nieuweItemsCommitten != null ? "Druk op Enter om ");
                
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
                else if (input == ConsoleKey.Escape) // terug naar hoofdmenu
                {
                    active = false;
                    Console.Clear();
                    bestellenInWelkeCategorie(nieuweItems);
                }
                else if (input == ConsoleKey.RightArrow & pgmax == screen) // als je na het laatste scherm naar rechts gaat dan gaat hij terug naar het eerste scherm
                {
                    screen = 0;
                    Console.Clear();
                }
                
                else if (opties.Contains(input)) // als de gebruiker een item van het menu kiest...
                {
                    Console.Clear();

                    //Console.WriteLine(menuData.gerechten[menuItemIndexOphalen(screen, input)].Gerechtnaam);
                    //else if (categorie == 2) Console.WriteLine(menuData.Desserts[menuItemIndexOphalen(screen, input)].Dessertnaam;
                    //else if (categorie == 3) Console.WriteLine(menuData.Dranken[menuItemIndexOphalen(screen, input)].Dranknaam);

                    //var x = menuData.gerechten[1];

                    string recentItemNaam = "";
                    int recentItemHvh = 0;

                    // het is nog niet geprogrammeerd dat de item aan het aantal wordt toegevoegd als ie al aanwezig is.

                    if (categorie == 1) // nieuw item toevoegen aan lege bestelling dummy
                    {
                        int rightIndex = menuItemIndexOphalen(screen, input); // juiste index van menukaart ophalen
                        nieuweItems.gerechten.Add(menuData.gerechten[rightIndex]); // menu item aan lege dummy bestelling toevoegen
                        Console.WriteLine($"U heeft gekozen voor {menuData.gerechten[menuItemIndexOphalen(screen, input)].Gerechtnaam}.");
                        nieuweItems.gerechten[nieuweItems.gerechten.Count-1].Aantal = kiesHoeveelheidKeuze(); // prompt hoeveel er besteld moeten worden

                        recentItemNaam = nieuweItems.gerechten[nieuweItems.gerechten.Count - 1].Gerechtnaam; 
                        recentItemHvh = nieuweItems.gerechten[nieuweItems.gerechten.Count - 1].Aantal;

                    }
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
                    bestellingKiesOptie(categorie, nieuweItems, recentItemNaam, recentItemHvh);



                    // je voert een nummer in
                    // dan hoeveel je er wilt, met een lijn onder die optie
                    // met de pijltjes links en rechts selecteer je hoeveel
                    // dan druk je op enter of op escape om te bevestigen
                    // om de bestelling definitief op de rekening te zetten..
                    // terug naar selecteer categorie, dan op enter.
                    // dan gaat hij terug naar het medewerkersscherm

                }

                else
                {
                    bestellingKiesOptie(categorie, nieuweItems);
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
        
        public static int menuItemIndexOphalen(int welkScherm, ConsoleKey key)
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

            int juisteIndex = (welkScherm * 8) + (c - 1);
            return juisteIndex;
        }
        
        public static int kiesHoeveelheidKeuze()
        {
            Console.WriteLine("Geef aan hoeveel er u voor deze tafel wilt bestellen.");

            int aantalVanOptie = Convert.ToInt32(Console.ReadLine());

            return aantalVanOptie;

        }

        public static void bestellingDoorvoeren(Bestelling nieuweToevoeging)
        {
            
            // bestellingen inlezen
            string bestellingenJson = File.ReadAllText("bestellingen.JSON");
            var bestellingenData = JsonConvert.DeserializeObject<bestellingenRoot>(bestellingenJson);

            bool maakNieuweRekening = true;

            // appenden aan bestaande rekening
            for (int i = 0; i < bestellingenData.Bestellingen.Count(); i++) 
            {
                if (bestellingenData.Bestellingen[i].Tafel == nieuweToevoeging.Tafel) // is de tafel gevonden?
                {
                    for (int j = 0; j < nieuweToevoeging.gerechten.Count; j++) bestellingenData.Bestellingen[i].gerechten.Add(nieuweToevoeging.gerechten[j]);
                    for (int j = 0; j < nieuweToevoeging.Desserts.Count; j++) bestellingenData.Bestellingen[i].Desserts.Add(nieuweToevoeging.Desserts[j]);
                    for (int j = 0; j < nieuweToevoeging.Dranken.Count; j++) bestellingenData.Bestellingen[i].Dranken.Add(nieuweToevoeging.Dranken[j]);
                    maakNieuweRekening = false;
                }
            }
            
            // nieuwe rekening aanmaken ipv aan bestaande toevoegen
            if (maakNieuweRekening) bestellingenData.Bestellingen.Add(nieuweToevoeging);

            // bestellingen met geupdate items naar disk schrijven
            var geupdateBestellingen = JsonConvert.SerializeObject(bestellingenData, Formatting.Indented);
            File.WriteAllText("bestellingen.json", geupdateBestellingen);

            // terug naar hoofdmenu
            medewerkerHome.SchermMedewerker($"Succesvol de nieuwe bestellingen aan de rekening van tafel {nieuweToevoeging.Tafel} toegevoegd!");

        }

    }
}
