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

        //public Izdavac Izdavac { get; set; } 

        public List<Posetilac> PosetiociKojiSuKupili { get; set; } = new List<Posetilac>();

        public List<Posetilac> PosetiociListaZelja { get; set; } = new List<Posetilac>();

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                ISBN,
                Naziv,
                Zanr,
                GodinaIzdanja.ToString(),
                Cena.ToString(),
                BrojStrana.ToString()
                //List<Autor> Autori = new List<Autor>
                 /*dodati nakon kreiranja klase Izdavac*/
                //Izdavac
                //public List<Posetilac> PosetiociKojiSuKupili
                //public List<Posetilac> PosetiociListaZelja
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
            /*dodati ostatak*/
        }

    }
}
