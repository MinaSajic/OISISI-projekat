using SajamKnjiga.Core.Models;
using SajamKnjiga.Core.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SajamKnjiga.Core.DAO
{
    public class PosetilacDAO
    {
        private List<Posetilac> posetioci;
        private Storage<Posetilac> storage;

        private int NadjiPozicijuPosetioca(string brojClanskeKarte)
        {

            for (int i = 0; i < posetioci.Count; i++)
            {
                if (posetioci[i].BrojClanskeKarte == brojClanskeKarte)
                {
                    return i;
                }
            }
            return -1;
        }


        public PosetilacDAO()
        {
            storage = new Storage<Posetilac>("Posetioci.txt");

            posetioci = storage.Load();
        }

        public void SacuvajPosetioce()
        {
            storage.Save(posetioci);
        }

        public Posetilac DobaviPosetioca(string brojClanskeKarte)
        {
            int pozicija = NadjiPozicijuPosetioca(brojClanskeKarte);

            if(pozicija == -1)
            {
                return null;
            }

            return posetioci[pozicija];
        }

        public List<Posetilac> DobaviSvePosetioce()
        {
            return posetioci;
        }

        public bool PostojiLiPosetilac(string brojClanskeKarte)
        {
            foreach (Posetilac posetilac in posetioci)
            {
                if (posetilac.BrojClanskeKarte == brojClanskeKarte)
                {
                    return true;
                }
            }

            return false;
        }

        internal void DodajPosetioca(Posetilac posetilac)
        {
            
            posetioci.Add(posetilac);
            storage.Save(posetioci);
        }

        internal void IzbrisiPosetioca(string brojClanskeKarte)
        {

            int pozicija = NadjiPozicijuPosetioca(brojClanskeKarte);

            if (pozicija == -1)
            {
                return;
            }
            posetioci.RemoveAt(pozicija);
            storage.Save(posetioci);
        }

        internal void IzmeniPosetioca(Posetilac posetilac)
        {

            Posetilac original = DobaviPosetioca(posetilac.BrojClanskeKarte);

            if(original == null)
            {
                return;
            }

            original.Ime = posetilac.Ime;   // original se menja ovde sa novim podacima iz kopije
            original.Prezime = posetilac.Prezime;
            original.DatumRodjenja = posetilac.DatumRodjenja;
            original.Adresa = posetilac.Adresa;
            original.KontaktTelefon = posetilac.KontaktTelefon;
            original.Email = posetilac.Email;
            original.TrenutnaGodinaClanstva = posetilac.TrenutnaGodinaClanstva;
            original.Status = posetilac.Status;
            original.ProsecnaOcenaRecenzija = posetilac.ProsecnaOcenaRecenzija;

            storage.Save(posetioci);
        }
    }
}
