using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;
using System.IO;

namespace De_Wolven_Menuapp
{
    internal class ResvUTest
    {
        //JSON laden werkt correct
        public static void TestforJsonLoad()
        {
            string reserveringJson = File.ReadAllText("reserveringenbestand.json");
            var reserveringsData = JsonConvert.DeserializeObject<Information>(reserveringJson);
            Debug.Assert(reserveringsData is Information);
            Console.WriteLine("done");
        }
    }
}
