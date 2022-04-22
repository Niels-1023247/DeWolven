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
            Console.WriteLine("Voer de naam in van de reservering die je wilt veranderen: \n");
            string welkenaam = Console.ReadLine();

        }
    }  
}
