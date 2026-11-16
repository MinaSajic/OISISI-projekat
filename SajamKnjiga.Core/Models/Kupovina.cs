using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using SajamKnjiga.Core.Serialization;
using SajamKnjiga.Core.Utils;

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

        public override string ToString()
        {
            return $"Broj članske karte: {Posetilac?.BrojClanskeKarte}, " +
                   $"ISBN knjige: {Knjiga?.ISBN}, " +
                   $"Datum kupovine: {DatumKupovine:dd.MM.yyyy.}, " +
                   $"Ocena: {Ocena}, Komentar: {Komentar}";
        }

        public Kupovina Kopija()
        {
            return new Kupovina
            {
                Posetilac = Posetilac,
                Knjiga = Knjiga,
                DatumKupovine = DatumKupovine,
                Ocena = Ocena,
                Komentar = Komentar
            };
        }

        public void FromCSV(string[] values)
        {
            Posetilac = new Posetilac { BrojClanskeKarte = values[0] };
            Knjiga = new Knjiga { ISBN = values[1] };
            DatumKupovine = DatumFormat.TekstUDatum(values[2]);

           
            Ocena = int.Parse(values[3]);
            Komentar = values[4];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
               Posetilac.BrojClanskeKarte,
               Knjiga.ISBN,
               DatumFormat.DatumUTekst(DatumKupovine),
               Ocena.ToString(),
               Komentar
            };
            return csvValues;
        }
    }
}
    

