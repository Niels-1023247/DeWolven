using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace De_Wolven_Menuapp
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            //string jsonString = JsonSerializer.Serialize(weatherForecast);
            //Beginscherm.Begin();
            //var welcome = Welcome.FromJson("Assets/Menukaart.JSON");// wat betekent dit???
            var startbeginscherm = new Beginscherm();
            startbeginscherm.Begin();
        }

    }

}
