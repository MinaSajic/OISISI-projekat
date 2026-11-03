using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SajamKnjiga.Core.Models
{
    public class Knjiga
    {
        public string ISBN { get; set; }

        public string Naziv { get; set; }

        public string Zanr { get; set; }
        
        public int GodinaIzdanja { get; set; }

        public int Cena {  get; set; }

        public int BrojStrana { get; set; }

        public List<Autor> Autori { get; set; }

        //izdavac

        public List<Posetilac> PosetiociKojiSuKupili {  get; set; }

        public List<Posetilac> PosetiociListaZelja {  get; set; }

    }
}
