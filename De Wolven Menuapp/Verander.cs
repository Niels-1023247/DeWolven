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
                if (reserveringsData.Reserveringen[i].Name == welkenaam) // komt de nieuwe code overeen?
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
                    string changeCountofPeople = Console.ReadLine();
                    reserveringsData.Reserveringen[i].CountofPeople = changeCountofPeople;

                    var updatedReservations = JsonConvert.SerializeObject(reserveringsData, Formatting.Indented);
                    File.WriteAllText("reserveringenbestand.json", updatedReservations);
                    Console.WriteLine(updatedReservations);
                    Console.ReadLine();

                }
            }
            
        }
    }  
}
