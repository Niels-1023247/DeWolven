namespace De_Wolven_Menuapp
{
    public class Menukaart
    {
        public _hoofdgerechten[] Gerechten { get; set; }
        public _drankjes[] Dranken { get; set; }
        public _desserts[] Desserts { get; set; }
    }
    public class _hoofdgerechten
    {   
        public string GerechtNaam { get; set; }
        public string Prijs { get; set; }
        public string Allergenen { get; set; }
    }    
    public class _drankjes
    {   
        public string DrankNaam { get; set; }
        public string Prijs { get; set; }
        public string Allergenen { get; set; }
    }    
    public class _desserts
    {   
        public string DessertNaam { get; set; }
        public string Prijs { get; set; }
        public string Allergenen { get; set; }
    }    

}