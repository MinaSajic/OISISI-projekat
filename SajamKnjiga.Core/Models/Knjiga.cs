using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SajamKnjiga.Core.Serialization;

namespace SajamKnjiga.Core.Models
{
    public class Knjiga : ISerializable
    {
        public string ISBN { get; set; }

        public string Naziv { get; set; }

        public string Zanr { get; set; }
        
        public int GodinaIzdanja { get; set; }

        public int Cena {  get; set; }

        public int BrojStrana { get; set; }

        public List<Autor> Autori { get; set; } = new List<Autor>();

        public List<string> LkAutora { get; set; } = new List<string>();

        public Izdavac Izdavac { get; set; } 

        public int SifraIzdavaca { get; set; }

        public override string ToString()
        {
            return $"{ISBN} | {Naziv} | {Zanr} | {GodinaIzdanja} | {Cena} din | {BrojStrana} str.";
        }
        public List<string> PosetiociKojiSuKupili { get; set; } = new List<string>();

        public List<string> PosetiociListaZelja { get; set; } = new List<string>();

        public string[] ToCSV()
            
        {
            string[] csvValues =
            {
                ISBN,
                Naziv,
                Zanr,
                GodinaIzdanja.ToString(),
                Cena.ToString(),
                BrojStrana.ToString(),
                string.Join("%", LkAutora),
                SifraIzdavaca.ToString()
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            ISBN = values[0];
            Naziv = values[1];
            Zanr = values[2];
            GodinaIzdanja = int.Parse(values[3]);
            Cena = int.Parse(values[4]);
            BrojStrana = int.Parse(values[5]);

            LkAutora = new List<string>();

            if (values.Length > 6 && !string.IsNullOrEmpty(values[6]))
            {
                LkAutora = values[6].Split('%').ToList();
            }

            if (values.Length > 7 && int.TryParse(values[7], out int sifra))
            {
                SifraIzdavaca = sifra;
            }
        }
    }
}
