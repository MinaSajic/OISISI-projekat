using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SajamKnjiga.Core.Models;
using SajamKnjiga.Core.Services;

namespace SajamKnjiga.Controllers
{
    internal class AutorController
    {
        private AutorService autoriServis;

        public AutorController(AutorService autoriServis)
        {
            this.autoriServis = autoriServis;
        }

        public void Meni()
        {
            bool izlazIzPrograma = false;

            while (!izlazIzPrograma)
            {
                Console.WriteLine();
                Console.WriteLine("Izaberi od ponudjenih:");
                Console.WriteLine("1 Prikaz autora");
                Console.WriteLine("2 Prikaz podataka o autoru");
                Console.WriteLine("3 Dodavanje autora");
                Console.WriteLine("4 Izmena autora");
                Console.WriteLine("5 Brisanje autora");
                Console.WriteLine("0 Nazad");

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
            List<Autor> autori = autoriServis.DobaviSveAutore();

            Console.WriteLine();

            ConsoleColor prethodnaBoja = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine("AUTORI SAJMA");
            Console.WriteLine();

            Console.WriteLine(
                $"{"Broj LK", -16}{"Ime i prezime", -25}{"Grad", -20}{"Iskustvo",10}");
            Console.WriteLine(new string('-', 71));

            Console.ForegroundColor = prethodnaBoja;

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
            Console.WriteLine("Unesi broj licne karte autora:");
            string lk = Console.ReadLine();

            Autor autor = autoriServis.DobaviAutora(lk);

            if(autor == null)
            {
                Console.WriteLine("Autor nije pronadjen!");
                return;
            }

            Console.WriteLine();

            ConsoleColor prethodnaBoja = Console.ForegroundColor;
            Console.ForegroundColor= ConsoleColor.Magenta;

            Console.WriteLine("PODACI O AUTORU");
            Console.WriteLine(new String('-', 48));

            Console.ForegroundColor = prethodnaBoja;

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
    }
}
