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
        private static List<Knjiga> knjige;

        private Storage<Knjiga> storage;


        public KnjigaDAO()
        {
            storage = new Storage<Knjiga>("Knjige.txt");

            knjige = storage.Load();
        }

        public Knjiga DobaviKnjigu(string ISBN)
        {
            int position = -1;

            for (int i = 0; i < knjige.Count; i++)
            {
                if (knjige[i].ISBN == ISBN)
                {
                    position = i;
                    break;
                }
            }
            return knjige[position];
        }



        public List<Knjiga> DobaviSveKnjige()
        {
            return knjige;
        }


        internal void DodavanjeKnjige(Knjiga knjiga)
        {
            knjige.Add(knjiga);  // ubacujemo novu knjigu u listu
            storage.Save(knjige); // upisuje odmah celu izmenjenu listu u Knjige.txt

        }

        internal void ObrisiKnjigu(string isbn)
        {

            int position = -1;

            for (int i = 0; i < knjige.Count; i++)
            {
                if (knjige[i].ISBN == isbn)
                {
                    position = i;
                    break;
                }
            }

            knjige.RemoveAt(position);
            storage.Save(knjige);
        }

        internal void IzmeniKnjigu(Knjiga knjiga)
        {
            int position = -1;

            for (int i = 0; i < knjige.Count; i++)
            {
                if (knjige[i].ISBN == knjiga.ISBN)
                {
                    position = i;
                    break;
                }
            }

            knjige[position] = knjiga;
            storage.Save(knjige);
        }
    }


}

