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
            if (DeserialisedResult.Tafels[Resv.Date] == null)
            {
                var newdate = new Tafels
                {

                };
            }
        }
    }
}
