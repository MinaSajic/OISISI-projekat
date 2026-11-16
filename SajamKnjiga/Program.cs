using SajamKnjiga.Controllers;
using SajamKnjiga.Core.Models;
using SajamKnjiga.Core.Services;

namespace SajamKnjiga
{
    internal class Program
    {
        static void Main()
        {

            PosetilacService posetiociServis = new PosetilacService();
            KnjigaService knjigeServis = new KnjigaService();
            KupovinaService kupovineServis = new KupovinaService();

            PosetilacController posetilacController = new PosetilacController(posetiociServis);

            KupovinaController kupovinaController = new KupovinaController(kupovineServis,posetiociServis,knjigeServis);

            KnjigaController knjigaController = new KnjigaController(knjigeServis);

            bool izlaziIzProgram = false;

            while (!izlaziIzProgram)
            {
                Console.WriteLine("Izaberi opciju");
                Console.WriteLine("1 Posetioci");
                Console.WriteLine("2 Kupovine");
                Console.WriteLine("3 Knjige");
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