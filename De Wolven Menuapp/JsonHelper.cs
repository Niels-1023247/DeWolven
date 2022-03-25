/*namespace De_Wolven_Menuapp
{
    public partial class Welcome
    {
        public Menukaart Menukaart { get; set; }
    }

    public class Menukaart //uitzoeken
    {
        public List<Gerechten> Gerechten { get; set; }
        public List<Dranken> Dranken { get; set; }
        public List<Dessert> Desserts { get; set; }
    }

    public class Dessert
    {
        public string DessertNaam { get; set; }
        public string Prijs { get; set; }
        public string Allergenen { get; set; }
    }

    public class Dranken
    {
        public string DrankNaam { get; set; }
        public string Prijs { get; set; }
        public string Allergenen { get; set; }
    }

    public class Gerechten
    {
        public string GerechtNaam { get; set; }
        public string Prijs { get; set; }
        public string Allergenen { get; set; }
    }

    public partial class Welcome
    {
        public static Welcome FromJson(string json)
        {
            return JsonSerializer.Deserialize<Welcome>(json);
        }
    }

    public static class Serialize
    {
        public static string ToJson(this Welcome self)
        {
            return JsonConvert.SerializeObject(self, De_Wolven_Menuapp.Converter.Settings);
        }
    }

*/