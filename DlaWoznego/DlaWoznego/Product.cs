using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DlaWoznego
{
    internal class Produkt
    {
        public int NrPokoju { get; set; }
        public object Pokoj { get; set; }
        public string Nazwa { get; set; }
        public Produkt(int nrpok, string pok, string naz)
        {
            NrPokoju = nrpok;
            Pokoj = pok;
            Nazwa = naz;
        }
        public override string ToString()
        {
            Get_room_name(NrPokoju);
            Console.WriteLine(Pokoj);
            return String.Format("{0} {1}", Pokoj, Nazwa);
        }

        public void Get_room_name(int roomNumber)
        {
            Console.WriteLine("Ustawiam nazwę pokoju");
            switch (roomNumber) {
                case 1:
                    Pokoj = "Lobby";
                    break;
                case 2:
                    Pokoj = "Biuro 1";
                    break;
                case 3:
                    Pokoj = "Biuro 2";
                    break;
                case 4:
                    Pokoj = "Serowerownia 1";
                    break;
                case 5:
                    Pokoj = "Serwerownia 2";
                    break;
                case 6:
                    Pokoj = "Kuchnia";
                    break;
                case 7:
                    Pokoj = "Łazienka";
                    break;
                case 8:
                    Pokoj = "Sala Konferencyjna";
                    break;
                case 9:
                    Pokoj = "Magazyn";
                    break;
                case 10:
                    Pokoj = "Moja Kanciapa";
                    break;
                default:
                    Pokoj = "Nieznany";
                    break;
            }
        }
    }
}
