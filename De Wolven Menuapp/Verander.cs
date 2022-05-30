using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace De_Wolven_Menuapp
{
    internal class Verander
    {
        public static void DisplayReserveringen()
        {
            var reserveringJson = File.ReadAllText("reserveringenbestand.json");
            Information reserveringsData = JsonConvert.DeserializeObject<Information>(reserveringJson);
            for(int i=0; i < reserveringsData.Reserveringen.Count; i++)
            {
                Console.WriteLine(reserveringsData.Reserveringen[i].Name);
                Console.WriteLine(reserveringsData.Reserveringen[i].Date);
                Console.WriteLine(reserveringsData.Reserveringen[i].Time);
                Console.WriteLine(reserveringsData.Reserveringen[i].Code);
                Console.WriteLine(reserveringsData.Reserveringen[i].CountofPeople);
            }
            
        }

        public static void VeranderenReservering()
        {
            DisplayReserveringen();

            var reserveringJson = File.ReadAllText("reserveringenbestand.json");
            Information reserveringsData = JsonConvert.DeserializeObject<Information>(reserveringJson);

            Console.WriteLine("Voer de naam in van de reservering die je wilt veranderen: \n");
            string welkenaam = Console.ReadLine();
            for (int i = 0; i < reserveringsData.Reserveringen.Count; i++) // ga elke reservering af
            {
                if (reserveringsData.Reserveringen[i].Name == welkenaam) // komt de naam overeen?
                {
                    Console.WriteLine("Voor wie is de reservering, voer de naam in:");
                    string changeName = Console.ReadLine();
                    reserveringsData.Reserveringen[i].Name = changeName;
                    Console.WriteLine($"Voor welke datum is de reservering van {changeName}");
                    string changeDate = Console.ReadLine();
                    reserveringsData.Reserveringen[i].Date = changeDate;
                    Console.WriteLine($"Voor welk tijdstip is de reservering?");
                    string changeTime = Console.ReadLine();
                    reserveringsData.Reserveringen[i].Time = changeTime;
                    Console.WriteLine("Voor hoeveel mensen wilt u reserveren");
                    int changeCountofPeople = Convert.ToInt32(Console.ReadLine());
                    reserveringsData.Reserveringen[i].CountofPeople = changeCountofPeople;

                    var updatedReservations = JsonConvert.SerializeObject(reserveringsData, Formatting.Indented);
                    File.WriteAllText("reserveringenbestand.json", updatedReservations);
                    Console.WriteLine(updatedReservations);
                    Console.ReadLine();

                }
            }
            
        }
        public static void reserveringAanpassenKlant()
        {
            EnkeleReservering geselecteerdeReservering;
            
            // reserveringen inlezen
            var reserveringJson = File.ReadAllText("reserveringenbestand.json");
            Information reserveringsData = JsonConvert.DeserializeObject<Information>(reserveringJson);

            // vraag klant om code van reservering
            Console.WriteLine("Wat is de code van uw reservering?\nDeze heeft u ontvangen na het maken van uw reservering.\nVoorbeeld: 32767\n");
            int opgegevenCode = Convert.ToInt32(Console.ReadLine()); // er moet nog een controle unit bij zodat de code juist is ingevoerd

            // ga elke reservering af
            for (int i = 0; i < reserveringsData.Reserveringen.Count; i++)
            {
                // als de ingevoerde code overeenkomt, laat reservering zien
                if (opgegevenCode == reserveringsData.Reserveringen[i].Code)
                {
                    geselecteerdeReservering = reserveringsData.Reserveringen[i];

                    Console.Clear();
                    laatEenReserveringZien(geselecteerdeReservering);
                    Console.WriteLine("Welke wilt u aanpassen? Voer 1, 2, 3 of 4 in.\n");
                    
                    // keuzemenu voor welk veld aangepast moet worden
                    ConsoleKey welkVeldAanpassen = Console.ReadKey().Key;
                    if (welkVeldAanpassen == ConsoleKey.D1)
                    {
                        Console.WriteLine("U past de naam aan naar: \n");
                        geselecteerdeReservering.Name = Console.ReadLine();
                        Console.WriteLine($"U heeft de naam aangepast naar: {geselecteerdeReservering.Name}");
                    } else if (welkVeldAanpassen == ConsoleKey.D2)
                    {
                        Console.WriteLine("U past de datum aan naar: \n");
                        geselecteerdeReservering.Date = Console.ReadLine();
                        Console.WriteLine($"U heeft de datum aangepast naar: {geselecteerdeReservering.Date}");
                    }
                    else if (welkVeldAanpassen == ConsoleKey.D3)
                    {
                        Console.WriteLine("U past de tijd aan naar: \n");
                        geselecteerdeReservering.Time = Console.ReadLine();
                        Console.WriteLine($"U heeft de tijd aangepast naar: {geselecteerdeReservering.Time}");
                    }
                    else if (welkVeldAanpassen == ConsoleKey.D4)
                    {
                        Console.WriteLine("U past de hoeveelheid mensen aan naar: \n");
                        geselecteerdeReservering.CountofPeople = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine($"U heeft de hoeveelheid mensen aangepast naar: {geselecteerdeReservering.CountofPeople}");
                    }
                    else
                    {
                        Console.WriteLine("Geen geldige input");
                    }

                    // schrijf geüpdate reservering terug naar json
                    reserveringsData.Reserveringen[i] = geselecteerdeReservering;
                    var updatedReservations = JsonConvert.SerializeObject(reserveringsData, Formatting.Indented);
                    File.WriteAllText("reserveringenbestand.json", updatedReservations);

                    laatEenReserveringZien(reserveringsData.Reserveringen[i]);
                }
            }
            // terug naar hoofdmenu
            Console.WriteLine("Druk op Enter om terug te gaan naar het hoofdmenu.");
            ConsoleKey terugNaarKlantenMenuToets = Console.ReadKey().Key;

            if (terugNaarKlantenMenuToets == ConsoleKey.Enter) Hoofdmenuscherm.SchermKlanten();
            else Hoofdmenuscherm.SchermKlanten(); // programmapad moet nog gefixt worden, hij gaat nu altijd terug naar het klantenhoofdmenu
        }
        
        // deze method accepteert een object van de class EnkeleReservering en laat die netjes zien aan de gebruiker
        public static void laatEenReserveringZien(EnkeleReservering deReserveringDieJeWiltZien)
        {
            Console.WriteLine($"De reservering op de naam {deReserveringDieJeWiltZien.Name} wordt weergeven.");
            Console.WriteLine("[1] Naam: " + deReserveringDieJeWiltZien.Name);
            Console.WriteLine("[2] Datum: " + deReserveringDieJeWiltZien.Date);
            Console.WriteLine("[3] Tijd: " + deReserveringDieJeWiltZien.Time);
            Console.WriteLine("[4] Aantal mensen: " + deReserveringDieJeWiltZien.CountofPeople);
            Console.WriteLine($"Code: {deReserveringDieJeWiltZien.Code}");
        }
    }  
}
