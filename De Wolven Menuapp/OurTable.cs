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
        public static void AddTable(EnkeleReservering Resv)
        {
            var JsonString = File.ReadAllText("OurTable.json");
            var DeserialisedResult = JsonConvert.DeserializeObject<InGebruik>(JsonString);
            Tafels newTables = new Tafels
            {

            };
            try
            {

                var newdate = new InGebruik
                {
                    Tafel = new Dictionary<string, Dictionary<string, Tafels>>()
                };
            }
            catch (System.NullReferenceException)
            {

            }
            if (DeserialisedResult.Tafel[Resv.Date] == null)
            {
                var newdate = new InGebruik
                {
                    Tafel=new Dictionary<string, Dictionary<string, Tafels>>
                };
            }
            else if (DeserialisedResult.Tafel[Resv.Date] != null)
            {
                
                
            }
        }
    }
}
