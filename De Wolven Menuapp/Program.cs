using System;

namespace De_Wolven_Menuapp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            beginscherm a = new beginscherm();
            a.Begin();
            string soortgebruiker = System.Console.ReadLine();


            if (soortgebruiker == "1")
            {
                Console.Clear();
                hoofdmenuscherm b = new hoofdmenuscherm();
                b.SchermKlanten();
                string optieklanten = System.Console.ReadLine();
                if (optieklanten == "1")
                {
                    Console.WriteLine("Nog niks");
                }

                if (optieklanten == "2")
                {
                    Console.Clear();
                    Menu.menukaart();
                }
                if (optieklanten == "3")

                {
                    Console.WriteLine("ook nog niks");
                }

                if (optieklanten == "4")
                {
                    Console.Clear();
                    contact c = new contact();
                    c.Contactgegevens();

                }


            }
            if (soortgebruiker == "2")
            {
                hoofdmenuscherm e = new hoofdmenuscherm();
                Console.Clear();
                e.SchermMedewerker();
                string optiemedewerker = Console.ReadLine();

            }
        }

    }

}
