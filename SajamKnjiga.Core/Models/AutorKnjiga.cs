using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SajamKnjiga.Core.Serialization;

namespace SajamKnjiga.Core.Models
{
    public class AutorKnjiga : ISerializable
    {

        public string LKAutora {  get; set; }

        public string ISBNKnjige { get; set; }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                LKAutora,
                ISBNKnjige,
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            LKAutora = values[0];
            ISBNKnjige = values[1];
        }

    }
}
