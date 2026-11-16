using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SajamKnjiga.Core.Models;
using SajamKnjiga.Core.Storage;

namespace SajamKnjiga.Core.DAO
{
    internal class IzdavacDAO
    {
        private List<Izdavac> izdavaci;
        private Storage<Izdavac> storage;

        public IzdavacDAO()
        {
            storage = new Storage<Izdavac>("Izdavaci.txt");
            izdavaci = storage.Load();
        }

        public List<Izdavac> DobaviSveIzdavace()
        {
            return izdavaci;
        }

        public Izdavac DobaviIzdavaca(int sifra)
        {
            foreach(Izdavac izdavac in izdavaci)
            {
                if(izdavac.Sifra == sifra)
                {
                    return izdavac;
                }
            }

            return null;
        }

        internal void DodajIzdavaca(Izdavac izdavac)
        {
            izdavaci.Add(izdavac);
            storage.Save(izdavaci);
        }

        internal void ObrisiIzdavaca(int sifra)
        {
            Izdavac izdavac = DobaviIzdavaca(sifra);

            if(izdavac == null)
            {
                return;
            }

            izdavaci.Remove(izdavac);
            storage.Save(izdavaci);
        }

        internal void IzmeniIzdavaca(Izdavac izdavac)
        {
            Izdavac postojeciIzdavac = DobaviIzdavaca(izdavac.Sifra);

            if(postojeciIzdavac == null)
            {
                return;
            }

            int pozicija = izdavaci.IndexOf(postojeciIzdavac);
            izdavaci[pozicija] = izdavac;

            storage.Save(izdavaci);
        }
    }
}
