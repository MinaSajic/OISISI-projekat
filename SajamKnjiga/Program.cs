using SajamKnjiga.Core.Models;
using SajamKnjiga.Core.Storage;
using System.ComponentModel.Design;
using System.Runtime.Versioning;
using System.Text;
using System.IO;

namespace SajamKnjiga
{
    internal class Program
    {
        private static List<Posetilac> posetioci;
        private static List<Autor> autori;
        private static List<Knjiga> knjige;

        static void Main(string[] args)
        {
            posetioci = UcitajPosetioce();
            autori = UcitajAutore();
            knjige = UcitajKnjige();
            bool izlazIzProgama = false;

            while (!izlazIzProgama)
            {
                Console.WriteLine();
                Console.WriteLine("Izaberi od ponudjenih: ");
                Console.WriteLine("1 Prikaz posetilaca");
                Console.WriteLine("2 Dodavanje posetilaca");
                Console.WriteLine("3 Brisanje posetioca");
                Console.WriteLine("4 Izmena posetioca");
                Console.WriteLine("5 Prikaz autora");
                Console.WriteLine("6 Dodavanje autora");
                Console.WriteLine("7 Izmena autora");
                Console.WriteLine("8 Brisanje autora");
                Console.WriteLine("9 Prikaz knjiga");
                Console.WriteLine("10 Obrisi knjigu");
                Console.WriteLine("0 Izlaz iz programa");

                string odabranaOpcija = Console.ReadLine();

                switch (odabranaOpcija)
                {
                    case "1":
                        PrikaziPosetioce();
                        break;
                    case "2":
                        DodajPosetioca();
                        break;
                    case "3":
                        BrisanjePosetioca();
                        break;
                    case "4":
                        IzmeniPosetioca();
                        break;
                    case "5":
                        PrikaziAutore();
                        break;
                    case "6":
                        DodajAutora();
                        break;
                    case "7":
                        IzmeniAutora();
                        break;
                    case "8":
                        ObrisiAutora();
                        break;
                    case "9":
                        PrikaziKnjige();
                        break;
                    case "10":
                        ObrisiKnjigu();
                        break;
                    case "0":
                        izlazIzProgama = true;  // tek nakon izlaska iz programa (izborom 0 u terminalu) mi se ukloni posetilac u datoteci Posetioci.txt, ne pre
                        break;
                    default:
                        Console.WriteLine("Pogresna opcija");
                        break;
                }
            }

            SacuvajPosetioce(posetioci);
            SacuvajAutore(autori);
            SacuvajKnjige(knjige);
        }

        private static void PrikaziPosetioce()
        {

            Console.WriteLine("Prikaz posetioca:");
            foreach (Posetilac posetilac in posetioci)
            {
                Console.WriteLine(posetilac);

            }


        }

        private static void DodajPosetioca()
        {
            Console.WriteLine("Dodavanje posetioca");
            Posetilac posetilac = new Posetilac();

            Console.WriteLine("Unesi ime: ");
            posetilac.Ime = Console.ReadLine();

            Console.WriteLine("Unesi prezime: ");
            posetilac.Prezime = Console.ReadLine();

            Console.WriteLine("Unesi datum rodjenja: ");
            if (!DateOnly.TryParseExact(Console.ReadLine(), new[] { "d.M.yyyy", "d.M.yyyy." }, null, System.Globalization.DateTimeStyles.None, out DateOnly datumRodjenja))
            {
                Console.WriteLine("Neispravan datum.");
                return;
            }

            posetilac.DatumRodjenja = datumRodjenja;

            posetilac.Adresa = new Adresa();

            Console.WriteLine("Unesi ulicu: ");
            posetilac.Adresa.Ulica = Console.ReadLine();

            Console.WriteLine("Unesi broj: ");
            posetilac.Adresa.Broj = Console.ReadLine();

            Console.WriteLine("Unesi grad: ");
            posetilac.Adresa.Grad = Console.ReadLine();

            Console.WriteLine("Unesi drzavu: ");
            posetilac.Adresa.Drzava = Console.ReadLine();

            Console.WriteLine("Unesi kontakt telefon: ");
            posetilac.KontaktTelefon = Console.ReadLine();

            Console.WriteLine("Unesi email: ");
            posetilac.Email = Console.ReadLine();

            Console.WriteLine("Unesi br.clanske karte: ");
            posetilac.BrojClanskeKarte = Console.ReadLine();

            Console.WriteLine("Unesi trenutnu godinu clanstva: ");
            posetilac.TrenutnaGodinaClanstva = Console.ReadLine();

            Console.WriteLine("Unesi status: ");
            posetilac.Status = Enum.Parse<StatusPosetioca>(Console.ReadLine());

            Console.WriteLine("Unesi prosecnu ocenu recenzija: ");
            posetilac.ProsecnaOcenaRecenzija = double.Parse(Console.ReadLine());

            posetioci.Add(posetilac);
        }



