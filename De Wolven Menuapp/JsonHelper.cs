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
        //public string Dummy1 { get; set; }
        public Gerechten[] Dishes;

        //public _hoofdgerechten[] Gerechten { get; set; }
        //public _drankjes[] Dranken { get; set; }
        //public _desserts[] Desserts { get; set; }

    }

    public class Gerechten
    {
        public string GerechtNaam { get; set; }
        public string Prijs { get; set; }
        public string Allergie { get; set; }
    }



    //public class _hoofdgerechten
    //{
    //    public string GerechtNaam { get; set; }
    //    public string Prijs { get; set; }
    //    public string Allergenen { get; set; }
    //}
    //public class _drankjes
    //{
    //    public string DrankNaam { get; set; }
    //    public string Prijs { get; set; }
    //    public string Allergenen { get; set; }
    //}
    //public class _desserts
    //{
    //    public string DessertNaam { get; set; }
    //    public string Prijs { get; set; }
    //    public string Allergenen { get; set; }
    //}


}