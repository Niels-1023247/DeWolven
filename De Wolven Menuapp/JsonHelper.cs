﻿using System;
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
        public MenuDranken[] Dranken { get; set; }
        public MenuDesserts[] Desserts { get; set; }

    }

    public class Gerechten
    {
        public string Gerechtnaam { get; set; }
        public string Prijs { get; set; }
        public string Allergenen { get; set; }
    }

    public class MenuDranken
    {
        public string Dranknaam { get; set; }
        public string Prijs { get; set; }
        public string Allergenen { get; set; }
    }
    public class MenuDesserts
    {
        public string Dessertnaam { get; set; }
        public string Prijs { get; set; }
        public string Allergenen { get; set; }
    }


}