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


        public KupovinaController(KupovinaService kupovineServis, PosetilacService posetiociServis, KnjigaService knjigeServis)
        {
            this.kupovineServis = kupovineServis;
            this.posetiociServis = posetiociServis;
            this.knjigeServis = knjigeServis;
        }

        public void Meni()
        {
            bool izlazIzProgama = false;



            while (!izlazIzProgama)
            {
                Console.WriteLine();
                Console.WriteLine("Izaberi od ponudjenih");
                Console.WriteLine("1 Prikaz kupovina");
                Console.WriteLine("2 Dodavanje kupovine");
                Console.WriteLine("3 Izmena kupovine");
                Console.WriteLine("4 Brisanje kupovine");
                Console.WriteLine("0 Nazad");

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
                    // TODO
                    // Napraviti case za nazad
                    default:
                        Console.WriteLine("Pogresna opcija");
                        break;
                }
            }

        }

        // ovde 

        private void PrikazKupovina()
        {
            Console.WriteLine("Prikaz kupovina:");
            List<Kupovina> kupovine = kupovineServis.DobaviSveKupovine();
            foreach (Kupovina kupovina in kupovine)
            {
                Console.WriteLine(kupovina);
            }
        }

        private void DodavanjeKupovine()
        {
            Console.WriteLine("Dodavanje kupovine");
            Kupovina kupovina = new Kupovina();

            Console.WriteLine("Unesi broj clanske karte: ");
            Posetilac posetilac = posetiociServis.DobaviPosetioca(Console.ReadLine());

            if (posetilac == null)

            {
                Console.WriteLine("Posetilac ne postoji");
                return;
            }

            Console.WriteLine("Unesi ISBN knjige: ");
            Knjiga knjiga = knjigeServis.DobaviKnjigu(Console.ReadLine());

            if (knjiga == null)

            {
                Console.WriteLine("Knjiga ne postoji");
                return;
            }

            kupovina.Posetilac = posetilac;
            kupovina.Knjiga = knjiga;

            Console.WriteLine("Unesi datum: ");
            kupovina.DatumKupovine = DateOnly.Parse(Console.ReadLine());

            Console.WriteLine("Unesi ocenu: ");
            kupovina.Ocena = int.Parse(Console.ReadLine());

            Console.WriteLine("Unesi komentar: ");
            kupovina.Komentar = Console.ReadLine();

            if (kupovineServis.DodavanjeKupovine(kupovina))
            {
                Console.WriteLine("Kupovina je dodata");
            }
            else
            {
                Console.WriteLine("Kupovina vec postoji");
            }

        }

        private void BrisanjeKupovine()
        {
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
            Console.WriteLine("Unesi broj clanske karte: ");
            string brojKarte = Console.ReadLine();

            Console.WriteLine("Unesi ISBN knjige: ");
            string isbn = Console.ReadLine();

            Kupovina original = kupovineServis.DobaviKupovinu(brojKarte, isbn);

            if (original == null)
            {
                Console.WriteLine("Kupovina ne postoji");
                return;
            }

            Kupovina kupovina = original.Kopija();

            Console.WriteLine("Menjanje podatka za: ");
            Console.WriteLine("1 Datum kupovine");
            Console.WriteLine("2 Ocenu");
            Console.WriteLine("3 Komentar");

            string odabranaOpcija = Console.ReadLine();

            Console.WriteLine("Unesi novu vrednost: ");
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

    }
}
