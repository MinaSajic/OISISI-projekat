using SajamKnjiga.Core.Models;
using SajamKnjiga.Core.Storage;
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
                Console.WriteLine("3 Prikaz autora");
                Console.WriteLine("4 Dodavanje autora");
                Console.WriteLine("5 Izmena autora");
                Console.WriteLine("6 Brisanje autora");
                Console.WriteLine("7 Prikaz knjiga");
                Console.WriteLine("8 Obrisi knjigu");
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
                        PrikaziAutore();
                        break;
                    case "4":
                        DodajAutora();
                        break;
                    case "5":
                        IzmeniAutora();
                        break;
                    case "6":
                        ObrisiAutora();
                        break;
                    case "7":
                        PrikaziKnjige();
                        break;
                    case "8":
                        ObrisiKnjigu();
                        break;
                    case "0":
                        izlazIzProgama = true;
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
                Console.WriteLine();
                Console.WriteLine(posetilac.Ime);
                Console.WriteLine(posetilac.Prezime);
                Console.WriteLine(posetilac.DatumRodjenja);
                Console.WriteLine(posetilac.Adresa.Ulica);
                Console.WriteLine(posetilac.Adresa.Broj);
                Console.WriteLine(posetilac.Adresa.Grad);
                Console.WriteLine(posetilac.Adresa.Drzava);
                Console.WriteLine(posetilac.KontaktTelefon);
                Console.WriteLine(posetilac.Email);
                Console.WriteLine(posetilac.BrojClanskeKarte);
                Console.WriteLine(posetilac.TrenutnaGodinaClanstva);
                Console.WriteLine(posetilac.Status);
                Console.WriteLine(posetilac.ProsecnaOcenaRecenzija);

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
            //    posetilac.DatumRodjenja = Console.ReadLine();

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

        private static void SacuvajAutore (List<Autor> autori)
        {
            Storage<Autor> storage = new Storage<Autor>("Autori.txt");
            storage.Save(autori);
        }

        private static void PrikaziAutore()
        {
            Console.WriteLine("Prikaz autora:");

            foreach(Autor autor in autori)
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

            Autor autor = autori.Find(autor => autor.LK == lk);

            if(autor == null)
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

            switch(izmena)
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

            if(autor == null)
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

            foreach(Knjiga knjiga in knjige)
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

            if(knjiga == null)
            {
                Console.WriteLine("Knjiga nije pronadjena!");
                return;
            }

            knjige.Remove(knjiga);
            Console.WriteLine("Knjiga je uspesno obrisana!");
        }
    }
}