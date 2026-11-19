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
    internal class PosetilacController
    {

        private  PosetilacService posetiociServis;
        private PovezivanjeEntiteta povezivanje;

        public PosetilacController(PosetilacService posetiociServis,PovezivanjeEntiteta povezivanje)
        {
            this.posetiociServis = posetiociServis;
            this.povezivanje = povezivanje;
        }


        public void Meni()
        {
            bool izlazIzProgama = false;

           

            while (!izlazIzProgama)
            {
                Console.WriteLine();
                Console.WriteLine("Izaberi od ponudjenih");
                Console.WriteLine("1 Prikaz svih posetilaca");
                Console.WriteLine("2 Prikazi podatke o posetiocu");
                Console.WriteLine("3 Dodavanje posetilaca");
                Console.WriteLine("4 Brisanje posetioca");
                Console.WriteLine("5 Izmena posetioca");
                Console.WriteLine("6 Prikaz kupljenih knjiga");
                Console.WriteLine("7 Prikaz liste zelja");
                Console.WriteLine("8 Dodavanje knjiga na listu zelja");
                Console.WriteLine("9 Uklanjanje knjiga sa liste zelja");
                Console.WriteLine("10 Prikaz autora sa liste zelja posetioca");
                Console.WriteLine("11 Sortiranje posetilaca");
                Console.WriteLine("0. Nazad");
                Console.WriteLine();
                Console.Write("Izaberi od ponudjenih: ");

                string odabranaOpcija = Console.ReadLine();


                switch (odabranaOpcija)
                {
                    case "1":
                        PrikaziPosetioce();
                        break;
                    case "2":
                        DetaljiOPosetiocu();
                        break;
                    case "3":
                        DodajPosetioca();
                        break;
                    case "4":
                        BrisanjePosetioca();
                        break;
                    case "5":
                        IzmeniPosetioca();
                        break;
                    case "6":
                        PrikazKupljenihKnjiga();
                        break;
                    case "7":
                        prikazListeZelja();
                        break;
                    case "8":
                        DodajKnjigeNaListuZelja();
                        break;
                    case "9":
                        UkloniKnjiguSaListeZelja();
                        break;
                    case "10":
                        PrikazAutoraPosetioca();
                        break;
                    case "11":
                        SortiranjePosetilaca();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Pogresna opcija");
                        break;
                }
            }
        }

        private void PrikaziPosetioce()
        {
            List<Posetilac> posetioci = posetiociServis.DobaviSvePosetioce();

            Console.WriteLine();

            ConsoleColor prethodnaBoja = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine("POSETIOCI SAJMA");
            Console.WriteLine();
            Console.WriteLine(
                $"{"Članska karta",-16}{"Ime i prezime",-25}{"Grad",-16}{"Status",-10}{"Ocena",6}");
            Console.WriteLine(new string('─', 73));

            Console.ForegroundColor = prethodnaBoja;

            foreach (Posetilac posetilac in posetioci)
            {
                string punoIme = $"{posetilac.Ime} {posetilac.Prezime}";
                string grad = posetilac.Adresa?.Grad ?? "—";
                string status = posetilac.Status == StatusPosetioca.V ? "VIP" : "Redovan";

                Console.WriteLine(
                    $"{posetilac.BrojClanskeKarte,-16}{punoIme,-25}{grad,-16}" +
                    $"{status,-10}{posetilac.ProsecnaOcenaRecenzija,6:F2}");
            }

            Console.WriteLine();
            Console.WriteLine($"Ukupno posetilaca: {posetioci.Count}");
        }

        private void DetaljiOPosetiocu()
        {
            Console.WriteLine("Unesi broj clanske karte: ");
            string BrojClanskeKarte = Console.ReadLine()?.Trim() ?? "";

           

            if (!posetiociServis.PostojiLiPosetilac(BrojClanskeKarte))
            {
                Console.WriteLine("Posetilac sa tim brojem clanske karte ne postoji.");
                return;
            }
            Posetilac posetilac = posetiociServis.DobaviPosetioca(BrojClanskeKarte);

            string status = posetilac.Status == StatusPosetioca.V ? "VIP" : "Redovan";
            ConsoleColor prethodnaBoja = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(new string('-', 30));

            Console.ForegroundColor= prethodnaBoja;

            Console.WriteLine($"{posetilac.Ime} {posetilac.Prezime}");

            Console.ForegroundColor= ConsoleColor.Magenta ;
            Console.WriteLine(new string('-', 30));

            Console.ForegroundColor = prethodnaBoja;

            Console.WriteLine($"Clanska karta:  {posetilac.BrojClanskeKarte} ");
            Console.WriteLine($"Datum Rodjenja:  {posetilac.DatumRodjenja} ");
            Console.WriteLine($"Adresa: {posetilac.Adresa.Ulica} {posetilac.Adresa.Broj}," +
                              $"{posetilac.Adresa.Grad} {posetilac.Adresa.Drzava}");
            Console.WriteLine($"Kontakt Telefon:  {posetilac.KontaktTelefon} ");
            Console.WriteLine($"Email:  {posetilac.Email} ");
            Console.WriteLine($"Trenutna godina clanstva:  {posetilac.TrenutnaGodinaClanstva} ");
            Console.WriteLine($"Status:  {status} ");
            Console.WriteLine($"Prosecna ocena recenzija:  {posetilac.ProsecnaOcenaRecenzija} ");
        }
       

        private void DodajPosetioca()
        {
            Console.WriteLine("Dodavanje posetioca");
            Posetilac posetilac = new Posetilac();

            Console.WriteLine("Unesi ime: ");
            posetilac.Ime = Console.ReadLine();

            Console.WriteLine("Unesi prezime: ");
            posetilac.Prezime = Console.ReadLine();

            Console.WriteLine("Unesi datum rodjenja: ");
            if (!DateOnly.TryParseExact(Console.ReadLine(), new[] { "d.M.yyyy", "d.M.yyyy." }, null, System.Globalization.DateTimeStyles.None, out DateOnly datumRodjenja))
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

            if (posetiociServis.DodajPosetioca(posetilac))
            {
                Console.WriteLine("Posetilac je dodat");
            }
            else
            {
                Console.WriteLine("Posetilac sa datim brojem clanske karte vec postoji");
            }


        }

        

        private void BrisanjePosetioca()
        {
            Console.WriteLine("Unesi broj clanske karte posetioca: ");

            string BrClanskeKarte = Console.ReadLine();

            if (posetiociServis.IzbrisiPosetioca(BrClanskeKarte))
            {
                Console.WriteLine("Posetilac je obrisan iz liste.");
            }
            else
            {
                Console.WriteLine("Posetilac sa datim brojem clanske karte ne postoji");
            }
        }

        private void IzmeniPosetioca()
        {
            Console.WriteLine("Unesi broj clanske karte: ");
            string BrK = Console.ReadLine();

            if (!posetiociServis.PostojiLiPosetilac(BrK))
            {
                Console.WriteLine("Posetilac sa datim brojem clanske karte ne postoji");
                return;
            }

            Posetilac posetilac = posetiociServis.DobaviPosetioca(BrK).Kopija();

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
            // Console.WriteLine("10 Broj clanske karte");
            Console.WriteLine("10 Trenutnu godinu clanstva");
            Console.WriteLine("11 Status");
            Console.WriteLine("12 Prosecna ocena recenzija");

            string odabranaOpcija = Console.ReadLine();

            Console.WriteLine("Unesi novu vrednost: ");

            string nova_vrednost = Console.ReadLine();

            switch (odabranaOpcija)
            {
                case "1":
                    posetilac.Ime = nova_vrednost;  // menjamo kopiju Posetilac, lista je netaknuta
                    break;
                case "2":
                    posetilac.Prezime = nova_vrednost;
                    break;
                case "3":
                    posetilac.DatumRodjenja = DateOnly.ParseExact(nova_vrednost, new[] { "d.M.yyyy", "d.M.yyyy." }, null, System.Globalization.DateTimeStyles.None);
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
                // case "10":
                // posetilac.BrojClanskeKarte = nova_vrednost;
                // break;
                case "10":
                    posetilac.TrenutnaGodinaClanstva = nova_vrednost;               // TODO
                    break;                                                          // Da li je prosecna ocena recenzija podatak posetioca ili nesto drugo? U cemu se razlikuje od ocene?
                case "11":
                    posetilac.Status = Enum.Parse<StatusPosetioca>(nova_vrednost);  // TODO
                    break;                                                          // Da li ovde trebam implementirati pitanje da li je korisnik uneo odgovarajuci format za status,prosecnuocenurecenzija?
                case "12":
                    posetilac.ProsecnaOcenaRecenzija = double.Parse(nova_vrednost);
                    break;
                default:
                    Console.WriteLine("Pogresna opcija");
                    return;

                    // Pitanje: da li treba ovako da implementiram f-ju za izmenu posetioca za neki njegov podatak ili projekat trazi da izmenim sve podatke tog posetioca?
                    // TODO
            }

            //SacuvajPosetioce(posetioci);
            if (posetiociServis.IzmeniPosetioca(posetilac))
            {
                Console.WriteLine("Izmena je sacuvana.");
            }
            else
            {
                Console.WriteLine("Izmena nije uspela");
            }


        }


        private void PrikazKupljenihKnjiga()
        {
            SkraceniPrikaz();

            Console.Write("Unesite broj clanske karte: ");

            
            string brojKarte = Console.ReadLine();

            Console.WriteLine();

            Posetilac posetilac = posetiociServis.DobaviPosetioca(brojKarte);

            
            if (posetilac == null)
            {
                Console.WriteLine("Posetilac nije pronadjen.");
                return;
            }

            if (posetilac.Kupovine.Count == 0)
            {
                Console.WriteLine("Posetilac nema kupljenih knjiga.");
                return;
            }

            Console.WriteLine($"Ime: {posetilac.Ime} {posetilac.Prezime}");

            Console.WriteLine();

            Console.WriteLine("Kupljene knjige:");


            foreach (Kupovina kupovina in posetilac.Kupovine)
            {
                Console.WriteLine($"- {kupovina.Knjiga.Naziv}");
            }
        }


        private void SkraceniPrikaz()
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

        private void DodajKnjigeNaListuZelja()
        {
            SkraceniPrikaz();

            Console.Write("Unesite broj clanske karte: ");


            string brojKarte = Console.ReadLine();

            Console.WriteLine();
            ConsoleColor prethodna = Console.ForegroundColor;
            Console.ForegroundColor= ConsoleColor.Magenta;
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

            Console.Write("Unesite ISBN-ove knjiga koje zelite dodati na svoju listu zelja: ");

            

            string isbn = Console.ReadLine();

            if (povezivanje.DodajKnjiguNaListuZelja(brojKarte, isbn))
            {
                Console.WriteLine("Dodato je na listu zelja.");
            }
            else
            {
                Console.WriteLine("Proveri broj karte i ISBN. Knjiga je mozda vec na listi.");
            }
        }

        private void UkloniKnjiguSaListeZelja()
        {
            SkraceniPrikaz();

            Console.WriteLine("Unesite broj clanske karte: ");
            string brojKarte = Console.ReadLine();

            Posetilac posetilac = posetiociServis.DobaviPosetioca(brojKarte);

            if(posetilac == null)
            {
                Console.WriteLine("Posetilac nije pronadjen");
                return;
            }

            if(posetilac.ListaZelja.Count == 0)
            {
                Console.WriteLine("Lista zelja je prazna.");
                return;
            }

            ConsoleColor prethodnaBoja = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine();
            Console.WriteLine("KNJIGE NA LISTI ZELJA");
            Console.WriteLine();

            Console.WriteLine($"{"ISBN", -18}Naziv");
            Console.WriteLine(new string('-', 48));

            Console.ForegroundColor = prethodnaBoja;

            foreach(Knjiga knjiga in posetilac.ListaZelja)
            {
                Console.WriteLine($"{knjiga.ISBN, -18}{knjiga.Naziv}");
            }

            Console.WriteLine();
            Console.WriteLine("Unesite ISBN knjige koju zelite ukloniti: ");
            string isbn = Console.ReadLine();

            if(povezivanje.UkloniKnjiguSaListeZelja(brojKarte, isbn))
            {
                Console.WriteLine("Knjiga je uspesno uklonjena sa liste zelja");
            }
            else
            {
                Console.WriteLine("Uklanjanje nije uspelo. Proverite ISBN.");
            }
        }


        private void prikazListeZelja()
        {
            SkraceniPrikaz();

            Console.Write("Unesite broj clanske karte: ");

         

            string brojKarte = Console.ReadLine()?.Trim() ?? "";

            Posetilac posetilac = posetiociServis.DobaviPosetioca(brojKarte);

            if (posetilac == null)
            {
                Console.WriteLine("Posetilac nije pronadjen.");
                return;
            }

            if (posetilac.ListaZelja.Count == 0)
            {
                Console.WriteLine("Lista zelja je prazna.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"{posetilac.Ime} {posetilac.Prezime}");
          
          

            foreach (Knjiga knjiga in posetilac.ListaZelja)
            {
                Console.WriteLine($" -{knjiga.Naziv}");
            }
        }

        private void PrikazAutoraPosetioca()
        {
            SkraceniPrikaz();

            Console.WriteLine("Unesite broj clanske karte: ");
            string brojKarte = Console.ReadLine();

            Posetilac posetilac = posetiociServis.DobaviPosetioca(brojKarte);

            if(posetilac == null)
            {
                Console.WriteLine("Posetilac nije pronadjen");
                return;
            }

            List<Autor> autori = new List<Autor>();

            foreach(Knjiga knjiga in posetilac.ListaZelja)
            {
                foreach(Autor autor in knjiga.Autori)
                {
                    if(!autori.Exists(a => a.LK == autor.LK))
                    {
                        autori.Add(autor);
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine($"Posetilac: {posetilac.Ime} {posetilac.Prezime}");

            ConsoleColor prethodnaBoja = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine();
            Console.WriteLine("AUTORI KNJIGA SA LISTE");
            Console.WriteLine();

            Console.WriteLine($"{"Ime", -20}{"Prezime", -20}Email");
            Console.WriteLine(new string('-', 65));

            Console.ForegroundColor = prethodnaBoja;

            if (autori.Count == 0)
            {
                Console.WriteLine("Nema autora za prikaz.");
                return;
            }

            foreach(Autor autor in autori)
            {
                Console.WriteLine(
                    $"{autor.Ime, -20}{autor.Prezime, -20}{autor.E_mail}");
            }

            Console.WriteLine();
            Console.WriteLine($"Ukupno autora: {autori.Count}");
        }

        private void SortiranjePosetilaca()
        {
            List<Posetilac> posetioci = posetiociServis.DobaviSvePosetioce();

            Console.WriteLine("Sortiranje posetilaca po: ");
            Console.WriteLine("1 Ime");
            Console.WriteLine("2 Prezime");
            Console.WriteLine("3 Datumu rodjenja");
            Console.WriteLine("4 Kontakt telefonu");
            Console.WriteLine("5 Email-u");
            Console.WriteLine("6 Broju clanske karte");
            Console.WriteLine("7 Trenutnoj godini clanstva");
            Console.WriteLine("8 Prosecnoj oceni recenzija");
            Console.Write("Izaberi opciju: ");

            string opcija = Console.ReadLine();

            Console.WriteLine("1 Rastuce");
            Console.WriteLine("2 Opadajuce");
            Console.Write("Izaberi redosled: ");

            string redosled = Console.ReadLine();

            if(redosled != "1" && redosled != "2")
            {
                Console.WriteLine("Pogresan redosled");
                return;
            }

            switch (opcija)
            {
                case "1":
                     posetioci = posetioci.OrderBy(p => p.Ime).ToList();
                    break;
                case "2":
                    posetioci = posetioci.OrderBy(p => p.Prezime).ToList();
                    break;
                case "3":
                    posetioci = posetioci.OrderBy(p => p.DatumRodjenja).ToList();
                    break;
                case "4":
                    posetioci = posetioci.OrderBy(p => p.KontaktTelefon).ToList();
                    break;
                case "5":
                    posetioci = posetioci.OrderBy(p => p.Email).ToList();
                    break;
                case "6":
                    posetioci = posetioci.OrderBy(p => p.BrojClanskeKarte).ToList();
                    break;
                case "7":
                    posetioci = posetioci.OrderBy(p => p.TrenutnaGodinaClanstva).ToList();
                    break;
                case "8":
                    posetioci = posetioci.OrderBy(p => p.ProsecnaOcenaRecenzija).ToList();
                    break;
                default:
                    Console.WriteLine("Pogresna opcija!");
                    return;
            }

            if(redosled == "2")
            {
                posetioci.Reverse();
            }

            ConsoleColor prethodna = Console.ForegroundColor;

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("SORTIRANI POSETIOCI — OSNOVNI PODACI");
            Console.WriteLine(
                $"{"Članska karta",-18}{"Ime",-18}{"Prezime",-20}{"Datum rođenja",-16}Adresa");
            Console.WriteLine(new string('─', 95));
            Console.ForegroundColor = prethodna;

            foreach (Posetilac p in posetioci)
            {
                string adresa = p.Adresa == null
                    ? "—"
                    : $"{p.Adresa.Ulica} {p.Adresa.Broj}, {p.Adresa.Grad}, {p.Adresa.Drzava}";

                Console.WriteLine(
                    $"{p.BrojClanskeKarte,-18}{p.Ime,-18}{p.Prezime,-20}" +
                    $"{p.DatumRodjenja,-16:dd.MM.yyyy}{adresa}");
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("SORTIRANI POSETIOCI — KONTAKT I ČLANSTVO");
            Console.WriteLine(
                $"{"Članska karta",-18}{"Telefon",-20}{"Email",-32}" +
                $"{"God. članstva",-16}{"Pros. ocena",12}");
            Console.WriteLine(new string('─', 98));
            Console.ForegroundColor = prethodna;

            foreach (Posetilac p in posetioci)
            {
                Console.WriteLine(
                    $"{p.BrojClanskeKarte,-18}{p.KontaktTelefon,-20}{p.Email,-32}" +
                    $"{p.TrenutnaGodinaClanstva,-16}{p.ProsecnaOcenaRecenzija,12:F2}");
            }
        }

    }
}

