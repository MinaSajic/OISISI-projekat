using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SajamKnjiga.Core.DAO;
using SajamKnjiga.Core.Models;

namespace SajamKnjiga.Core.Services
{
    public class IzdavacService
    {
        private IzdavacDAO izdavacDAO;

        public IzdavacService()
        {
            izdavacDAO = new IzdavacDAO();
        }

        public List<Izdavac> DobaviSveIzdavace()
        {
            return izdavacDAO.DobaviSveIzdavace();
        }

        public Izdavac DobaviIzdavaca(int sifra)
        {
            return izdavacDAO.DobaviIzdavaca(sifra);
        }

        public bool PostojiLiIzdavac(int sifra)
        {
            return izdavacDAO.DobaviIzdavaca(sifra) != null;
        }

        public void DodajIzdavaca(Izdavac izdavac)
        {
            if(PostojiLiIzdavac(izdavac.Sifra))
            {
                return;
            }

            izdavacDAO.DodajIzdavaca(izdavac);
        }

        public void ObrisiIzdavaca(int sifra)
        {
            if(!PostojiLiIzdavac(sifra))
            {
                return;
            }

            izdavacDAO.ObrisiIzdavaca(sifra);
        }

        public void IzmeniIzdavaca(Izdavac izdavac)
        {
            if(!PostojiLiIzdavac(izdavac.Sifra))
            {
                return;
            }

            izdavacDAO.IzmeniIzdavaca(izdavac);
        }
    }
}
