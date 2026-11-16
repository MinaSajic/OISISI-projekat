using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SajamKnjiga.Core.DAO;
using SajamKnjiga.Core.Models;

namespace SajamKnjiga.Core.Services
{
    public class AdresaService
    {
        private AdresaDAO adresaDAO;

        public AdresaService()
        {
            this.adresaDAO = new AdresaDAO();
        }

        public List<Adresa> DobaviSveAdrese()
        {
            return adresaDAO.DobaviSveAdrese();
        }

        public Adresa DobaviAdresu(int id)
        {
            return adresaDAO.DobaviAdresu(id);
        }

        public bool PostojiLiAdresa(int id)
        {
            return adresaDAO.DobaviAdresu(id) != null;
        }

        public void DodajAdresu(Adresa adresa)
        {
            if(PostojiLiAdresa(adresa.Id))
            {
                return;
            }

            adresaDAO.DodajAdresu(adresa);
        }

        public void ObrisiAdresu(int id)
        {
            if(!PostojiLiAdresa(id))
            {
                return;
            }

            adresaDAO.ObrisiAdresu(id);
        }

        public void IzmeniAdresu(Adresa adresa)
        {
            if(!PostojiLiAdresa(adresa.Id))
            {
                return;
            }

            adresaDAO.IzmeniAdresu(adresa);
        }
    }
}
