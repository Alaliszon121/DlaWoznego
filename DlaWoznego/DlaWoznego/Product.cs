using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DlaWoznego
{
    internal class Produkt
    {
        public object Pokoj { get; set; }
        public string Nazwa { get; set; }
        public Produkt(object pok, string naz)
        {
            Pokoj = pok;
            Nazwa = naz;
        }
        public override string ToString()
        {
            return String.Format("{0} {1}", Pokoj, Nazwa);
        }
    }
}
