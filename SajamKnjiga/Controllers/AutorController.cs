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
                        //dodati za detaljan prikaz
                        //TODO
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
        //TODO 
        //Mia
        private void PrikaziAutore()
        {
            Console.WriteLine("Prikaz autora:");

            List<Autor> autori = autoriServis.DobaviSveAutore();

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