        private static void SacuvajPosetioce(List<Posetilac> posetioci)
        {
            Storage<Posetilac> storage = new Storage<Posetilac>("Posetioci.txt");
            storage.Save(posetioci);
        }

        private static List<Posetilac> UcitajPosetioce()
        {
            Storage<Posetilac> storage = new Storage<Posetilac>("Posetioci.txt");
            List<Posetilac> posetioci = storage.Load();

            return posetioci;
        }

        private static List<Autor> UcitajAutore()
        {
            Storage<Autor> storage = new Storage<Autor>("Autori.txt");
            List<Autor> autori = storage.Load();

            return autori;
        }

        private static void SacuvajAutore(List<Autor> autori)
        {
            Storage<Autor> storage = new Storage<Autor>("Autori.txt");
            storage.Save(autori);
        }

        private static void PrikaziAutore()
        {
            Console.WriteLine("Prikaz autora:");

            foreach (Autor autor in autori)
            {
                Console.WriteLine(autor.Ime);
                Console.WriteLine(autor.Prezime);
                Console.WriteLine(autor.DatumRodjenja);
                Console.WriteLine(autor.Adresa.Ulica);
                Console.WriteLine(autor.Adresa.Broj);
                Console.WriteLine(autor.Adresa.Grad);
                Console.WriteLine(autor.Adresa.Drzava);
                Console.WriteLine(autor.Telefon);
                Console.WriteLine(autor.E_mail);
                Console.WriteLine(autor.LK);
                Console.WriteLine(autor.GodineIskustva);
                Console.WriteLine();
            }
        }

        private static void DodajAutora()
        {
            Console.WriteLine("Dodavanje autora:");

            Autor autor = new Autor();

            Console.WriteLine("Unesi ime:");
            autor.Ime = Console.ReadLine();

            Console.WriteLine("Unesi prezime:");
            autor.Prezime = Console.ReadLine();

            Console.WriteLine("Unesi datum rodjenja (yyyy-MM-dd):");
            autor.DatumRodjenja = DateOnly.Parse(Console.ReadLine());

            autor.Adresa = new Adresa();

            Console.WriteLine("Unesi ulicu:");
            autor.Adresa.Ulica = Console.ReadLine();

            Console.WriteLine("Unesi broj:");
            autor.Adresa.Broj = Console.ReadLine();

            Console.WriteLine("Unesi grad:");
            autor.Adresa.Grad = Console.ReadLine();

            Console.WriteLine("Unesi drzavu:");
            autor.Adresa.Drzava = Console.ReadLine();

            Console.WriteLine("Unesi telefon:");
            autor.Telefon = Console.ReadLine();

            Console.WriteLine("Unesi email:");
            autor.E_mail = Console.ReadLine();

            Console.WriteLine("Unesi broj licne karte:");
            autor.LK = Console.ReadLine();

            Console.WriteLine("Unesi godine iskustva:");
            autor.GodineIskustva = int.Parse(Console.ReadLine());

            autori.Add(autor);
        }

