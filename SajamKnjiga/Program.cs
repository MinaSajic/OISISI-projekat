using SajamKnjiga.Core.Models;
using SajamKnjiga.Core.Storage;
using System.ComponentModel.Design;
using System.Runtime.Versioning;
using System.Text;
using System.IO;
using SajamKnjiga.Core.Services;

namespace SajamKnjiga
{
    internal class Program
    {
       
        // TODO
        private static KnjigaService knjigeServis;

        //private static List<Posetilac> posetioci;  // obrisati i ostale kada se implementiraju service i DAO klase za njih
        // TODO: Mia
        private static List<Autor> autori;  // ovo obrisati nakon implementacije AutorDAO i AutorService
       
        private static KupovinaService kupovineServis;

        static void Main(string[] args)
        { 
            
         
            knjigeServis = new KnjigaService();

            kupovineServis = new KupovinaService();

            //posetioci = UcitajPosetioce(); // obrisati i ostale kada se implementiraju service i DAO klase za njih
            autori = UcitajAutore(); // TODO: Mia
                                     // ovo obrisati nakon implemetnacije AutorDAO i AutorService i napravi objekat autoriServis

            bool izlazIzProgama = false;

            while (!izlazIzProgama)
            {
                Console.WriteLine();
                Console.WriteLine("Izaberi od ponudjenih: ");
                Console.WriteLine("5 Prikaz autora");
                Console.WriteLine("6 Dodavanje autora");
                Console.WriteLine("7 Izmena autora");
                Console.WriteLine("8 Brisanje autora");
                Console.WriteLine("9 Prikaz knjiga");
                Console.WriteLine("10 Dodavanje knjige");
                Console.WriteLine("11 Izmena knjige");
                Console.WriteLine("12 Brisanje knjige");
                Console.WriteLine("0 Izlaz iz programa");

                string odabranaOpcija = Console.ReadLine();

                switch (odabranaOpcija)
                {
                   
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
                        DodavanjeKnjige();
                        break;
                    case "11":
                        IzmeniKnjigu();
                        break;
                    case "12":
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

            //SacuvajPosetioce(posetioci);
            SacuvajAutore(autori); // TODO: Mia
           // SacuvajKnjige(knjige);
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

        private static List<Kupovina> UcitajKupovinu()
        {
            Storage<Kupovina> storage = new Storage<Kupovina>("Kupovine.txt");
            List<Kupovina> kupovina = storage.Load();

            return kupovina;
        }

        private static void SacuvajKupovinu(List<Kupovina> kupovina)
        {
            Storage<Kupovina> storage = new Storage<Kupovina>("Kupovine.txt");
            storage.Save(kupovina);
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
            List<Knjiga> knjige = knjigeServis.DobaviSveKnjige();
            foreach (Knjiga knjiga in knjige)
            {
                Console.WriteLine(knjiga);
            }
        }

        private static void DodavanjeKnjige()
        {
            Knjiga knjiga = new Knjiga();

            Console.WriteLine("Unesi ISBN knjige: ");
            knjiga.ISBN = Console.ReadLine();

            Console.WriteLine("Unesi naziv knjige: ");
            knjiga.Naziv = Console.ReadLine();

            Console.WriteLine("Unesi zanr knjige: ");
            knjiga.Zanr = Console.ReadLine();

            Console.WriteLine("Unesi godinu izdanja knjige: ");
            knjiga.GodinaIzdanja = int.Parse(Console.ReadLine());

            Console.WriteLine("Unesi cenu knjige: ");
            knjiga.Cena = int.Parse(Console.ReadLine());

            Console.WriteLine("Unesi broj strana knjige: ");
            knjiga.BrojStrana = int.Parse(Console.ReadLine());

            knjigeServis.DodavanjeKnjige(knjiga);

            // Mina
            // TODO 
            // Autori,PosetiociKojiSuKupili,PosetiociListaZelja -> ovo trebam kasnije implementirati za dodavanje_knjige


        }

        private static void IzmeniKnjigu()
        {
            Console.WriteLine("Unesi ISBN knjige: ");
            string isbn = Console.ReadLine();

            if (!knjigeServis.PostojiLiKnjiga(isbn))
            {
                Console.WriteLine("Knjiga sa unetim ISBN-om ne postoji");
                return;
            }

            Knjiga knjiga = knjigeServis.DobaviKnjigu(isbn);

            Console.WriteLine("Menjanje podatka za: ");
            Console.WriteLine("1 ISBN");
            Console.WriteLine("2 Naziv");
            Console.WriteLine("3 Zanr");
            Console.WriteLine("4 Godinu izdanja");
            Console.WriteLine("5 Cenu");
            Console.WriteLine("6 Broj strana");
            Console.WriteLine("7 Autore");
            Console.WriteLine("8 Izdavaca");

            string odabranaOpcija = Console.ReadLine();

            Console.WriteLine("Unesi novu vrednost");

            string nova_vrednost = Console.ReadLine();

            switch (odabranaOpcija)
            {
                case "1":
                    knjiga.ISBN = nova_vrednost;
                    break;
                case "2":
                    knjiga.Naziv = nova_vrednost;
                    break;
                case "3":
                    knjiga.Zanr = nova_vrednost;
                    break;
                case "4":
                    knjiga.GodinaIzdanja = int.Parse(nova_vrednost);
                    break;
                case "5":
                    knjiga.Cena = int.Parse(nova_vrednost);
                    break;
                case "6":
                    knjiga.BrojStrana = int.Parse(nova_vrednost);
                    break;
                case "7":
                    knjiga.ISBN = nova_vrednost;
                    break;

            }

            knjigeServis.IzmeniKnjigu(knjiga);
            Console.WriteLine("Izmena je sacuvana.");


        }

        private static void ObrisiKnjigu()
        {
            Console.WriteLine("Unesi ISBN knjige koju zelis da obrises:");
            string isbn = Console.ReadLine();

            if (knjigeServis.IzbrisiKnjigu(isbn)){
                Console.WriteLine("Knjiga je obrisana iz liste");
            } else
            {
                Console.WriteLine("Knjiga sa unetim ISBN-om ne postoji");
            }

      
        }

       

       


        private static void PretragaPosetilaca()
        {

        }
    }
}
