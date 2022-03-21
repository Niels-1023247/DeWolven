using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Wolven_Menuapp
{
    internal class beginscherm
    {
        public void Begin()
        {
            Console.WriteLine("Welkom bij de wolven!");
            Console.WriteLine("Bent u gast of medewerker?");
            Console.WriteLine("[1] Gast");
            Console.WriteLine("[2] Medewerker");
            Console.WriteLine("Voer 1 of 2 in en druk op <ENTER>");
        }
    }
}
