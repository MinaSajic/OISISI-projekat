using SajamKnjiga.Core.DAO;
using SajamKnjiga.Core.Models;
using SajamKnjiga.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SajamKnjiga.Controllers
{
    internal class AutorController
    {
        private AutorService autoriServis;
        private KnjigaService knjigeServis;

        private PovezivanjeEntiteta povezivanje;

        public AutorController(AutorService autoriServis, KnjigaService knjigeServis,PovezivanjeEntiteta povezivanje)
        {
            this.autoriServis = autoriServis;
            this.knjigeServis = knjigeServis;
            this.povezivanje = povezivanje;
        }

        public void Meni()
        {
            bool izlazIzPrograma = false;

            while (!izlazIzPrograma)
            {
                Console.WriteLine();
                Console.WriteLine("1 Prikaz autora");
                Console.WriteLine("2 Prikaz podataka o autoru");
                Console.WriteLine("3 Dodavanje autora");
                Console.WriteLine("4 Izmena autora");
                Console.WriteLine("5 Brisanje autora");
                Console.WriteLine("6 Dodavanje knjige autoru");
                Console.WriteLine("7 Uklanjanje knjige sa autora");
                Console.WriteLine("0 Nazad");
                Console.WriteLine();
                Console.Write("Izaberi od ponudjenih: ");

                string odabranaOpcija = Console.ReadLine();
                
                switch(odabranaOpcija)
                {
                    case "1":
                        PrikaziAutore();
                        break;
                    case "2":
                        PrikaziAutora();
                        break;
                    case "3":
                        DodajAutora();
                        break;
                    case "4":
                        IzmeniAutora();
                        break;
                    case "5":
                        ObrisiAutora();
                        break;
                    case "6":
                        DodajKnjiguAutoru();
                        break;
                    case "7":
                        SkloniKnjiguSaAutora();
                        break;
                    case "0":
                        izlazIzPrograma = true;
                        break;
                    default:
                        Console.WriteLine("Pogresna opcija");
                        break;

                }
                
            }
        }
        private void PrikaziAutore()
        {
            IspisiZaglavlje("AUTORI SAJMA", 71, $"{"Broj LK",-16}{"Ime i prezime",-25}{"Grad",-20}{"Iskustvo",10}");

            List<Autor> autori = autoriServis.DobaviSveAutore();

            foreach (Autor autor in autori)
            {
                string punoIme = $"{autor.Ime} {autor.Prezime}";
                string grad = autor.Adresa?.Grad ?? "-";

                Console.WriteLine(
                    $"{autor.LK, -16}{punoIme, -25}{grad, -20}{autor.GodineIskustva, 10}");
            }

            Console.WriteLine();
            Console.WriteLine($"Ukupno autora: { autori.Count}");
        }

        private void PrikaziAutora()
        {

            IspisiListuAutora();

            Console.WriteLine();
            Console.Write("Unesi broj licne karte autora: ");
            string lk = Console.ReadLine();

            Autor autor = autoriServis.DobaviAutora(lk);

            if(autor == null)
            {
                Console.WriteLine("Autor nije pronadjen!");
                return;
            }

            IspisiZaglavlje("PODACI O AUTORU", 48);

            Console.WriteLine($"{"Broj LK:", -20}{autor.LK}");
            Console.WriteLine($"{"Ime:", -20}{autor.Ime}");
            Console.WriteLine($"{"Prezime:", -20}{autor.Prezime}");
            Console.WriteLine($"{"Datum rodjenja:", -20}{autor.DatumRodjenja:dd.MM.yyyy.}");
            Console.WriteLine($"{"Ulica:", -20}{autor.Adresa?.Ulica??"-"}");
            Console.WriteLine($"{"Broj:", -20}{autor.Adresa?.Broj??"-"}");
            Console.WriteLine($"{"Grad:", -20}{autor.Adresa?.Grad??"-"}");
            Console.WriteLine($"{"Drzava:", -20}{autor.Adresa?.Drzava??"-"}");
            Console.WriteLine($"{"Telefon:", -20}{autor.Telefon}");
            Console.WriteLine($"{"Email:", -20}{ autor.E_mail} ");
            Console.WriteLine($"{"Godine iskustva:", -20}{autor.GodineIskustva}");
            Console.WriteLine();

            Console.WriteLine("Knjige: ");
            Console.WriteLine();
            if (autor.Knjige.Count == 0)
            {
                Console.WriteLine("Nema povezanih autora!");
            }
            else
            {
                for (int i = 0; i < autor.Knjige.Count; i++)
                {
                    Knjiga knjiga = autor.Knjige[i];
                    Console.WriteLine($" {i + 1}. {knjiga.Naziv} - {knjiga.Zanr} (ISBN: {knjiga.ISBN})");
                }
            }
        }

        private void DodajAutora()
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

            if (autoriServis.PostojiLiAutor(autor.LK))
            {
                Console.WriteLine("Autor sa tim brojem licne karte vec postoji.");
            }
            else
            {
                autoriServis.DodajAutora(autor);
                Console.WriteLine("Autor je uspesno dodat!");
            }
        }

        private void IzmeniAutora()
        {
            Console.WriteLine("Unesi broj licne karte autora kojeg zelis da izmenis:");
            string lk = Console.ReadLine();

            Autor autor = autoriServis.DobaviAutora(lk);

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

            autoriServis.IzmeniAutora(autor);
        }


        private void IspisiListuAutora()
        {
            List<Autor> autori = autoriServis.DobaviSveAutore();

            IspisiZaglavlje("AUTORI SAJMA", 30, $"{"Broj LK",-16}{"Ime i prezime",-25}");

            

            foreach (Autor a in autori)
            {
                string punoIme = $"{a.Ime} {a.Prezime}";
                Console.WriteLine($"{a.LK,-16}{punoIme,-25}");
            }

            Console.WriteLine();
        }

        private void IspisiZaglavlje(string naslov, int sirina, string kolone = null)
        {
            Console.WriteLine();

            ConsoleColor prethodnaBoja = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine(naslov);

            if (kolone != null)
            {
                Console.WriteLine();
                Console.WriteLine(kolone);
            }

            Console.WriteLine(new string('-', sirina));

            Console.ForegroundColor = prethodnaBoja;
        }

        private void ObrisiAutora()
        {
            Console.WriteLine("Unesi broj licne karte autora kojeg zelis da obrises:");
            string lk = Console.ReadLine();

            if (!autoriServis.PostojiLiAutor(lk))
            {
                Console.WriteLine("Autor nije pronadjen");
                return;
            }

            autoriServis.ObrisiAutora(lk);
            Console.WriteLine("Autor je uspesno obrisan!");
        }

        private void DodajKnjiguAutoru()
        {

            List<Autor> autori = autoriServis.DobaviSveAutore();

            IspisiZaglavlje("AUTORI SAJMA", 30, $"{"Broj LK",-16}{"Ime i prezime",-25}");

            foreach (Autor autor in autori)
            {
                string punoIme = $"{autor.Ime} {autor.Prezime}";

                Console.WriteLine(
                    $"{autor.LK,-16}{punoIme,-25}");
            }

            Console.WriteLine();

            Console.Write("Unesi lk autora : ");
            string lk = Console.ReadLine();

            List<Knjiga> knjige = knjigeServis.DobaviSveKnjige();

            IspisiZaglavlje("KNJIGE SAJMA", 55, $"{"ISBN",-18}{"Naziv",-30}{"Zanr",-20}");

            foreach (Knjiga knjiga in knjige)
            {
                string autoriKnjige = string.Join(", ",
                    knjiga.Autori.Select(autor => $"{autor.Ime} {autor.Prezime}"));

                Console.WriteLine(
                    $"{knjiga.ISBN,-18}{knjiga.Naziv,-30}{knjiga.Zanr,-20}");
            }

            Console.WriteLine();

            Console.Write("Unesi ISBN knjige : ");
            string isbn = Console.ReadLine();


            bool uspesno = povezivanje.DodajKnjiguAutoru(isbn, lk);

            
            if (uspesno)
            {
                Console.WriteLine("Knjiga je uspesno dodata autoru.");
            }
            else
            {
                Console.WriteLine("Dodavanje nije uspelo. Proveri ISBN i LK!");
            }
        }

        private void SkloniKnjiguSaAutora()
        {

            // PRIKAZ AUTORA



            List<Autor> autori = autoriServis.DobaviSveAutore();

            IspisiZaglavlje("AUTORI SAJMA", 30, $"{"Broj LK",-16}{"Ime i prezime",-25}");

            foreach (Autor autor in autori)
            {
                string punoIme = $"{autor.Ime} {autor.Prezime}";

                Console.WriteLine(
                    $"{autor.LK,-16}{punoIme,-25}");
            }

            Console.WriteLine();

            Console.Write("Unesi broj LK autora: ");
            string lk = Console.ReadLine();

            // PRIKAZ KNJIGA 

            List<Knjiga> knjige = knjigeServis.DobaviSveKnjige();

            IspisiZaglavlje("KNJIGE SAJMA", 55, $"{"ISBN",-18}{"Naziv",-30}{"Zanr",-20}");

            foreach (Knjiga knjiga in knjige)
            {
                string autoriKnjige = string.Join(", ",
                    knjiga.Autori.Select(autor => $"{autor.Ime} {autor.Prezime}"));

                Console.WriteLine(
                    $"{knjiga.ISBN,-18}{knjiga.Naziv,-30}{knjiga.Zanr,-20}");
            }

            Console.WriteLine();

            Console.Write("Unesi ISBN knjige: ");
            string isbn = Console.ReadLine();

            bool uspesno = povezivanje.UkloniKnjiguSaAutora(isbn, lk);


            if (uspesno)
            {
                Console.WriteLine("Knjiga je uspesno uklonjena sa autora.");
            }
            else
            {
                Console.WriteLine("Uklanjanje nije uspelo. Proveri ISBN i LK!");
            }
        }
    }
}
