using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace De_Wolven_Menuapp
{
    internal class Contact
    {
        // laat contactgegevens zien
        public static void Contactgegevens()
        {

            var JsonString = File.ReadAllText(GetFilePath.Dir("ContactInfo.json"));
            var DeserialisedResult = JsonConvert.DeserializeObject<ContactInfo>(JsonString);
            while (true)
            {

                ConsoleKey input;
                Console.Clear();
                Console.WriteLine("De Wolven");
                Console.WriteLine("CONTACT");
                Console.WriteLine($"Adres: {DeserialisedResult.Address}");
                Console.WriteLine($"Telefoonnumer: {DeserialisedResult.Phone}");
                Console.WriteLine("\n\nOPENINGSTIJDEN");
                Console.WriteLine("Openigtijden:\n");
                Console.WriteLine($"Maandag:   {DeserialisedResult.OpenT.Monday}");
                Console.WriteLine($"Dinsdag:   {DeserialisedResult.OpenT.Tuesday}");
                Console.WriteLine($"Woensdag:  {DeserialisedResult.OpenT.Wednesday}");
                Console.WriteLine($"Donderdag: {DeserialisedResult.OpenT.Thursday}");
                Console.WriteLine($"Vrijdag:   {DeserialisedResult.OpenT.Friday}");
                Console.WriteLine($"Zaterdag:  {DeserialisedResult.OpenT.Saturday}");
                Console.WriteLine($"Zondag:    {DeserialisedResult.OpenT.Sunday}");
                Console.WriteLine("\nDrup op Esc om terug te gaan");
                input = Console.ReadKey().Key;
                if (input == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }
        // kies welke contactgegevens veranderd zullen worden
        public static void ChangeInfoMenu()
        {
            while (true)
            {
                Console.WriteLine("Wat wilt u veranderen?\n[1]Adres\n[2]Telefoonnummer\n[3]openingstijden\n\nDruk op Esc om terug te gaan");
                var input = Console.ReadKey().Key;
                Console.WriteLine("");
                if (input == ConsoleKey.D1) { ChangeInfo(1); }
                else if (input == ConsoleKey.D2) { ChangeInfo(2); }
                else if (input == ConsoleKey.D3) { ChangeInfo(3); }
                else if (input == ConsoleKey.Escape) { break; }
            }
        }
        // functie om de gegevens per soort (adres/telefoonnummer/openingstijden) te veranderen op basis van variable i
        public static void ChangeInfo(int i)
        { 
            var JsonString = File.ReadAllText(GetFilePath.Dir("ContactInfo.json"));
            var DeserialisedResult = JsonConvert.DeserializeObject<ContactInfo>(JsonString);
            if (i == 1)
            {
                Console.WriteLine("Geef een nieuw adres:\nVOORBEELD:VoorbeeldStraat 123, 1234AB Rotterdam\n");
                var newAddress = new ContactInfo
                {
                    Address = Console.ReadLine(),
                    Phone = DeserialisedResult.Phone,
                    OpenT = DeserialisedResult.OpenT,
                };
                var AdJsonString = JsonConvert.SerializeObject(newAddress, Formatting.Indented);
                File.WriteAllText(GetFilePath.Dir("ContactInfo.json"), AdJsonString);
            }
            if (i == 2)
            {
                Console.WriteLine("Geef een nieuw Telefoonnummer:\nVOORBEELD: 010-1234567\n");
                var newPhone = new ContactInfo
                {
                    Address= DeserialisedResult.Address,
                    Phone = Console.ReadLine(),
                    OpenT = DeserialisedResult.OpenT,
                };
                var PhJsonString = JsonConvert.SerializeObject(newPhone, Formatting.Indented);
                File.WriteAllText(GetFilePath.Dir("ContactInfo.json"), PhJsonString);
            }
            if (i == 3)
            {
                Console.WriteLine("Wat zijn de nieuwe openingstijden?\nVOORBEELD: 0:00-23:59\n0:00-23:00\n Vul een tijd in voor iedere dag\n");
                Console.WriteLine("Ma: "); var Monday=Console.ReadLine();
                Console.WriteLine("Di: "); var Tuesday = Console.ReadLine();
                Console.WriteLine("Wo: "); var Wednesday = Console.ReadLine();
                Console.WriteLine("Do: "); var Thursday = Console.ReadLine();
                Console.WriteLine("Vr: "); var Friday = Console.ReadLine();
                Console.WriteLine("Za: "); var Saturday = Console.ReadLine();
                Console.WriteLine("Zo: "); var Sunday = Console.ReadLine();
                var newOpent = new OpeningsTijden
                {
                    Monday = Monday,
                    Tuesday = Tuesday,
                    Wednesday = Wednesday,
                    Thursday = Thursday,
                    Friday = Friday,
                    Saturday = Saturday,
                    Sunday = Sunday
                };
                var NewOpenT = new ContactInfo
                {
                    Address = DeserialisedResult.Address,
                    Phone = DeserialisedResult.Phone,
                    OpenT = newOpent
                };
                var OpTJsonString = JsonConvert.SerializeObject(NewOpenT, Formatting.Indented);
                File.WriteAllText(GetFilePath.Dir("ContactInfo.json"), OpTJsonString);

            }
            
            //var newOpen = new OpeningsTijden
            //{
            //    Monday = Console.ReadLine(),
            //    Tuesday = Console.ReadLine(),
            //    Wednesday = Console.ReadLine(),
            //    Thursday = Console.ReadLine(),
            //    Friday = Console.ReadLine(),
            //    Saturday = Console.ReadLine(),
            //    Sunday = Console.ReadLine()
            //};
            //var newContactInfo = new ContactInfo
            //{
            //    Address = Console.ReadLine(),
            //    Phone = Console.ReadLine(),
            //    OpenT = newOpen,
            //};
            
            //var writeJsonString = JsonConvert.SerializeObject(newContactInfo, Formatting.Indented);
            //File.WriteAllText("ContactInfo.json",writeJsonString);
            
        }
    }
}
