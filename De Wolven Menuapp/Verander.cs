using System;
using System.Globalization;
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
            Console.WriteLine("RESERVERINGEN INZIEN\n");
            while (true)
            {
                var reserveringJson = File.ReadAllText(GetFilePath.Dir("reserveringenbestand.json"));
                reserveringenRoot reserveringsData = JsonConvert.DeserializeObject<reserveringenRoot>(reserveringJson);
                EnkeleReservering res = new EnkeleReservering();
                for (int i = 1; i < reserveringsData.Reserveringen.Count; i++)
                {
                    res = reserveringsData.Reserveringen[i];
                    Console.WriteLine($"Reservering op naam {res.Name}");
                    Console.WriteLine($"{res.Date} | {res.Time} | Code: {res.Code} | {res.CountofPeople} mensen\n");
                }
                Console.WriteLine("[Elke toets] Terug naar vorige menu");
                ConsoleKey terug = Console.ReadKey().Key;
                break;
            }
            
        }

        public static void VeranderenReservering()
        {
            DisplayReserveringen();

            var reserveringJson = File.ReadAllText(GetFilePath.Dir("reserveringenbestand.json"));
            reserveringenRoot reserveringsData = JsonConvert.DeserializeObject<reserveringenRoot>(reserveringJson);

            Console.WriteLine("Voer de naam in van de reservering die je wilt veranderen: \n");
            string welkenaam = Console.ReadLine();
            for (int i = 0; i < reserveringsData.Reserveringen.Count; i++) // ga elke reservering af
            {
                if (reserveringsData.Reserveringen[i].Name == welkenaam) // komt de naam overeen?
                {
                    Console.WriteLine("Voor wie is de reservering, voer de naam in:");
                    string changeName = Console.ReadLine();
                    reserveringsData.Reserveringen[i].Name = changeName;


                    Console.WriteLine($"Voor welke datum is de reservering van {changeName} \n(Het gewenste formaat is: dag/maand/jaar (voorbeeld: 24/05/2022).)\n Let op, eerdere datums worden niet geaccepteerd!");
                    string changeDate;
                    changeDate = Console.ReadLine();


                    // variabel om te checken of de reservering op dezelfde dag valt dat je reserveert. Zodat je bij de tijdsvalidatie een extra check kan uitvoeren.
                    bool zelfdeDag = false;

                    // functie die bekijkt of de ingevoerde datum wel overeenkomt met het gewenste formaat en of de ingevoerde datum niet eerder is dan de huidige datum.
                    bool funcDatumValidatie(string dateToValidate)
                    {
                        DateTime d;
                        bool dateValidatie = DateTime.TryParseExact(
                        dateToValidate,
                        "dd/MM/yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out d);

                        // check of de reservering op dezelfde dag valt dat je reserveert. Ik vraag bij de tijds check deze waarde.
                        if (DateTime.ParseExact(dateToValidate, "dd/MM/yyyy", CultureInfo.InvariantCulture) == DateTime.Now.Date)
                        {
                            zelfdeDag = true;
                        }

                        // als de ingevoerde datum eerder is dan de huidige datum dan zet hij de validatie bool op false.
                        if (DateTime.ParseExact(dateToValidate, "dd/MM/yyyy", CultureInfo.InvariantCulture) < DateTime.Now.Date)
                        {
                            dateValidatie = false;
                        }

                        return dateValidatie;

                    }

                    // Check of de datum wel voldoet na de Validatie Functie, zo niet vraagt ie om een nieuwe input en doet hij de validatie opnieuw
                    bool dateVoldoet = funcDatumValidatie(changeDate);
                    while (!dateVoldoet)
                    {
                        Console.WriteLine("Sorry uw datum ( " + changeDate + " ) voldoet niet aan ons formaat of is al geweest.\n Dit is het gewenste formaat: dag/maand/jaar (voorbeeld: 24/05/2022).\n Voer uw datum nogmaals in.");
                        changeDate = Console.ReadLine();
                        dateVoldoet = funcDatumValidatie(changeDate);

                    }

                    reserveringsData.Reserveringen[i].Date = changeDate;


                    Console.WriteLine($"Hoelaat is de reservering? (Voer de tijd in volgens het volgende formaat: 15:43 )\n");

                    string changeTime = Console.ReadLine();


                    // valideert of de ingevulde tijd wel klopt qua formaat en als je voor vandaag reserveert dat die tijd niet al geweest is
                    bool funcTijdValidatie(string time, string format = "HH:mm")
                    {
                        DateTime outTime;

                        // Checkt als je reserveert voor vandaag en checkt dan of die tijd niet al geweest is.

                        if (zelfdeDag && (DateTime.ParseExact(time, format, CultureInfo.InvariantCulture, DateTimeStyles.None) < DateTime.Now)) { return false; }

                        return DateTime.TryParseExact(time, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out outTime);
                    }

                    bool tijdVoldoet = funcTijdValidatie(changeTime);

                    // Check of de tijd wel voldoet na de Validatie Functie, zo niet vraagt ie om een nieuwe input en doet hij de validatie opnieuw
                    while (!tijdVoldoet)
                    {
                        Console.WriteLine("Sorry uw tijd ( " + changeTime + " ) voldoet niet aan ons formaat of is al geweest (als u voor vandaag reserveert).\n Dit is het gewenste formaat: 15:43 \n Voer uw tijd nogmaals in.");
                        changeTime = Console.ReadLine();
                        tijdVoldoet = funcTijdValidatie(changeTime);

                    }

                    reserveringsData.Reserveringen[i].Time = changeTime;


                    Console.WriteLine("Voor hoeveel mensen wilt u reserveren");
                    int changeCountofPeople = Convert.ToInt32(Console.ReadLine());
                    reserveringsData.Reserveringen[i].CountofPeople = changeCountofPeople;

                    var updatedReservations = JsonConvert.SerializeObject(reserveringsData, Formatting.Indented);
                    File.WriteAllText("reserveringenbestand.json", updatedReservations);

                }
            }
            
        }
        public static void reserveringAanpassenKlant()
        {
            EnkeleReservering geselecteerdeReservering;
            
            // reserveringen inlezen
            var reserveringJson = File.ReadAllText(GetFilePath.Dir("reserveringenbestand.json"));
            reserveringenRoot reserveringsData = JsonConvert.DeserializeObject<reserveringenRoot>(reserveringJson);

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
                        Console.WriteLine("U verandert de datum van uw reservering. \n(Het gewenste formaat is: dag/maand/jaar (voorbeeld: 24/05/2022).)" +
                            "\n Let op, eerdere datums worden niet geaccepteerd!\n Voer nu uw nieuwe datum in:\n");
                        
                        geselecteerdeReservering.Date = Console.ReadLine();


                        // variabel om te checken of de reservering op dezelfde dag valt dat je reserveert. Zodat je bij de tijdsvalidatie een extra check kan uitvoeren.
                        bool zelfdeDag = false;

                        // functie die bekijkt of de ingevoerde datum wel overeenkomt met het gewenste formaat en of de ingevoerde datum niet eerder is dan de huidige datum.
                        bool funcDatumValidatie(string dateToValidate)
                        {
                            DateTime d;
                            bool dateValidatie = DateTime.TryParseExact(
                            dateToValidate,
                            "dd/MM/yyyy",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out d);


                            try
                            {
                                // als de ingevoerde datum eerder is dan de huidige datum dan zet hij de validatie bool op false.
                                if (DateTime.ParseExact(dateToValidate, "dd/MM/yyyy", CultureInfo.InvariantCulture) < DateTime.Now.Date)
                                {
                                    dateValidatie = false;
                                }
                            }
                            

                            catch (Exception ex)
                            {
                                dateValidatie = false;
                                return dateValidatie;

                            }

                            return dateValidatie;

                        }

                        // Check of de datum wel voldoet na de Validatie Functie, zo niet vraagt ie om een nieuwe input en doet hij de validatie opnieuw
                        bool dateVoldoet = funcDatumValidatie(geselecteerdeReservering.Date);
                        while (!dateVoldoet)
                        {
                            Console.WriteLine("Sorry uw datum ( " + geselecteerdeReservering.Date + " ) voldoet niet aan ons formaat of is al geweest.\n Dit is het gewenste formaat: dag/maand/jaar (voorbeeld: 24/05/2022).\n Voer uw datum nogmaals in.");
                            geselecteerdeReservering.Date = Console.ReadLine();
                            dateVoldoet = funcDatumValidatie(geselecteerdeReservering.Date);

                        }

                        
                        Console.WriteLine($"U heeft de datum aangepast naar: {geselecteerdeReservering.Date}");
                    }
                    else if (welkVeldAanpassen == ConsoleKey.D3)
                    {
                        Console.WriteLine($"Hoelaat is de reservering? (Voer de tijd in volgens het volgende formaat: 15:43 )\n U past de tijd aan naar: \n");

                        geselecteerdeReservering.Time = Console.ReadLine();


                        // valideert of de ingevulde tijd wel klopt qua formaat en als je voor vandaag reserveert dat die tijd niet al geweest is
                        bool funcTijdValidatie(string time, string format = "HH:mm")
                        {
                            DateTime outTime;

                            bool zelfdeDag = false;

                            // Checkt als je reserveert voor vandaag en checkt dan of die tijd niet al geweest is.
                            if (DateTime.ParseExact(geselecteerdeReservering.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture) == DateTime.Now.Date)
                            {
                                zelfdeDag = true;
                            }

                            if (zelfdeDag && (DateTime.ParseExact(time, format, CultureInfo.InvariantCulture, DateTimeStyles.None) < DateTime.Now)) 
                            { 
                                return false; 
                            }

                            return DateTime.TryParseExact(time, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out outTime);
                        }

                        bool tijdVoldoet = funcTijdValidatie(geselecteerdeReservering.Time);

                        // Check of de tijd wel voldoet na de Validatie Functie, zo niet vraagt ie om een nieuwe input en doet hij de validatie opnieuw
                        while (!tijdVoldoet)
                        {
                            Console.WriteLine("Sorry uw tijd ( " + geselecteerdeReservering.Time + " ) voldoet niet aan ons formaat of is al geweest (als u voor vandaag reserveert).\n Dit is het gewenste formaat: 15:43 \n Voer uw tijd nogmaals in.");
                            geselecteerdeReservering.Time = Console.ReadLine();
                            tijdVoldoet = funcTijdValidatie(geselecteerdeReservering.Time);

                        }

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
                    File.WriteAllText(GetFilePath.Dir("reserveringenbestand.json"), updatedReservations);

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
