using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SajamKnjiga.Core.Models
{
    public class Autor
    {
        public string Ime { get; set; }

        public string Prezime { get; set; }
        
        public DateOnly DatumRodjenja { get; set; }

        public Adresa Adresa { get; set; }

        public string Telefon { get; set; }

        public string E_mail { get; set; }

        public string LK { get; set; }

        public int GodineIskustva {  get; set; }

        public List<Knjiga> Autori { get; set; }

    }
}
