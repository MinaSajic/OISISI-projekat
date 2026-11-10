using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SajamKnjiga.Core.Serialization;

namespace SajamKnjiga.Core.Models
{
    public class Autor : ISerializable
    {
        public string Ime { get; set; }

        public string Prezime { get; set; }
        
        public DateOnly DatumRodjenja { get; set; }

        public Adresa Adresa { get; set; }

        public string Telefon { get; set; }

        public string E_mail { get; set; }

        public string LK { get; set; }

        public int GodineIskustva {  get; set; }

        public List<Knjiga> Knjige { get; set; } = new List<Knjiga>();

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Ime,
                Prezime,
                DatumRodjenja.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                Adresa.Ulica,
                Adresa.Broj,
                Adresa.Grad,
                Adresa.Drzava,
                Telefon,
                E_mail,
                LK,
                GodineIskustva.ToString()
                //List<Knjiga> Knjige
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Ime = values[0];
            Prezime = values[1];
            DatumRodjenja = DateOnly.ParseExact(values[2], "yyyy-MM-dd", CultureInfo.InvariantCulture);
            Adresa = new Adresa()
            {
                Ulica = values[3],
                Broj = values[4],
                Grad = values[5],
                Drzava = values[6]
            };
            Telefon = values[7];
            E_mail = values[8];
            LK = values[9];
            GodineIskustva = int.Parse(values[10]);
            //List<Knjiga> Knjige
        }
    }
}
