using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Wolven_Menuapp
{
    internal class hoofdmenuscherm
    {
        public void SchermMedewerker()
        {
            Console.WriteLine("Welkom alpha");
            Console.WriteLine("[1] Bekijk de reserveringen");
            Console.WriteLine("[2] Neem een bestelling op");
            Console.WriteLine("[3] Maak een nieuwe reservering");
            Console.WriteLine("[4] Bekijk besteltotaal per tafel");
            Console.WriteLine("[5] Verander menukaart");
            Console.WriteLine("Voer 1, 2, 3, 4 of 5 in en druk op <ENTER>");
        }

        public void SchermKlanten()
        {
            Console.WriteLine("Welkom bij de wolven gast!");
            Console.WriteLine("[1] Reserveer een tafel");
            Console.WriteLine("[2] Bekijk ons menukaart");
            Console.WriteLine("[3] Login met uw speciale code");
            Console.WriteLine("[4] Over ons");
            Console.WriteLine("Voer 1, 2, 3 of 4 in en druk op <ENTER>");
        }
    }
}
