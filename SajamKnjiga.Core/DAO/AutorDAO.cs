using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SajamKnjiga.Core.Models;
using SajamKnjiga.Core.Storage;

namespace SajamKnjiga.Core.DAO
{
    internal class AutorDAO
    {
        private List<Autor> autori;

        private Storage<Autor> storage;

        public AutorDAO()
        {
            storage = new Storage<Autor>("Autori.txt");
            autori = storage.Load();
        }

        public List<Autor> DobaviSveAutore()
        {
            return autori;
        }

        public Autor DobaviAutora(string lk)
        {
            foreach (Autor autor in autori)
            {
                if(autor.LK == lk)
                {
                    return autor;
                }
            }

            return null;
        }

        internal void DodajAutora(Autor autor)
        {
            autori.Add(autor);
            storage.Save(autori);
        }

        internal void ObrisiAutora(string lk)
        {
            Autor autor = DobaviAutora(lk);

            if(autor == null)
            {
                return;
            }

            autori.Remove(autor);
            storage.Save(autori);
        }

        internal void IzmeniAutora(Autor autor)
        {
            Autor postojeciAutor = DobaviAutora(autor.LK);

            if(postojeciAutor == null)
            {
                return;
            }

            int pozicija = autori.IndexOf(postojeciAutor);
            autori[pozicija] = autor;

            storage.Save(autori);
        }

    }
}
