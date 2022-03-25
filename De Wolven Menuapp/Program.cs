using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace De_Wolven_Menuapp
{
    public class Menukaart
    {
        public string ItemType { get; set; } = "";
        public string Naam { get; set; } = "";
        public string Prijs { get; set; } = "";
        public string Allergenen { get; set; } = "";

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Beginscherm.Begin();
            
        }

    }

}