        private static void IzmeniAutora()
        {
            Console.WriteLine("Unesi broj licne karte autora kojeg zelis da izmenis:");
            string lk = Console.ReadLine();

            Autor autor = autori.Find(a => a.LK == lk);

            if (autor == null)
            {
                Console.WriteLine("Autor nije pronadjen");
                return;
            }

            Console.WriteLine("Izaberite koje polje zelite da izmenite:");
            Console.WriteLine("1 Izmena imena:");
            Console.WriteLine("2 Izmena prezimena:");
            Console.WriteLine("3 Izmena datuma rodjenja:");
            Console.WriteLine("4 Izmena ulice:");
            Console.WriteLine("5 Izmena broja:");
            Console.WriteLine("6 Izmena grada");
            Console.WriteLine("7 Izmena drzave:");
            Console.WriteLine("8 Izmena telefona");
            Console.WriteLine("9 Izmena email-a");
            Console.WriteLine("10 Izmena godina iskustva");
            Console.WriteLine("0 Nazad");
            string izmena = Console.ReadLine();

            switch (izmena)
            {
                case "1":
                    Console.WriteLine("Unesi novo ime autora:");
                    autor.Ime = Console.ReadLine();
                    Console.WriteLine("Autor je uspesno izmenjen!");
                    break;
                case "2":
                    Console.Write("Unesi novo prezime autora:");
                    autor.Prezime = Console.ReadLine();
                    Console.WriteLine("Autor je uspesno izmenjen!");
                    break;
                case "3":
                    Console.WriteLine("Unesi novi datum rodjenja (yyyy-MM-dd):");
                    autor.DatumRodjenja = DateOnly.Parse(Console.ReadLine());
                    Console.WriteLine("Autor je uspesno izmenjen!");
                    break;
                case "4":
                    Console.WriteLine("Unesi novu ulicu:");
                    autor.Adresa.Ulica = Console.ReadLine();
                    Console.WriteLine("Autor je uspesno izmenjen!");
                    break;
                case "5":
                    Console.WriteLine("Unesi novi broj:");
                    autor.Adresa.Broj = Console.ReadLine();
                    Console.WriteLine("Autor je uspesno izmenjen!");
                    break;
                case "6":
                    Console.WriteLine("Unesi novi grad:");
                    autor.Adresa.Grad = Console.ReadLine();
                    Console.WriteLine("Autor je uspesno izmenjen!");
                    break;
                case "7":
                    Console.WriteLine("Unesi novu drzavu:");
                    autor.Adresa.Drzava = Console.ReadLine();
                    Console.WriteLine("Autor je uspesno izmenjen!");
                    break;
                case "8":
                    Console.WriteLine("Unesi novi telefon:");
                    autor.Telefon = Console.ReadLine();
                    Console.WriteLine("Autor je uspesno izmenjen!");
                    break;
                case "9":
                    Console.WriteLine("Unesi novi email:");
                    autor.E_mail = Console.ReadLine();
                    Console.WriteLine("Autor je uspesno izmenjen!");
                    break;
                case "10":
                    Console.WriteLine("Unesi nove godine iskustva:");
                    autor.GodineIskustva = int.Parse(Console.ReadLine());
                    Console.WriteLine("Autor je uspesno izmenjen!");
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Pogresna opcija");
                    break;
            }
        }

        private static void ObrisiAutora()
        {
            Console.WriteLine("Unesi broj licne karte autora kojeg zelis da obrises:");
            string lk = Console.ReadLine();

            Autor autor = autori.Find(a => a.LK == lk);

            if (autor == null)
            {
                Console.WriteLine("Autor nije pronadjen");
                return;
            }

            autori.Remove(autor);
            Console.WriteLine("Autor je uspesno obrisan!");
        }
        private static List<Knjiga> UcitajKnjige()
        {
            Storage<Knjiga> storage = new Storage<Knjiga>("Knjige.txt");
            List<Knjiga> knjige = storage.Load();

            return knjige;
        }

        private static void SacuvajKnjige(List<Knjiga> knjige)
        {
            Storage<Knjiga> storage = new Storage<Knjiga>("Knjige.txt");
            storage.Save(knjige);
        }

        private static void PrikaziKnjige()
        {
            Console.WriteLine("Prikaz knjiga:");

            foreach (Knjiga knjiga in knjige)
            {
                Console.WriteLine(knjiga.ISBN);
                Console.WriteLine(knjiga.Naziv);
                Console.WriteLine(knjiga.Zanr);
                Console.WriteLine(knjiga.GodinaIzdanja);
                Console.WriteLine(knjiga.Cena);
                Console.WriteLine(knjiga.BrojStrana);
                Console.WriteLine();
            }
        }

