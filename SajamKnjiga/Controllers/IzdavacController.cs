using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SajamKnjiga.Core.Models;
using SajamKnjiga.Core.Services;

namespace SajamKnjiga.Controllers
{
    internal class IzdavacController
    {
        private IzdavacService izdavacServis;

        public IzdavacController(IzdavacService izdavacServis)
        {
            this.izdavacServis = izdavacServis;
        }

        public void Meni()
        {
            bool izlazIzPrograma = false;

            while(!izlazIzPrograma)
            {
               
               
                Console.WriteLine("1 Prikaz izdavaca");
                Console.WriteLine("2 Prikaz podataka o izdavacu");
                Console.WriteLine("3 Dodavanje izdavaca");
                Console.WriteLine("4 Izmena izdavaca");
                Console.WriteLine("5 Brisanje izdavaca");
                Console.WriteLine("6 Postavi sefa izdavaca");
                Console.WriteLine("0 Nazad");
                Console.WriteLine();
                Console.Write("Izaberi od ponudjenih:");
                string odabranaOpcija = Console.ReadLine();

                switch(odabranaOpcija)
                {
                    case "1":
                        PrikaziIzdavace();
                        break;
                    case "2":
                        PrikaziIzdavaca();
                        break;
                    case "3":
                        DodajIzdavaca();
                        break;
                    case "4":
                        IzmeniIzdavaca();
                        break;
                    case "5":
                        ObrisiIzdavaca();
                        break;
                    case "6":
                        PostaviSefaIzdavaca();
                        break;
                    case "0":
                        izlazIzPrograma = true;
                        break;
                    default:
                        Console.WriteLine("Pogresna opcija!");
                        break;
                }
            }
        }

        private void PrikaziIzdavace()
        {
            List<Izdavac> izdavaci = izdavacServis.DobaviSveIzdavace();

            Console.WriteLine();

            ConsoleColor prethodnaBoja = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine("IZDAVACI SAJMA");
            Console.WriteLine();

            Console.WriteLine(
                $"{"Sifra", -12}{"Naziv izdavaca", -35}");

            Console.WriteLine(new string('-', 47));

            Console.ForegroundColor = prethodnaBoja;

            foreach (Izdavac izdavac in izdavaci)
            {
                Console.WriteLine(
                    $"{izdavac.Sifra, -12}{izdavac.Naziv, -35}");
            }

            Console.WriteLine();
            Console.WriteLine($"Ukupno izdavaca: {izdavaci.Count}");
        }

        private void PrikaziIzdavaca()
        {
            Console.Write("Unesi sifru izdavaca: ");

            if (!int.TryParse(Console.ReadLine(), out int sifra))
            {
                Console.WriteLine("Sifra mora biti broj!");
                return;
            }

            Izdavac izdavac = izdavacServis.DobaviIzdavaca(sifra);

            if (izdavac == null)
            {
                Console.WriteLine("Izdavac nije pronadjen!");
            }

            Console.WriteLine();

            ConsoleColor prethodnaBoja = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine("PODACI O IZDAVACU");
            Console.WriteLine(new string('-', 48));

            Console.ForegroundColor = prethodnaBoja;

            Console.WriteLine($"{"Sifra:",-20}{izdavac.Sifra}");
            Console.WriteLine($"{"Naziv:",-20}{izdavac.Naziv}");
        }

        private void DodajIzdavaca()
        {
            Console.WriteLine("Dodavanje izdavaca:");

            Izdavac izdavac = new Izdavac();

            Console.Write("Unesi sifru izdavaca: ");

            if(!int.TryParse(Console.ReadLine(), out int sifra))
            {
                Console.WriteLine("Sifra mora biti broj!");
                return;
            }
            izdavac.Sifra = sifra;

            Console.Write("Unesi naziv izdavaca: ");
            izdavac.Naziv = Console.ReadLine();

            if(izdavacServis.PostojiLiIzdavac(izdavac.Sifra))
            {
                Console.WriteLine("Izdavac sa tom sifrom vec postoji!");
            }
            else
            {
                izdavacServis.DodajIzdavaca(izdavac);
                Console.WriteLine("Izdavac je uspesno dodat!");
            }

           
        }

        private void IzmeniIzdavaca()
        {
            Console.Write("Unesi sifru izdavaca kojeg zelis da izmenis: ");

            if (!int.TryParse(Console.ReadLine(), out int sifra))
            {
                Console.WriteLine("Sifra mora biti broj!");
                return;
            }

            Izdavac izdavac = izdavacServis.DobaviIzdavaca(sifra);

            if(izdavac == null)
            {
                Console.WriteLine("Izdavac nije pronadjen");
                return;
            }

           
            Console.WriteLine("1 Izmena naziva izdavaca");
            Console.WriteLine("0 Nazad");
            Console.Write("Izaberite koje polje zelite da izmenite: ");

            string izmena = Console.ReadLine();

            switch(izmena)
            {
                case "1":
                    Console.Write("Unesite novi naziv izdavaca: ");
                    izdavac.Naziv = Console.ReadLine();
                    Console.WriteLine("Izdavac je uspesno izmenjen!");
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Pogresna opcija");
                    break;
            }

            izdavacServis.IzmeniIzdavaca(izdavac);
        }

        private void ObrisiIzdavaca()
        {
            Console.Write("Unesi sifru izdavaca kojeg zelis da obrises: ");

            if (!int.TryParse(Console.ReadLine(), out int sifra))
            {
                Console.WriteLine("Sifra mora biti broj!");
                return;
            }

            if(!izdavacServis.PostojiLiIzdavac(sifra))
            {
                Console.WriteLine("Izdavac nije pronadjen");
                return;
            }

            izdavacServis.ObrisiIzdavaca(sifra);
            Console.WriteLine("Izdavac je uspesno obrisan");

        }

        private void PostaviSefaIzdavaca()
        {
            PrikaziIzdavace();

            Console.Write("Unesi sifru izdavaca: ");
            if (!int.TryParse(Console.ReadLine(), out int sifra))
            {
                Console.WriteLine("Sifra mora biti broj!");
                return;
            }

            Izdavac izdavac = izdavacServis.DobaviIzdavaca(sifra);
            if (izdavac == null)
            {
                Console.WriteLine("Izdavac nije pronadjen!");
                return;
            }

            if (izdavac.SpisakAutora.Count == 0)
            {
                Console.WriteLine("Ovaj izdavac nema autora cije su knjige izdate kod njega.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"{"LK",-15}{"Ime",-20}{"Prezime",-20}{"Iskustvo",-10}");
            Console.WriteLine(new string('-', 65));

            foreach (Autor a in izdavac.SpisakAutora)
            {
                Console.WriteLine($"{a.LK,-15}{a.Ime,-20}{a.Prezime,-20}{a.GodineIskustva,-10}");
            }

            Console.Write("Unesi LK autora kojeg zelis da postavis za sefa: ");
            string lk = Console.ReadLine()?.Trim() ?? "";

            Autor izabrani = izdavac.SpisakAutora.FirstOrDefault(a => a.LK == lk);
            if (izabrani == null)
            {
                Console.WriteLine("Autor nije medju autorima ovog izdavaca.");
                return;
            }

            if (!izdavac.PostaviSefa(izabrani))
            {
                Console.WriteLine("Greska: sef izdavaca mora imati najmanje 5 godina iskustva!");
                return;
            }

            izdavacServis.IzmeniIzdavaca(izdavac);
            Console.WriteLine("Sef izdavaca je uspesno postavljen!");
        }

    }
}
