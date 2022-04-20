using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De_Wolven_Menuapp
{
    public class Information
    {
        public EnkeleReservering[] Reserveringen { get; set; }
    }
    public class EnkeleReservering
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string CountofPeople { get; set; }
    }
}