        private static void ObrisiKnjigu()
        {
            Console.WriteLine("Unesi ISBN knjige koju zelis da obrises:");
            string isbn = Console.ReadLine();

            Knjiga knjiga = knjige.Find(k => k.ISBN == isbn);

            if (knjiga == null)
            {
                Console.WriteLine("Knjiga nije pronadjena!");
                return;
            }

            knjige.Remove(knjiga);
            Console.WriteLine("Knjiga je uspesno obrisana!");
        }

        private static void BrisanjePosetioca()
        {
            Console.WriteLine("Unesi broj clanske karte posetioca: ");

            string BrK = Console.ReadLine();

            int position = -1;

            for (int i = 0; i < posetioci.Count; i++)
            {
                if (posetioci[i].BrojClanskeKarte == BrK)
                {
                    position = i;
                    break;
                }
            }

            if (position == -1)
            {
                Console.WriteLine("Posetilac nije pronadjen");
                return;
            }

            posetioci.RemoveAt(position);
            Console.WriteLine("Posetilac je obrisan iz liste.");
        }

        private static void IzmeniPosetioca()
        {
            Console.WriteLine("Unesi broj clanske karte: ");
            string BrK = Console.ReadLine();

            int position = -1;

            for (int i = 0; i < posetioci.Count; i++)
            {
                if (posetioci[i].BrojClanskeKarte == BrK)
                {
                    position = i;
                    break;
                }
            }

            if (position == -1)
            {
                Console.WriteLine("Posetilac nije pronadjen");
                return;
            }

            Posetilac posetilac = posetioci[position];

            Console.WriteLine("Menjanje podatka za: ");
            Console.WriteLine("1 Ime");
            Console.WriteLine("2 Prezime");
            Console.WriteLine("3 Datum rodjenja");
            Console.WriteLine("4 Ulicu");
            Console.WriteLine("5 Broj ulice");
            Console.WriteLine("6 Grad");
            Console.WriteLine("7 Drzavu");
            Console.WriteLine("8 Kontakt telefon");
            Console.WriteLine("9 Email");
            Console.WriteLine("10 Broj clanske karte");
            Console.WriteLine("11 Trenutnu godinu clanstva");
            Console.WriteLine("12 Status");
            Console.WriteLine("13 Prosecna ocena recenzija");

            string odabranaOpcija = Console.ReadLine();

            Console.WriteLine("Unesi novu vrednost: ");

            string nova_vrednost = Console.ReadLine();

            switch (odabranaOpcija)
            {
                case "1":
                    posetilac.Ime = nova_vrednost;
                    break;
                case "2":
                    posetilac.Prezime = nova_vrednost;
                    break;
                case "3":
                    posetilac.DatumRodjenja = DateOnly.ParseExact(nova_vrednost, new[] { "d.M.yyyy", "d.M.yyyy." }, null, System.Globalization.DateTimeStyles.None);
                    break;
                case "4":
                    posetilac.Adresa.Ulica = nova_vrednost;
                    break;
                case "5":
                    posetilac.Adresa.Broj = nova_vrednost;
                    break;
                case "6":
                    posetilac.Adresa.Grad = nova_vrednost;
                    break;
                case "7":
                    posetilac.Adresa.Drzava = nova_vrednost;
                    break;
                case "8":
                    posetilac.KontaktTelefon = nova_vrednost;
                    break;
                case "9":
                    posetilac.Email = nova_vrednost;
                    break;
                case "10":
                    posetilac.BrojClanskeKarte = nova_vrednost;
                    break;
                case "11":
                    posetilac.TrenutnaGodinaClanstva = nova_vrednost;               // TODO
                    break;                                                          // Da li je prosecna ocena recenzija podatak posetioca ili nesto drugo? U cemu se razlikuje od ocene?
                case "12":
                    posetilac.Status = Enum.Parse<StatusPosetioca>(nova_vrednost);  // TODO
                    break;                                                          // Da li ovde trebam implementirati pitanje da li je korisnik uneo odgovarajuci format za status,prosecnuocenurecenzija?
                case "13":
                    posetilac.ProsecnaOcenaRecenzija = double.Parse(nova_vrednost);
                    break;

                    // Pitanje: da li treba ovako da implementiram f-ju za izmenu posetioca za neki njegov podatak ili projekat trazi da izmenim sve podatke tog posetioca?
                    // TODO
            }

            SacuvajPosetioce(posetioci);
            Console.WriteLine("Izmena je sacuvana.");

        }
    }
}
