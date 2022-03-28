using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace De_Wolven_Menuapp
{
    public class Menukaart
    {
        public Gerechten[] gerechten;
        public MenuDranken[] dranken { get; set; }
        public MenuDesserts[] desserts { get; set; }

    }

    public class Gerechten
    {
        public string gerechtnaam { get; set; }
        public string prijs { get; set; }
        public string allergenen { get; set; }
    }

    public class MenuDranken
    {
        public string dranknaam { get; set; }
        public string prijs { get; set; }
        public string allergenen { get; set; }
    }
    public class MenuDesserts
    {
        public string dessertnaam { get; set; }
        public string prijs { get; set; }
        public string allergenen { get; set; }
    }


}