using SajamKnjiga.Core.Models;
using SajamKnjiga.Core.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SajamKnjiga.Core.DAO
{
    internal class PosetilacDAO
    {
        private List<Posetilac> posetioci;
        private Storage<Posetilac> storage;


        public PosetilacDAO()
        {
            storage = new Storage<Posetilac>("Posetioci.txt");

            posetioci = storage.Load();
        }

        public Posetilac DobaviPosetioca(string brojClanskeKarte)
        {
            int position = -1;

            for (int i = 0; i < posetioci.Count; i++)
            {
                if (posetioci[i].BrojClanskeKarte == brojClanskeKarte)
                {
                    position = i;
                    break;
                }
            }

            return posetioci[position];
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

            int position = -1;

            for (int i = 0; i < posetioci.Count; i++)
            {
                if (posetioci[i].BrojClanskeKarte == brojClanskeKarte)
                {
                    position = i;
                    break;
                }
            }

            posetioci.RemoveAt(position);
            storage.Save(posetioci);
        }

        internal void IzmeniPosetioca(Posetilac posetilac)
        {
            int position = -1;

            for (int i = 0; i < posetioci.Count; i++)
            {
                if (posetioci[i].BrojClanskeKarte == posetilac.BrojClanskeKarte)
                {
                    position = i;
                    break;
                }
            }

            posetioci[position] = posetilac;
            storage.Save(posetioci);
        }
    }
}
