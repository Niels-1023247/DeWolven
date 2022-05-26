using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Wolven_Menuapp
{
	internal static class Menu
	{
		public static void Menukaart() // method om het menu tevoorschijn te *. toveren *'.
		 // methode voor overschakelen naar de verschillende menu soorten
		{
			while (true)
			{
				Console.Clear();
				Console.WriteLine("Welkom bij het menu-scherm van De Wolven!");
				Console.WriteLine("[1] Bekijk onze Gerechten");
				Console.WriteLine("[2] Bekijk onze Desserts");
				Console.WriteLine("[3] Bekijk onze Dranken");
				Console.WriteLine("Voer 1, 2 of 3 in");
				ConsoleKey optieklanten = Console.ReadKey().Key;
				if (optieklanten == ConsoleKey.D1)
				{
					Console.Clear();
					Gerechtenkaart.Gerechtenmenukaart();
				}

				else if (optieklanten == ConsoleKey.D2)
				{
					Console.Clear();
					Desserts.Dessertsmenukaart();
				}
				else if (optieklanten == ConsoleKey.D3)

				{
					Console.Clear();
					Dranken.Drankenmenukaart();
				}
				else if (optieklanten == ConsoleKey.Escape) // terug naar hoofdmenu
				{
					Console.Clear();
					break;
				}
			}
		}
	}
}
