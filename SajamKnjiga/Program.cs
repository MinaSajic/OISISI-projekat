using SajamKnjiga.Core.Models;
using SajamKnjiga.Core.Storage;
using System.ComponentModel.Design;
using System.Runtime.Versioning;
using System.Text;

namespace SajamKnjiga
{
    internal class Program
    {
        private static List<Posetilac> posetioci;

        static void Main(string[] args)
        {
            posetioci = UcitajPosetioce();
            bool izlazIzProgama = false;

            while (!izlazIzProgama)
            {
                Console.WriteLine();
                Console.WriteLine("Izaberi od ponudjenih: ");
                Console.WriteLine("1 Prikaz posetilaca");
                Console.WriteLine("2 Dodavanje posetilaca");
                Console.WriteLine("3 Brisanje posetioca");
                Console.WriteLine("4 Izmena posetioca");
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
                    case "0":
                        izlazIzProgama = true;  // tek nakon izlaska iz programa (izborom 0 u terminalu) mi se ukloni posetilac u datoteci Posetioci.txt, ne pre
                        break;
                    case "3":
                        BrisanjePosetioca();
                        break;
                    case "4":
                        IzmeniPosetioca();
                        break;
                    default:
                        Console.WriteLine("Pogresna opcija");
                        break;
                }
            }

            SacuvajPosetioce(posetioci);
        }

        private static void PrikaziPosetioce() { 
        
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
            if (!DateOnly.TryParseExact(Console.ReadLine(), new[] { "d.M.yyyy", "d.M.yyyy." }, out DateOnly datumRodjenja))
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

        private static void BrisanjePosetioca()
        {
            Console.WriteLine("Unesi broj licne karte posetioca: ");

            string BrK = Console.ReadLine();

            int position = -1;

            for(int i = 0; i < posetioci.Count; i++)
            {
                if (posetioci[i].BrojClanskeKarte == BrK)
                {
                    position = i;
                    break;
                }
            }

            if(position == -1)
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

            for(int i = 0; i < posetioci.Count; i++)
            { 
                if (posetioci[i].BrojClanskeKarte == BrK)
                {
                    position = i;
                    break;
                }
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
                    posetilac.DatumRodjenja = DateOnly.ParseExact(nova_vrednost, new[] { "d.M.yyyy", "d.M.yyyy."});
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
