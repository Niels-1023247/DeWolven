using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace De_Wolven_Menuapp
{
    public class JsonHelper
    {
        public class Menukaart
        {
            public string ItemType { get; set; } = "";
            public string Naam { get; set; } = "";
            public string Prijs { get; set; } = "";
            public string Allergenen { get; set; } = "";
            public static string JsonFileName()
            {
                return Path.Combine("Assets", "Menukaart.JSON");
            }
        }
        public static List<Menukaart> ReadMenu()
        {
            string json = File.ReadAllText(Menukaart.JsonFileName());
            return JsonSerializer.Deserialize<List<Menukaart>>(json)
                ?? new List<Menukaart>();
        }
        public static void WriteMenu(List<Menukaart> menus)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string json = JsonSerializer.Serialize(menus, options);
            File.WriteAllText(Menukaart.JsonFileName(), json);
        }

    }
}

