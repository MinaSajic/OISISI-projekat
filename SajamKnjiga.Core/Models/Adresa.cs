using SajamKnjiga.Core.Serialization;

namespace SajamKnjiga.Core.Models
{
    public class Adresa : ISerializable
    {
        public int Id { get; set; }
        public string Ulica { get; set; } 
        public string Broj { get; set; } 
        public string Grad { get; set; } 
        public string Drzava { get; set; }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Ulica,
                Broj,
                Grad,
                Drzava
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Ulica = values[1];
            Broj = values[2];
            Grad = values[3];
            Drzava = values[4];
        }
    }
}
