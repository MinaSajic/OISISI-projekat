using SajamKnjiga.Core.DAO;
using SajamKnjiga.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SajamKnjiga.Core.Services
{
    public class PosetilacService
    {
        private PosetilacDAO posetilacDAO;

        public PosetilacService()
        {
            this.posetilacDAO = new PosetilacDAO();
        }

        public bool PostojiLiPosetilac(string brojClanskeKarte)
        {
            return posetilacDAO.PostojiLiPosetilac(brojClanskeKarte);
        }

        public Posetilac DobaviPosetioca(string brk)
        {
            return posetilacDAO.DobaviPosetioca(brk);
        }

        public List<Posetilac> DobaviSvePosetioce()
        {
            return posetilacDAO.DobaviSvePosetioce();
        }

        public void DodajPosetioca(Posetilac posetilac)
        {
            if (posetilacDAO.PostojiLiPosetilac(posetilac.BrojClanskeKarte))
            {
                return;
                // TODO
                // da neku gresku izbaci
            }

            posetilacDAO.DodajPosetioca(posetilac);

           
        }

        public void IzbrisiPosetioca(string brojClanskeKarte)
        {
            if (!posetilacDAO.PostojiLiPosetilac(brojClanskeKarte))
            {
                return;
                // TODO
                // da neku gresku izbaci
            }

            posetilacDAO.IzbrisiPosetioca(brojClanskeKarte);
        }

        public void IzmeniPosetioca(Posetilac posetilac)
        {
            posetilacDAO.IzmeniPosetioca(posetilac);
        }
    }
}
