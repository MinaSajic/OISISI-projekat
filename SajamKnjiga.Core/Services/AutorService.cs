using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SajamKnjiga.Core.DAO;
using SajamKnjiga.Core.Models;

namespace SajamKnjiga.Core.Services
{
    public class AutorService
    {
        private AutorDAO autorDAO;


        public AutorService()
        {
            autorDAO = new AutorDAO();
        }
        internal AutorService(AutorDAO autorDAO)
        {
            this.autorDAO = autorDAO;
        }

        public List<Autor> DobaviSveAutore()
        {
            return autorDAO.DobaviSveAutore();
        } 

        public Autor DobaviAutora(string lk)
        {
            return autorDAO.DobaviAutora(lk);
        }

        public bool PostojiLiAutor(string lk)
        {
            return autorDAO.DobaviAutora(lk) != null; 
        }

        public void DodajAutora(Autor autor)
        { 
            if(PostojiLiAutor(autor.LK))
            {
                return;
            }

            autorDAO.DodajAutora(autor);
        }

        public void ObrisiAutora(string lk)
        {
            if(!PostojiLiAutor(lk))
            {
                return;
            }

            autorDAO.ObrisiAutora(lk);
        }

        public void IzmeniAutora(Autor autor)
        {
            if(!PostojiLiAutor(autor.LK))
            {
                return;
            }

            autorDAO.IzmeniAutora(autor);
        }

    }
}
