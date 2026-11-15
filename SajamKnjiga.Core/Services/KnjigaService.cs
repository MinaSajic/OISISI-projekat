using SajamKnjiga.Core.DAO;
using SajamKnjiga.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SajamKnjiga.Core.Services
{
    public class KnjigaService
    {
        private KnjigaDAO knjigaDAO;

        public KnjigaService()
        {
            this.knjigaDAO = new KnjigaDAO();
        }

        public Knjiga DobaviKnjigu(string isbn)
        {
            return knjigaDAO.DobaviKnjigu(isbn);
        }

        public List<Knjiga> DobaviSveKnjige()
        {
            return knjigaDAO.DobaviSveKnjige();
        }

        public void DodavanjeKnjige(Knjiga knjiga)
        {
            // TODO: dodati gresku

            knjigaDAO.DodavanjeKnjige(knjiga);


        }

        public void IzbrisiKnjigu(string isbn)
        {
           /* if (!knjigaDAO.PostojiLiKnjiga(isbn))
            {
                return;
                // TODO: za gresku
                
            }
           */

            knjigaDAO.ObrisiKnjigu(isbn);
        }

        public void IzmeniKnjigu(Knjiga knjiga)
        {
            knjigaDAO.IzmeniKnjigu(knjiga);
        }
    }

}
