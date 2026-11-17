using SajamKnjiga.Core.Models;
using SajamKnjiga.Core.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SajamKnjiga.Core.DAO
{
    internal class KnjigaDAO
    {
        private List<Knjiga> knjige;

        private Storage<Knjiga> storage;

        private int NadjiPozicijuKnjige(string isbn)
        {
            for (int i = 0; i < knjige.Count; i++)
            {
                if (knjige[i].ISBN == isbn)
                {
                    return i;
                }
            }
            return -1;

        }
        public KnjigaDAO()
        {
            storage = new Storage<Knjiga>("Knjige.txt");

            knjige = storage.Load();

          
        }

        public Knjiga DobaviKnjigu(string isbn)
        {
            int pozicija = NadjiPozicijuKnjige(isbn);

            if(pozicija == -1)
            {
                return null;
            }
            return knjige[pozicija];
        }



        public List<Knjiga> DobaviSveKnjige()
        {
            return knjige;
        }

        public bool PostojiLiKnjiga(string isbn)
        {
            foreach (Knjiga knjiga in knjige)
            {
                if(knjiga.ISBN == isbn)
                {
                    return true;
                }
            }

            return false;
        }


        internal void DodavanjeKnjige(Knjiga knjiga)
        {
           
            knjige.Add(knjiga);  // ubacujemo novu knjigu u listu
            storage.Save(knjige); // upisuje odmah celu izmenjenu listu u Knjige.txt

        }

        internal void ObrisiKnjigu(string isbn)
        {

            int pozicija = NadjiPozicijuKnjige(isbn);

            if(pozicija == -1)
            {
                return;
            }
            knjige.RemoveAt(pozicija);
            storage.Save(knjige);
        }

        internal void IzmeniKnjigu(Knjiga knjiga)
        {
            Knjiga original = DobaviKnjigu(knjiga.ISBN);

            if(original == null)
            {
                return;
            }

            original.Naziv = knjiga.Naziv; // ovde menjamo ovo original sa podacima iz kopije
            original.Zanr = knjiga.Zanr;
            original.GodinaIzdanja = knjiga.GodinaIzdanja;
            original.Cena = knjiga.Cena;
            original.BrojStrana = knjiga.BrojStrana;

            storage.Save(knjige);

        }

        internal void SacuvajKnjige()
        {
            storage.Save(knjige);
        }
    }
}

