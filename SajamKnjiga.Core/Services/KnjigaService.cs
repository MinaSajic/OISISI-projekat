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
            knjigaDAO = new KnjigaDAO();
        }

        internal KnjigaService(KnjigaDAO knjigaDAO)
        {
            this.knjigaDAO = knjigaDAO;
        }

        public Knjiga DobaviKnjigu(string isbn)
        {
            return knjigaDAO.DobaviKnjigu(isbn);
        }

        public List<Knjiga> DobaviSveKnjige()
        {
            return knjigaDAO.DobaviSveKnjige();
        }

        public bool PostojiLiKnjiga(string isbn)
        {
            return knjigaDAO.PostojiLiKnjiga(isbn);
        }

        public bool DodavanjeKnjige(Knjiga knjiga)
        {

            if (knjigaDAO.PostojiLiKnjiga(knjiga.ISBN))
            {
                return false;
            }

            knjigaDAO.DodavanjeKnjige(knjiga);

            return true;
        }

        public bool IzbrisiKnjigu(string isbn)
        {
            if (!knjigaDAO.PostojiLiKnjiga(isbn))
            {
                return false;
            }

            knjigaDAO.ObrisiKnjigu(isbn);
            return true;
        }

        public bool IzmeniKnjigu(Knjiga knjiga)
        {

            if (!knjigaDAO.PostojiLiKnjiga(knjiga.ISBN))
            {
                return false;
            }
            knjigaDAO.IzmeniKnjigu(knjiga);
            return true;
        }
    }

}
