using SajamKnjiga.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SajamKnjiga.Controllers
{
    internal class KnjigaController
    {
        private KnjigaService knjigeServis;

        public KnjigaController(KnjigaService knjigeServis)
        {
            this.knjigeServis = knjigeServis;
        }



    }
}
