using SajamKnjiga.Core.DAO;
using SajamKnjiga.Core.Models;
using SajamKnjiga.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SajamKnjiga.Controllers
{
    internal class KupovinaController
    {
        private KupovinaService kupovineServis;

        private PosetilacService posetiociServis;

        private KnjigaService knjigeServis;

        private PovezivanjeEntiteta povezivanje;


        public KupovinaController(PovezivanjeEntiteta povezivanje)
        {
            this.povezivanje = povezivanje;
            kupovineServis = povezivanje.KupovineServis;
            posetiociServis = povezivanje.PosetiociServis;
            knjigeServis = povezivanje.KnjigeServis;
        }

        public void Meni()
        {
            bool izlazIzProgama = false;



            while (!izlazIzProgama)
            {
                Console.WriteLine();
                Console.WriteLine("1 Prikaz kupovina");
                Console.WriteLine("2 Dodavanje kupovine");
                Console.WriteLine("3 Izmena kupovine");
                Console.WriteLine("4 Brisanje kupovine");
                Console.WriteLine("5 Ponisti kupovinu");
                Console.WriteLine("0 Nazad");
                Console.WriteLine();
                Console.Write("Izaberi od ponudjenih: ");
                

                string odabranaOpcija = Console.ReadLine();


                switch (odabranaOpcija)
                {
                    case "1":
                        PrikazKupovina();
                        break;
                    case "2":
                        DodavanjeKupovine();
                        break;
                    case "3":
                        IzmeniKupovinu();
                        break;
                    case "4":
                        BrisanjeKupovine();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Pogresna opcija");
                        break;
                }
            }

        }

        // ovde 

        private void PrikazKupovina()
        {
            List<Kupovina> kupovine = kupovineServis.DobaviSveKupovine();

            TabelaKupovine();

            foreach (Kupovina kupovina in kupovine)
            {
                string brojKarte = kupovina.Posetilac?.BrojClanskeKarte ?? "";
                string isbn = kupovina.Knjiga?.ISBN ?? "";

                Posetilac? posetilac = posetiociServis.DobaviPosetioca(brojKarte);
                Knjiga? knjiga = knjigeServis.DobaviKnjigu(isbn);

                string punoIme = posetilac == null
                    ? "(nije pronadjen)"
                    : $"{posetilac.Ime} {posetilac.Prezime}";

                string naziv = knjiga?.Naziv ?? "(nije pronadjena)";

                Console.WriteLine(
                    $"{brojKarte,-12}{punoIme,-25}{isbn,-20}{naziv,-25}" +
                    $"{kupovina.DatumKupovine,-14:dd.MM.yyyy.}{kupovina.Ocena,6}  {kupovina.Komentar}");
            }

            Console.WriteLine();
            Console.WriteLine($"Ukupno kupovina: {kupovine.Count}");

        }
        private void DodavanjeKupovine()
        {
            Console.WriteLine("Dodavanje kupovine");
            Kupovina kupovina = new Kupovina();

            PrikazPosetioca();

            Console.Write("Unesi broj clanske karte: ");
            Posetilac posetilac = posetiociServis.DobaviPosetioca(Console.ReadLine());

            if (posetilac == null)

            {
                Console.WriteLine("Posetilac ne postoji");
                return;
            }

            PrikazKnjiga();

            Console.Write("Unesi ISBN knjige: ");
            Knjiga knjiga = knjigeServis.DobaviKnjigu(Console.ReadLine());

            if (knjiga == null)
            {
                Console.WriteLine("Knjiga ne postoji");
                return;
            }

            if(kupovineServis.PostojiLiKupovina(posetilac.BrojClanskeKarte, knjiga.ISBN))
            {
                Console.WriteLine("Kupovina vec postoji!");
                return;
            }

            kupovina.Posetilac = posetilac;
            kupovina.Knjiga = knjiga;

            Console.Write("Unesi datum kupovine (dd.MM.yyyy.): ");
            kupovina.DatumKupovine = DateOnly.Parse(Console.ReadLine());

            Console.Write("Unesi ocenu (1-5): ");
            string unosOcene = Console.ReadLine();

            if(!int.TryParse(unosOcene, out int ocena) || ocena < 1 || ocena > 5)
            {
                Console.WriteLine("Ocena mora biti broj od 1 do 5");
                return;
            } 
            kupovina.Ocena = ocena;

            Console.Write("Unesi komentar: ");
            kupovina.Komentar = Console.ReadLine();

            if (kupovineServis.DodavanjeKupovine(kupovina))
            {
                Console.WriteLine("Kupovina je dodata");
                povezivanje.poveziPosetioceIKnjige();
            }
            else
            {
                Console.WriteLine("Kupovina vec postoji");
            }

        }

        private void BrisanjeKupovine()
        {

            PrikazKupovina();

            Console.WriteLine("Unesi broj clanske karte i isbn knjige: ");

            string BrClanskeKarte = Console.ReadLine();

            string isbn = Console.ReadLine();

            if (kupovineServis.BrisanjeKupovine(BrClanskeKarte, isbn))
            {
                Console.WriteLine("Kupovina je obrisana iz liste.");
            }
            else
            {
                Console.WriteLine("Kupovina sa datim brojem clanske karte i isbn-om ne postoji");
            }
        }

        private void IzmeniKupovinu()
        {
            PrikazKupovina();
            Console.Write("Unesi broj clanske karte: ");
            string brojKarte = Console.ReadLine();

            
            Console.Write("Unesi ISBN knjige: ");
            string isbn = Console.ReadLine();

            Kupovina original = kupovineServis.DobaviKupovinu(brojKarte, isbn);

            if (original == null)
            {
                Console.WriteLine("Kupovina ne postoji");
                return;
            }

            Kupovina kupovina = original.Kopija();

            
            Console.WriteLine("1 Datum kupovine");
            Console.WriteLine("2 Ocenu");
            Console.WriteLine("3 Komentar");
            Console.Write("Menjanje podatka za: ");

            string odabranaOpcija = Console.ReadLine();

            Console.Write("Unesi novu vrednost: ");
            string nova_vrednost = Console.ReadLine();

            switch (odabranaOpcija)
            {
                case "1":
                    if (!DateOnly.TryParseExact(nova_vrednost, new[] { "d.M.yyyy", "d.M.yyyy." },
                        null, System.Globalization.DateTimeStyles.None, out DateOnly datum))
                    {
                        Console.WriteLine("Neispravan datum.");
                        return;
                    }
                    kupovina.DatumKupovine = datum;
                    break;
                case "2":
                    if (!int.TryParse(nova_vrednost, out int ocena) || ocena < 1 || ocena > 5)
                    {
                        Console.WriteLine("Ocena mora biti broj od 1 do 5.");
                        return;
                    }
                    kupovina.Ocena = ocena;
                    break;
                case "3":
                    kupovina.Komentar = nova_vrednost;
                    break;
                default:
                    Console.WriteLine("Pogresna opcija");
                    return;
            }

            if (kupovineServis.IzmenaKupovine(kupovina))
            {
                Console.WriteLine("Izmena je sacuvana.");
            }
            else
            {
                Console.WriteLine("Izmena nije uspela");
            }
        }

        private void TabelaKupovine()
        {
            Console.WriteLine();

            ConsoleColor prethodnaBoja = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine("KUPOVINE SAJMA");
            Console.WriteLine();
            Console.WriteLine(
                $"{"Br. karte",-12}{"Posetilac",-25}{"ISBN",-20}{"Knjiga",-25}" +
                $"{"Datum",-14}{"Ocena",6}  Komentar");
            Console.WriteLine(new string('-', 112));

            Console.ForegroundColor = prethodnaBoja;
        }

        private void PrikazPosetioca()
        {
            ConsoleColor prethodna = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine();
            Console.WriteLine("POSETIOCI SAJMA");
            Console.WriteLine();

            Console.WriteLine($"{"Broj karte",-20}{"Ime",-20}{"Prezime",-20}");
            Console.WriteLine(new string('-', 60));

            List<Posetilac> sviPosetioci = posetiociServis.DobaviSvePosetioce();

            Console.ForegroundColor = prethodna;

            foreach (Posetilac p in sviPosetioci)
            {
                Console.WriteLine($"{p.BrojClanskeKarte,-20}{p.Ime,-20}{p.Prezime,-20}");
            }

            Console.WriteLine();

        }

       

        private void PrikazKnjiga()
        {

            Console.WriteLine();
            ConsoleColor prethodna = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("KNJIGE SAJMA");
            Console.WriteLine();
            Console.WriteLine($"{"ISBN",-18}Naziv");

            Console.WriteLine(new string('-', 25));
            Console.ForegroundColor = prethodna;


            foreach (Knjiga knjiga in povezivanje.KnjigeServis.DobaviSveKnjige())
            {
                Console.WriteLine($"{knjiga.ISBN,-18}{knjiga.Naziv}");
            }

            Console.WriteLine();
        }

    }
}
