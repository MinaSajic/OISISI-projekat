using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SajamKnjiga.Core.Models
{
    public class Kupovina
    {
        public Posetilac Posetilac { get; set; }
        public Knjiga Knjiga { get; set; } 

        public DateOnly DatumKupovine { get; set; }

        private int ocena;
        public int Ocena
        {
            get { return ocena; }
            set
            {
                if (value >= 1 && value <= 5)
                {
                    ocena = value;
                }
                // ostavljam ovde bez elsa, dopunicu u drugoj klasi
                // ako korisnik ne unese broj izmedju 1 i 5
                // TODO
            }
        }

        public string Komentar {  get; set; }
    }
}
