using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace De_Wolven_Menuapp
{
    internal class OurTable
    {
        /* Maak NewDate method
         * maak NewTime method
         */
        public static void NewDate()
        {

        }
        public static void NewTime() 
        { 

        }
        public static void AddTable(EnkeleReservering Resv)
        {
            var JsonString = File.ReadAllText("OurTable.json");
            var DeserialisedResult = JsonConvert.DeserializeObject<Datum>(JsonString);
            string CurrentDate = Resv.Date;
            string CurrentTime = Resv.Time;
            bool datePresent = DeserialisedResult.Data.ContainsKey(CurrentDate);
            if (!datePresent) { NewDate(); }
            bool timePresent = DeserialisedResult.Data[CurrentDate].Tijdblok.ContainsKey(CurrentTime);
            if (!timePresent) { NewTime(); }
            Tafels newTables = new()
            {
                BeschTaf2 = DeserialisedResult.Data[CurrentDate].Tijdblok[CurrentTime].BeschTaf2,
                BeschTaf4 = DeserialisedResult.Data[CurrentDate].Tijdblok[CurrentTime].BeschTaf4,
                BeschTaf6 = DeserialisedResult.Data[CurrentDate].Tijdblok[CurrentTime].BeschTaf6,
            };
            if (Resv.CountofPeople<2 && Resv.CountofPeople > 0)
            {
                if (DeserialisedResult.Data[CurrentDate].Tijdblok[CurrentTime].BeschTaf2 == 0)
                {
                    if (DeserialisedResult.Data[CurrentDate].Tijdblok[CurrentTime].BeschTaf4 == 0)
                    {
                        if (DeserialisedResult.Data[CurrentDate].Tijdblok[CurrentTime].BeschTaf6 == 0) { Console.WriteLine("Geen zitplaatsen meer over"); }
                        else { newTables.BeschTaf6--; }
                    }
                    else { newTables.BeschTaf4--; }
                }
                else { newTables.BeschTaf2--; }
            }
            else if (Resv.CountofPeople>2 && Resv.CountofPeople <= 4)
            {
                if (DeserialisedResult.Data[CurrentDate].Tijdblok[CurrentTime].BeschTaf4 == 0)
                {
                    if (DeserialisedResult.Data[CurrentDate].Tijdblok[CurrentTime].BeschTaf6 == 0) { Console.WriteLine("Geen zitplaatsen meer over"); }
                    else { newTables.BeschTaf6--; }
                }
                else { newTables.BeschTaf4--; }
            }
            else if (Resv.CountofPeople>4&&Resv.CountofPeople <= 5)
            {
                if (DeserialisedResult.Data[CurrentDate].Tijdblok[CurrentTime].BeschTaf6 == 0) { Console.WriteLine("Geen zitplaatsen meer over"); }
                else { newTables.BeschTaf6--; }
            }
        }
    }
}
