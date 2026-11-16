using SajamKnjiga.Core.Models;
using SajamKnjiga.Core.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SajamKnjiga.Core.DAO
{
    internal class KupovinaDAO
    {
        private List<Kupovina> kupovine;

        private Storage<Kupovina> storage;

        private int NadjiPoziciju(string brojClanskeKarte, string isbn)
        {
            for (int i = 0; i < kupovine.Count(); i++)
            {
                if (kupovine[i].Posetilac.BrojClanskeKarte == brojClanskeKarte &&
                    kupovine[i].Knjiga.ISBN == isbn)
                {
                    return i;
                }
            }
            return -1;

        }
        public KupovinaDAO()
        {
            storage = new Storage<Kupovina>("Kupovine.txt");

            kupovine = storage.Load();
        }


        public Kupovina DobaviKupovinu(string brojClanskeKarte, string isbn)
        {
            int pozicija = NadjiPoziciju(brojClanskeKarte, isbn);

            if(pozicija == -1)
            {
                return null;
            }

            return kupovine[pozicija];
        }

        public List<Kupovina> DobaviSveKupovine()
        {
            return kupovine;
        }

        public bool PostojiLiKupovina(string brojClanskeKarte,string isbn)
        {
            foreach (Kupovina kupovina in kupovine)
            {
                if(kupovina.Posetilac.BrojClanskeKarte == brojClanskeKarte &&
                   kupovina.Knjiga.ISBN == isbn)
                {
                    return true;
                }
            }
            return false;
        }

        internal void DodavanjeKupovine(Kupovina kupovina)
        {
            kupovine.Add(kupovina);
            storage.Save(kupovine);
        }

        internal void IzmenaKupovine(Kupovina kupovina)
        {
            Kupovina original = DobaviKupovinu(kupovina.Posetilac.BrojClanskeKarte, kupovina.Knjiga.ISBN);

            if (original == null)
            {
                return;
            }

            original.DatumKupovine = kupovina.DatumKupovine;
            original.Ocena = kupovina.Ocena;
            original.Komentar = kupovina.Komentar;

            storage.Save(kupovine);
        }

        internal void BrisanjeKupovine(string brojClanskeKarte,string isbn)
        {
            int pozicija = NadjiPoziciju(brojClanskeKarte,isbn);

            if(pozicija == -1)
            {
                return;
            }

            kupovine.RemoveAt(pozicija);
            storage.Save(kupovine);
        }
    }
}
