using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SajamKnjiga.Core.Models;
using SajamKnjiga.Core.Services;

namespace SajamKnjiga.Controllers
{
    internal class AdresaController
    {
        private AdresaService adresaServis;

        public AdresaController(AdresaService adresaServis)
        {
            this.adresaServis = adresaServis;
        }

        public void Meni()
        {

            bool izlazIzPrograma = false;

            while(!izlazIzPrograma)
            {
                Console.WriteLine();
                Console.WriteLine("Izaberi od ponudjenih:");
                Console.WriteLine("1 Prikaz adrese");
                Console.WriteLine("2 Prikaz podataka o adresi");
                Console.WriteLine("3 Dodavanje adrese");
                Console.WriteLine("4 Izmena adrese");
                Console.WriteLine("5 Brisanje adrese");
                Console.WriteLine("0 Nazad");

                string odabranaOpcija = Console.ReadLine();

                switch(odabranaOpcija)
                {
                    case "1":
                        PrikaziAdrese();
                        break;
                    case "2":
                        //TODO
                        //detaljan prikaz adrese
                        break;
                    case "3":
                        DodajAdresu();
                        break;
                    case "4":
                        IzmeniAdresu();
                        break;
                    case "5":
                        ObrisiAdresu();
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

        //TODO
        //Mia
        private void PrikaziAdrese()
        {
            Console.WriteLine("Prikaz adrese:");

            List<Adresa> adrese = adresaServis.DobaviSveAdrese();

            foreach (Adresa adresa in adrese)
            {
                Console.WriteLine(adresa.Id);
                Console.WriteLine(adresa.Ulica);
                Console.WriteLine(adresa.Broj);
                Console.WriteLine(adresa.Grad);
                Console.WriteLine(adresa.Drzava);
                Console.WriteLine();
            }
        }

        private void DodajAdresu()
        {
            Console.WriteLine("Dodavanje adrese:");

            Adresa adresa = new Adresa();

            Console.WriteLine("Unesi ID adrese:");
            if(!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID mora biti broj!");
                return;
            }

            adresa.Id = id;

            Console.WriteLine("Unesi ulicu:");
            adresa.Ulica = Console.ReadLine();

            Console.WriteLine("Unesi broj:");
            adresa.Broj = Console.ReadLine();

            Console.WriteLine("Unesi grad:");
            adresa.Grad = Console.ReadLine();

            Console.WriteLine("Unesi drzavu:");
            adresa.Drzava = Console.ReadLine();

            if(adresaServis.PostojiLiAdresa(adresa.Id))
            {
                Console.WriteLine("Adresa sa tim ID-jem vec postoji.");
            }
            else
            {
                adresaServis.DodajAdresu(adresa);
                Console.WriteLine("Adresa je uspesno dodata!");
            }
        }

        private void IzmeniAdresu()
        {
            Console.WriteLine("Unesi ID adrese koju zelis da promenis:");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID mora biti broj!");
                return;
            }

            Adresa adresa = adresaServis.DobaviAdresu(id);

            if (adresa == null)
            {
                Console.WriteLine("Adresa nije pronadjena");
                return;
            }

            Console.WriteLine("Izaberite koje polje zelite da izmenite:");
            Console.WriteLine("1 Izmena ulice:");
            Console.WriteLine("2 Izmena broja:");
            Console.WriteLine("3 Izmena grada:");
            Console.WriteLine("4 Izmena drzave:");
            Console.WriteLine("0 Nazad:");
   
            string izmena = Console.ReadLine();

            switch (izmena)
            {
                case "1":
                    Console.WriteLine("Unesi novo ime ulice:");
                    adresa.Ulica = Console.ReadLine();
                    Console.WriteLine("Adresa je uspesno izmenjena!");
                    break;
                case "2":
                    Console.Write("Unesi novi broj ulice:");
                    adresa.Broj = Console.ReadLine();
                    Console.WriteLine("Adresa je uspesno izmenjena!");
                    break;
                case "3":
                    Console.WriteLine("Unesi novo ime grada");
                    adresa.Grad = Console.ReadLine();
                    Console.WriteLine("Adresa je uspesno izmenjena!");
                    break;
                case "4":
                    Console.WriteLine("Unesi novo ime drzave:");
                    adresa.Drzava = Console.ReadLine();
                    Console.WriteLine("Adresa je uspesno izmenjena!");
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Pogresna opcija");
                    break;
            }

            adresaServis.IzmeniAdresu(adresa);
        }

        private void ObrisiAdresu()
        {
            Console.WriteLine("Unesi ID adrese koju zelis da obrises:");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID mora biti broj!");
                return;
            }

            if (!adresaServis.PostojiLiAdresa(id))
            {
                Console.WriteLine("Adresa nije pronadjena");
                return;
            }

            adresaServis.ObrisiAdresu(id);
            Console.WriteLine("Adresa je uspesno obrisana!");
        }

    }
}
