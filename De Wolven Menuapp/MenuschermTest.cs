﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace De_Wolven_Menuapp
{
    internal class MenuschermTest
    {
        public static void ChangeConInfoTest()
        {
            var Backup = File.ReadAllText("ContactInfo.json");
            var testing = true;
            while (testing)
            {
                
                Contact.ChangeInfoMenu();
                Contact.Contactgegevens();
                File.WriteAllText("ContactInfo.json", Backup);
                Console.WriteLine(" again?");
                var input=Console.ReadKey().Key;
                testing= input!=ConsoleKey.N;
            }
        }
        public static void BadInputTest()
        {

        }

        /*test het laden van de JSONS
         * test veranderen van data in continfo
         * test aanpassen JSON, json aanpassen en kijken of het werkt
         * test invoeren onjuiste input (keyboard rammen, kijken of er iets gebeurt)*/
    }
}
