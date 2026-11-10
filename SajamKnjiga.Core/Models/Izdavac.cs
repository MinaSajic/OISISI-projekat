using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SajamKnjiga.Core.Serialization;

namespace SajamKnjiga.Core.Models
{
    public class Izdavac : ISerializable
    {
        public int Sifra {  get; set; }

        public string Naziv {  get; set; }
        
        public Autor SefIzdavaca { get; set; }

        public List<Autor> SpisakAutora = new List<Autor>();

        public List<Knjiga> SpisakKnjiga = new List<Knjiga>();

        public string[] ToCSV()
        {

            string lkSefa = "";

            if( SefIzdavaca != null)
            {
                lkSefa = SefIzdavaca.LK;
            }

            string[] csvValues =
            {
                Sifra.ToString(),
                Naziv,
                lkSefa,

            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Sifra = int.Parse(values[0]);
            Naziv = values[1];
            //string lkSefa = values[2];
        }

    }
}
