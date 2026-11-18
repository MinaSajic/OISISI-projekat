using SajamKnjiga.Core.Serialization;
using SajamKnjiga.Core.Utils;
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

        public List<string> IsbnKnjigaNaListiZelja { get; set; } = new List<string>();

        public List<Knjiga> ListaZelja { get; set; } = new List<Knjiga>();

        public List<string> KupovinePosetioca { get; set; } = new List<string>();

        public List<Kupovina> Kupovine { get; set; } = new List<Kupovina>();

        public override string ToString()
        {
            return $"{BrojClanskeKarte} | {Ime} | {Prezime} | " +
                   $"{DatumRodjenja} | {Adresa.Ulica} | {Adresa.Broj} | {Adresa.Grad} | {Adresa.Drzava} " +
                   $"{KontaktTelefon} | {Email} | {TrenutnaGodinaClanstva} | {Status} | {ProsecnaOcenaRecenzija}";
            // TO DO
            // Malo lepsi format od ovog da napravim za konzolu
        }

        public Posetilac Kopija()
        {
            return new Posetilac { 
            Ime = Ime,
            Prezime = Prezime,
            DatumRodjenja = DatumRodjenja,
            Adresa = new Adresa
            {
                Ulica = Adresa.Ulica,
                Broj = Adresa.Broj,
                Grad = Adresa.Grad,
                Drzava = Adresa.Drzava,
            },
            KontaktTelefon = KontaktTelefon,
            Email = Email,
            BrojClanskeKarte = BrojClanskeKarte,
            TrenutnaGodinaClanstva = TrenutnaGodinaClanstva,
            Status = Status,
            ProsecnaOcenaRecenzija = ProsecnaOcenaRecenzija,
            IsbnKnjigaNaListiZelja = new List<string>(IsbnKnjigaNaListiZelja)
            };
        }
        public void FromCSV(string[] values)
        {
            Ime = values[0];
            Prezime = values[1];
            DatumRodjenja = DatumFormat.TekstUDatum(values[2]);
            
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
            IsbnKnjigaNaListiZelja = new List<string>();
            if (values.Length > 13 && !string.IsNullOrWhiteSpace(values[13]))
            {
                IsbnKnjigaNaListiZelja = values[13]
                    .Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .ToList();
            }
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Ime,
                Prezime,
                DatumFormat.DatumUTekst(DatumRodjenja),
                Adresa.Ulica,
                Adresa.Broj,
                Adresa.Grad,
                Adresa.Drzava,
                KontaktTelefon,
                Email,
                BrojClanskeKarte,
                TrenutnaGodinaClanstva,
                Status.ToString(),
                ProsecnaOcenaRecenzija.ToString(),
                string.Join(";", IsbnKnjigaNaListiZelja)
            };
            return csvValues;
        }
    }
}
