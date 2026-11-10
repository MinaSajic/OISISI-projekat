using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using SajamKnjiga.Core.Serialization;

namespace SajamKnjiga.Core.Models
{
    public class Kupovina : ISerializable
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

        public void FromCSV(string[] values)
        {
            Posetilac.BrojClanskeKarte = values[0];
            Knjiga.ISBN = values[1];
            DatumKupovine = DateOnly.ParseExact(values[2], new[] { "d.M.yyyy.", "d.M.yyyy", "M'/'d'/'yyyy" });

           
            Ocena = int.Parse(values[3]);
            Komentar = values[4];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
               Posetilac.BrojClanskeKarte,
               Knjiga.ISBN,
               DatumKupovine.ToString("d.M.yyyy."),
               Ocena.ToString(),
               Komentar
            };
            return csvValues;
        }
    }
}
    

