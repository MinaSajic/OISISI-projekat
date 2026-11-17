using SajamKnjiga.Core.Models;
using SajamKnjiga.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SajamKnjiga.Controllers
{
    internal class KnjigaController
    {
        private KnjigaService knjigeServis;

        public KnjigaController(KnjigaService knjigeServis)
        {
            this.knjigeServis = knjigeServis;
        }

        public void Meni()
        {
            bool izlazIzProgama = false;



            while (!izlazIzProgama)
            {
                Console.WriteLine();
                Console.WriteLine("Izaberi od ponudjenih");
                Console.WriteLine("1 Prikaz knjiga");
                Console.WriteLine("2 Prikaz podataka o odredjenoj knjizi");
                Console.WriteLine("3 Dodavanje knjige");
                Console.WriteLine("4 Izmena knjige");
                Console.WriteLine("5 Brisanje knjige");
                Console.WriteLine("0 Nazad");

                string odabranaOpcija = Console.ReadLine();


                switch (odabranaOpcija)
                {
                    case "1":
                        PrikaziKnjige();
                        break;
                    case "2":
                        PrikaziKnjigu();
                        break;
                    case "3":
                        DodavanjeKnjige();
                        break;
                    case "4":
                        IzmeniKnjigu();
                        break;
                    case "5":
                        ObrisiKnjigu();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Pogresna opcija");
                        break;
                }
            }
        }

        private void PrikaziKnjige()
        {
            List<Knjiga> knjige = knjigeServis.DobaviSveKnjige();

            Console.WriteLine();

            ConsoleColor prethodnaBoja = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine("KNJIGE SAJMA");
            Console.WriteLine();

            Console.WriteLine(
                $"{"ISBN",-18}{"Naziv",-30}{"Zanr",-20}{"Godina",8}{"Cena",12}");

            Console.WriteLine(new string('-', 88));

            Console.ForegroundColor = prethodnaBoja;

            foreach (Knjiga knjiga in knjige)
            {
                Console.WriteLine(
                    $"{knjiga.ISBN,-18}{knjiga.Naziv,-30}{knjiga.Zanr,-20}{knjiga.GodinaIzdanja,8}{knjiga.Cena,12}");
            }

            Console.WriteLine();
            Console.WriteLine($"Ukupno knjiga: {knjige.Count}");
        }

        private void PrikaziKnjigu()
        {
            Console.WriteLine("Unesi ISBN knjige:");
            string isbn = Console.ReadLine();

            Knjiga knjiga = knjigeServis.DobaviKnjigu(isbn);

            if (knjiga == null)
            {
                Console.WriteLine("Knjiga nije pronadjena!");
                return;
            }

            Console.WriteLine();

            ConsoleColor prethodnaBoja = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine("PODACI O KNJIZI");
            Console.WriteLine(new string('-', 48));

            Console.ForegroundColor = prethodnaBoja;

            Console.WriteLine($"{"ISBN:",-20}{knjiga.ISBN}");
            Console.WriteLine($"{"Naziv:", -20}{knjiga.Naziv}");
            Console.WriteLine($"{"Zanr:", -20}{knjiga.Zanr}");
            Console.WriteLine($"{"Godina izdanja:", -20}{knjiga.GodinaIzdanja}");
            Console.WriteLine($"{"Cena:", -20}{knjiga.Cena} RSD");
            Console.WriteLine($"{"Broj strana:", -20}{knjiga.BrojStrana}");
            Console.WriteLine();
        }

        private void DodavanjeKnjige()
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

        private void IzmeniKnjigu()
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

        private void ObrisiKnjigu()
        {
            Console.WriteLine("Unesi ISBN knjige koju zelis da obrises:");
            string isbn = Console.ReadLine();

            if (knjigeServis.IzbrisiKnjigu(isbn))
            {
                Console.WriteLine("Knjiga je obrisana iz liste");
            }
            else
            {
                Console.WriteLine("Knjiga sa unetim ISBN-om ne postoji");
            }


        }






    }

}

