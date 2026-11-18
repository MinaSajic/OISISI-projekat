using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SajamKnjiga.Core.Services;
using SajamKnjiga.Core.Models;

namespace SajamKnjiga.Core.DAO
{
    public class PovezivanjeEntiteta
    {

        private AutorDAO autori;
        private KnjigaDAO knjige;

        private PosetilacDAO posetioci;

        private KupovinaDAO kupovine;

        public AutorService AutoriServis { get; }
        public KnjigaService KnjigeServis { get; }

        public PosetilacService PosetiociServis { get; }

        public PovezivanjeEntiteta()
        {
            autori = new AutorDAO();
            knjige = new KnjigaDAO();

            AutoriServis = new AutorService(autori);
            KnjigeServis = new KnjigaService(knjige);

            poveziAutoreIKnjige();

            posetioci = new PosetilacDAO();
            kupovine = new KupovinaDAO(); 
            PosetiociServis = new PosetilacService(posetioci);

            poveziPosetioceIKnjige();

            PoveziListuZeljaSaKnjigama();
        }

        private void poveziAutoreIKnjige()
        {
            foreach (Autor autor in autori.DobaviSveAutore())
            {
                autor.Knjige.Clear();
            }
            foreach (Knjiga knjiga in knjige.DobaviSveKnjige())
            {
                knjiga.Autori.Clear();

                foreach (string lk in knjiga.LkAutora)
                {
                    Autor autor = autori.DobaviAutora(lk);

                    if (autor != null)
                    {
                        knjiga.Autori.Add(autor);
                        autor.Knjige.Add(knjiga);
                    }
                }
            }
        }

        public bool DodajAutoraNaKnjigu(string isbn, string lk)
        {
            Knjiga knjiga = knjige.DobaviKnjigu(isbn);
            Autor autor = autori.DobaviAutora(lk);

            if (knjiga == null || autor == null)
            {
                return false;
            }

            if (knjiga.LkAutora.Contains(lk))
            {
                return false;
            }

            knjiga.LkAutora.Add(lk);
            knjiga.Autori.Add(autor);
            autor.Knjige.Add(knjiga);

            knjige.SacuvajKnjige();

            return true;
        }

        public bool UkloniAutoraSaKnjige(string isbn, string lk)
        {
            Knjiga knjiga = knjige.DobaviKnjigu(isbn);
            Autor autor = autori.DobaviAutora(lk);

            if (knjiga == null || autor == null)
            {
                return false;
            }

            if (!knjiga.LkAutora.Contains(lk))
            {
                return false;
            }

            knjiga.LkAutora.Remove(lk);
            knjiga.Autori.Remove(autor);
            autor.Knjige.Remove(knjiga);

            knjige.SacuvajKnjige();

            return true;
        }

        public bool DodajKnjiguAutoru(string isbn, string lk)
        {
            return DodajAutoraNaKnjigu(isbn, lk);
        }

        public bool UkloniKnjiguSaAutora(string isbn, string lk)
        {
            return UkloniAutoraSaKnjige(isbn, lk);
        }


        private void poveziPosetioceIKnjige()
        {
            foreach (Posetilac posetilac in posetioci.DobaviSvePosetioce())
            {
                posetilac.Kupovine.Clear();
            }

            foreach (Kupovina kupovina in kupovine.DobaviSveKupovine())
            {
                Posetilac posetilac = posetioci.DobaviPosetioca(kupovina.Posetilac.BrojClanskeKarte);
                Knjiga knjiga = knjige.DobaviKnjigu(kupovina.Knjiga.ISBN);


                if (posetilac != null && knjiga != null)
                {
                    kupovina.Posetilac = posetilac;
                    kupovina.Knjiga = knjiga;
                    posetilac.Kupovine.Add(kupovina);
                }
            }
        }

        private void PoveziListuZeljaSaKnjigama()
        {
            foreach (Posetilac posetilac in posetioci.DobaviSvePosetioce())
            {
                posetilac.ListaZelja.Clear();

                foreach (string isbn in posetilac.IsbnKnjigaNaListiZelja)
                {
                    Knjiga knjiga = knjige.DobaviKnjigu(isbn);

                    if (knjiga != null)
                    {
                        posetilac.ListaZelja.Add(knjiga);
                    }
                }
            }
        }


        public bool DodajKnjiguNaListuZelja(string brojKarte, string isbn)
        {
            Posetilac posetilac = posetioci.DobaviPosetioca(brojKarte);
            Knjiga knjiga = knjige.DobaviKnjigu(isbn);

            if (posetilac == null || knjiga == null)
            {
                return false;
            }

            if (posetilac.IsbnKnjigaNaListiZelja.Contains(knjiga.ISBN))
            {
                return false;
            }

            posetilac.IsbnKnjigaNaListiZelja.Add(knjiga.ISBN);
            posetilac.ListaZelja.Add(knjiga);
            posetioci.SacuvajPosetioce();

            return true;
        }
    }
}
