using SajamKnjiga.Core.Models;
using SajamKnjiga.Core.Storage;
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
                        izlazIzProgama = true;
                        break;
                    default:
                        Console.WriteLine("Pogresna opcija");
                        break;
                }
            }

            SacuvajPosetioce(posetioci);
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
            posetilac.DatumRodjenja = Console.ReadLine();

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
    }
}
