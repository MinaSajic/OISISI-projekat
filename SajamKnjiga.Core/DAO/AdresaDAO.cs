using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SajamKnjiga.Core.Models;
using SajamKnjiga.Core.Storage;

namespace SajamKnjiga.Core.DAO
{
    internal class AdresaDAO
    {
        private List<Adresa> adrese;
        private Storage<Adresa> storage;

        public AdresaDAO()
        {
            storage = new Storage<Adresa>("Adrese.txt");
            adrese = storage.Load();
        }

        public List<Adresa> DobaviSveAdrese()
        {
            return adrese;
        }

        public Adresa DobaviAdresu(int id)
        {
            foreach(Adresa adresa in adrese)
            {
                if(adresa.Id == id)
                {
                    return adresa;
                }
            }

            return null;
        }

        internal void DodajAdresu(Adresa adresa)
        {
            adrese.Add(adresa);
            storage.Save(adrese);
        }

        internal void ObrisiAdresu(int id)
        {
            Adresa adresa = DobaviAdresu(id);

            if(adresa == null)
            {
                return;
            }

            adrese.Remove(adresa);
            storage.Save(adrese);
        }

        internal void IzmeniAdresu(Adresa adresa)
        {
            Adresa postojecaAdresa = DobaviAdresu(adresa.Id);

            if(postojecaAdresa == null)
            {
                return;
            }

            int pozicija = adrese.IndexOf(postojecaAdresa);
            adrese[pozicija] = adresa;

            storage.Save(adrese);
        }

    }
}
