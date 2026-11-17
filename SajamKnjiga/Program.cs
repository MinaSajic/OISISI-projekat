using SajamKnjiga.Controllers;
using SajamKnjiga.Core.Models;
using SajamKnjiga.Core.Services;
using SajamKnjiga.Core.DAO;

namespace SajamKnjiga
{
    internal class Program
    {
        static void Main()
        {

            PovezivanjeEntiteta povezivanje = new PovezivanjeEntiteta();

            PosetilacService posetiociServis = new PosetilacService();
            KnjigaService knjigeServis = povezivanje.KnjigeServis;
            KupovinaService kupovineServis = new KupovinaService();
            AutorService autoriServis = povezivanje.AutoriServis;
            AdresaService adreseServis = new AdresaService();
            IzdavacService izdavaciServis = new IzdavacService();

            PosetilacController posetilacController = new PosetilacController(posetiociServis);

            KupovinaController kupovinaController = new KupovinaController(kupovineServis, posetiociServis, knjigeServis);

            KnjigaController knjigaController = new KnjigaController(knjigeServis, povezivanje);

            AutorController autorController = new AutorController(autoriServis);

            AdresaController adresaController = new AdresaController(adreseServis);

            IzdavacController izdavacController = new IzdavacController(izdavaciServis);

            bool izlaziIzProgram = false;

            while (!izlaziIzProgram)
            {
                Console.WriteLine("Izaberi opciju");
                Console.WriteLine("1 Posetioci");
                Console.WriteLine("2 Kupovine");
                Console.WriteLine("3 Knjige");
                Console.WriteLine("4 Autor");
                Console.WriteLine("5 Adresa");
                Console.WriteLine("6 Izdavac");
                Console.WriteLine("0 Izlazak iz programa");

                string odabranaOpcija = Console.ReadLine();
                switch (odabranaOpcija)
                {
                    case "1":
                        posetilacController.Meni();
                        break;
                    case "2":
                        kupovinaController.Meni();
                        break;
                    case "3":
                        knjigaController.Meni();
                        break;
                    case "4":
                        autorController.Meni();
                        break;
                    case "5":
                        adresaController.Meni();
                        break;
                    case "6":
                        izdavacController.Meni();
                        break;
                    case "0":
                        izlaziIzProgram = true;
                        break;
                    default:
                        Console.WriteLine("Pogresna opcija");
                        break;
                }


            }
        }
    }
}