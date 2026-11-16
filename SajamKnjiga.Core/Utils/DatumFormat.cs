using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SajamKnjiga.Core.Utils
{
    public static class DatumFormat
    {
        public const string Format = "yyyy-MM-dd";

        public static string DatumUTekst(DateOnly datum)
        {
            return datum.ToString(Format);
        }

        public static DateOnly TekstUDatum(string text)
        {
            return DateOnly.ParseExact(text, Format);
        }
    }
}
