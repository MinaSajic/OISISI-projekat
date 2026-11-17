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
        public AutorService AutoriServis { get; }
        public KnjigaService KnjigeServis { get; }

        public PovezivanjeEntiteta()
        {
            autori = new AutorDAO();
            knjige = new KnjigaDAO();

            AutoriServis = new AutorService(autori);
            KnjigeServis = new KnjigaService(knjige);

            poveziAutoreIKnjige();
        }

        private void poveziAutoreIKnjige()
        {
            foreach(Autor autor in autori.DobaviSveAutore())
            {
                autor.Knjige.Clear();
            }
            foreach(Knjiga knjiga in knjige.DobaviSveKnjige())
            {
                knjiga.Autori.Clear();

                foreach(string lk in knjiga.LkAutora)
                {
                    Autor autor = autori.DobaviAutora(lk);

                    if(autor != null)
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

            if(knjiga == null || autor == null)
            {
                return false;
            }

            if(knjiga.LkAutora.Contains(lk))
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

    }
}
