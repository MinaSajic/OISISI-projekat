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

        public PosetilacService(PosetilacDAO posetioci)
        {
            this.posetilacDAO = posetioci;
        }

        public bool PostojiLiPosetilac(string brojClanskeKarte)
        {
            return posetilacDAO.PostojiLiPosetilac(brojClanskeKarte);
        }

        public Posetilac DobaviPosetioca(string brojClanskeKarte)
        {
            return posetilacDAO.DobaviPosetioca(brojClanskeKarte);
        }

        public List<Posetilac> DobaviSvePosetioce()
        {
            return posetilacDAO.DobaviSvePosetioce();
        }

        public bool DodajPosetioca(Posetilac posetilac)
        {
            if (posetilacDAO.PostojiLiPosetilac(posetilac.BrojClanskeKarte))
            {
                return false;  // taj posetilac vec postoji, ne treba ga dodati, return false
               
            }

            posetilacDAO.DodajPosetioca(posetilac); // posetilac ne postoji, dodajemo ga
            return true;

           
        }

        public bool IzbrisiPosetioca(string brojClanskeKarte)
        {
            if (!posetilacDAO.PostojiLiPosetilac(brojClanskeKarte))
            {
                return false;                    // ako posetilac ne postoji nemamo koga da izbrisemo, return false
                // TODO
                // da neku gresku izbaci
            }

            posetilacDAO.IzbrisiPosetioca(brojClanskeKarte);
            return true;
        }

        public bool IzmeniPosetioca(Posetilac posetilac)
        {
            if (!posetilacDAO.PostojiLiPosetilac(posetilac.BrojClanskeKarte))
            {
                return false;    // ako posetilac ne postoji, nemamo koga da menjamo
            }
            posetilacDAO.IzmeniPosetioca(posetilac);
            return true;
        }
    }
}
