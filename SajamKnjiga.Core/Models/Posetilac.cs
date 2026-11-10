using SajamKnjiga.Core.Serialization;
using System.Globalization;

namespace SajamKnjiga.Core.Models
{
    public enum StatusPosetioca
    {
        R,
        V
    }
    public class Posetilac : ISerializable
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateOnly DatumRodjenja { get; set; }
        public Adresa Adresa { get; set; }
        public string KontaktTelefon { get; set; }
        public string Email { get; set; }

        public string BrojClanskeKarte { get; set; }
        public string TrenutnaGodinaClanstva { get; set; }
        public StatusPosetioca Status { get; set; }
        public double ProsecnaOcenaRecenzija { get; set; }

        public override string ToString()
        {
            return $"{BrojClanskeKarte} | {Ime} | {Prezime} | " +
                   $"{DatumRodjenja} | {Adresa.Ulica} | {Adresa.Broj} | {Adresa.Grad} | {Adresa.Drzava} " +
                   $"{KontaktTelefon} | {Email} | {TrenutnaGodinaClanstva} | {Status} | {ProsecnaOcenaRecenzija}";
            // TO DO
            // Malo lepsi format od ovog da napravim za konzolu
        }

        public void FromCSV(string[] values)
        {
            Ime = values[0];
            Prezime = values[1];
            DatumRodjenja = DateOnly.ParseExact(values[2],new[] { "d.M.yyyy.", "d.M.yyyy", "M'/'d'/'yyyy" });
            
            Adresa = new Adresa()
            {
                Ulica = values[3],
                Broj = values[4],
                Grad = values[5],
                Drzava = values[6],
            };

            KontaktTelefon = values[7];
            Email = values[8];
            BrojClanskeKarte = values[9];
            TrenutnaGodinaClanstva = values[10];
            Status = Enum.Parse<StatusPosetioca>(values[11]);
            ProsecnaOcenaRecenzija = double.Parse(values[12]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Ime,
                Prezime,
                DatumRodjenja.ToString("d.M.yyyy."),
                Adresa.Ulica,
                Adresa.Broj,
                Adresa.Grad,
                Adresa.Drzava,
                KontaktTelefon,
                Email,
                BrojClanskeKarte,
                TrenutnaGodinaClanstva,
                Status.ToString(),
                ProsecnaOcenaRecenzija.ToString()
            };
            return csvValues;
        }
    }
}
