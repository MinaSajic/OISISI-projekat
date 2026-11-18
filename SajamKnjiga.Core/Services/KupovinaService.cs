using SajamKnjiga.Core.DAO;
using SajamKnjiga.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SajamKnjiga.Core.Services
{
    public class KupovinaService
    {
        private KupovinaDAO kupovinaDAO;

        public KupovinaService()
        {
            this.kupovinaDAO = new KupovinaDAO();
        }

        internal KupovinaService(KupovinaDAO kupovine)
        {
            this.kupovinaDAO = kupovine;
        }

        public bool PostojiLiKupovina(string brojClanskeKarte, string isbn)
        {
            return kupovinaDAO.PostojiLiKupovina(brojClanskeKarte, isbn);
        }

        public Kupovina DobaviKupovinu(string brojClanskeKarte, string isbn)
        {
            return kupovinaDAO.DobaviKupovinu(brojClanskeKarte, isbn);
        }

        public List<Kupovina> DobaviSveKupovine()
        {
            return kupovinaDAO.DobaviSveKupovine();
        }

        public bool DodavanjeKupovine(Kupovina kupovina)
        {
            if (kupovinaDAO.PostojiLiKupovina(kupovina.Posetilac.BrojClanskeKarte, kupovina.Knjiga.ISBN))
            {
                return false;
            }

            kupovinaDAO.DodavanjeKupovine(kupovina);
            return true;
        }

        public bool IzmenaKupovine(Kupovina kupovina)
        {
            if (!kupovinaDAO.PostojiLiKupovina(kupovina.Posetilac.BrojClanskeKarte, kupovina.Knjiga.ISBN))
            {
                return false;               // ako ne postoji knjiga nemamo sta da menjamo
            }

            kupovinaDAO.IzmenaKupovine(kupovina);
            return true;
        }

        public bool BrisanjeKupovine(string brojClanskeKarte, string isbn)
        {

            if (!kupovinaDAO.PostojiLiKupovina(brojClanskeKarte, isbn))
            {
                return false;
            }

            kupovinaDAO.BrisanjeKupovine(brojClanskeKarte, isbn);
            return true;
        }

       
    }
}
