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
        
        public static void NewDate(string themDates, string themTimes)
        {
            //lees JSON, 
            var JstringTmp = File.ReadAllText("OurTable.json");
            var LeOld = JsonConvert.DeserializeObject<Datum>(JstringTmp);
            //voeg lege datum toe
            LeOld.Data.Add(themDates,null);
            var NewDate=JsonConvert.SerializeObject(LeOld,Formatting.Indented);
            File.WriteAllText("OurTable.json", NewDate);
            //naar newTime
            NewTime(themDates, themTimes);
        }
        public static void NewTime(string themDates,string themTimes) 
        {
            //lees JSON
            var JstringTmp= File.ReadAllText("OurTable.json");
            var LeOld = JsonConvert.DeserializeObject<Datum>(JstringTmp);
            //maak standaard tafels aan voor een uurblok
            var theBase = new Tafels()
            {
                BeschTaf2 = 5,
                BeschTaf4 = 4,
                BeschTaf6 = 5
            };
            //maak lege tijd aan
            var temp = new Tijd()
            {
                Tijdblok=new Dictionary<string, Tafels> { { themTimes,theBase} }
            };
            //zorgt ervoor dat er een nieuwe tijd wordt toegevoegd als er al een tijd in de datum staat
            if (LeOld.Data[themDates] == null) {
                LeOld.Data[themDates] = temp;
            }
            else
            {
                LeOld.Data[themDates].Tijdblok.Add(themTimes, theBase);
            }
            var NewTime = JsonConvert.SerializeObject(LeOld, Formatting.Indented);
            File.WriteAllText("OurTable.json", NewTime);
        }

        public static void AddTable(EnkeleReservering Resv)
        {
            var JsonString = File.ReadAllText("OurTable.json");
            var DeserialisedResult = JsonConvert.DeserializeObject<Datum>(JsonString);
            //lees  datum en tijd van de ingev. reservering
            string CurrentDate = Resv.Date;
            string CurrentTime = Resv.Time;
            //checkt of er al een datum in de JSON staat
            bool datePresent = DeserialisedResult.Data.ContainsKey(CurrentDate);
            //naar NewDate
            if (!datePresent) { NewDate(CurrentDate,CurrentTime); }
            //refresh de json in memory met de nieuwe informatie
            JsonString = File.ReadAllText("OurTable.json");
            DeserialisedResult = JsonConvert.DeserializeObject<Datum>(JsonString);
            //checkt of er al een tijd in de JSON staat
            bool timePresent = DeserialisedResult.Data[CurrentDate].Tijdblok.ContainsKey(CurrentTime);
            if (!timePresent) { NewTime(CurrentDate,CurrentTime); }
            //refresh de JSON in memory met nieuwe informatie
            JsonString = File.ReadAllText("OurTable.json");
            DeserialisedResult = JsonConvert.DeserializeObject<Datum>(JsonString);
            //maakt nieuwe Tafel(object) aan dat aangepast kan worden
            Tafels newTables = new()
            {
                BeschTaf2 = DeserialisedResult.Data[CurrentDate].Tijdblok[CurrentTime].BeschTaf2,
                BeschTaf4 = DeserialisedResult.Data[CurrentDate].Tijdblok[CurrentTime].BeschTaf4,
                BeschTaf6 = DeserialisedResult.Data[CurrentDate].Tijdblok[CurrentTime].BeschTaf6
            };
            //logica voor indelen tafels
            var themPeeps = Resv.CountofPeople;
            while (themPeeps > 0)
            {
                if (themPeeps <= 2 && themPeeps > 0)
                {
                    if (DeserialisedResult.Data[CurrentDate].Tijdblok[CurrentTime].BeschTaf2 == 0)
                    {
                        if (DeserialisedResult.Data[CurrentDate].Tijdblok[CurrentTime].BeschTaf4 == 0)
                        {
                            if (DeserialisedResult.Data[CurrentDate].Tijdblok[CurrentTime].BeschTaf6 == 0) { Console.WriteLine("Geen zitplaatsen meer over"); }
                            //tafel van beschikbaarheid schrijven
                            else { newTables.BeschTaf6--; break; }
                        }
                        else { newTables.BeschTaf4--; break; }
                    }
                    else { newTables.BeschTaf2--; break; }
                }
                else if (themPeeps > 2 && themPeeps <= 4)
                {
                    if (DeserialisedResult.Data[CurrentDate].Tijdblok[CurrentTime].BeschTaf4 == 0)
                    {
                        if (DeserialisedResult.Data[CurrentDate].Tijdblok[CurrentTime].BeschTaf6 == 0) { Console.WriteLine("Geen zitplaatsen meer over"); }
                        else { newTables.BeschTaf6--; break; }
                    }
                    else { newTables.BeschTaf4--; break; }
                }
                else if (themPeeps > 4 && themPeeps <= 6)
                {
                    if (DeserialisedResult.Data[CurrentDate].Tijdblok[CurrentTime].BeschTaf6 == 0) { Console.WriteLine("Geen zitplaatsen meer over"); }
                    else { newTables.BeschTaf6--; break; }
                }
                else if (themPeeps > 6)
                {
                    if (DeserialisedResult.Data[CurrentDate].Tijdblok[CurrentTime].BeschTaf6 == 0) { Console.WriteLine("Geen zitplaatsen meer over"); }
                    else
                    {
                        if (themPeeps <= 8) { newTables.BeschTaf4 -= 2; break; }
                        else
                        {
                            newTables.BeschTaf6--;
                            themPeeps -= 6;
                        }
                        
                    }
                }
            }
            DeserialisedResult.Data[CurrentDate].Tijdblok[CurrentTime] = newTables;
            var NewStuff = JsonConvert.SerializeObject(DeserialisedResult, Formatting.Indented);
            File.WriteAllText("OurTable.json", NewStuff);
        }
    }
}