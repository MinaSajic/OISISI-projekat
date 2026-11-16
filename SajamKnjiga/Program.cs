using SajamKnjiga.Core.Models;
using SajamKnjiga.Core.Services;

namespace SajamKnjiga
{
    internal class Program
    {
        private static PosetilacService posetiociServis;
        private static KnjigaService knjigeServis;
        private static AutorService autoriServis;
        private static KupovinaService kupovineServis;

        static void Main(string[] args)
        {
            posetiociServis = new PosetilacService();
            knjigeServis = new KnjigaService();
            autoriServis = new AutorService();
            kupovineServis = new KupovinaService();

            bool izlazIzProgama = false;

            while (!izlazIzProgama)
            {
                Console.WriteLine();
                Console.WriteLine("Izaberi od ponudjenih: ");
                Console.WriteLine("1 Prikaz posetilaca");
                Console.WriteLine("2 Dodavanje posetilaca");
                Console.WriteLine("3 Brisanje posetioca");
                Console.WriteLine("4 Izmena posetioca");
                Console.WriteLine("5 Prikaz autora");
                Console.WriteLine("6 Dodavanje autora");
                Console.WriteLine("7 Izmena autora");
                Console.WriteLine("8 Brisanje autora");
                Console.WriteLine("9 Prikaz knjiga");
                Console.WriteLine("10 Dodavanje knjige");
                Console.WriteLine("11 Izmena knjige");
                Console.WriteLine("12 Brisanje knjige");
                Console.WriteLine("13 Prikaz kupovina");
                Console.WriteLine("14 Dodavanje kupovine");
                Console.WriteLine("15 Brisanje kupovine");
                Console.WriteLine("16 Izmena kupovine");
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
                    case "3":
                        BrisanjePosetioca();
                        break;
                    case "4":
                        IzmeniPosetioca();
                        break;
                    case "5":
                        PrikaziAutore();
                        break;
                    case "6":
                        DodajAutora();
                        break;
                    case "7":
                        IzmeniAutora();
                        break;
                    case "8":
                        ObrisiAutora();
                        break;
                    case "9":
                        PrikaziKnjige();
                        break;
                    case "10":
                        DodavanjeKnjige();
                        break;
                    case "11":
                        IzmeniKnjigu();
                        break;
                    case "12":
                        ObrisiKnjigu();
                        break;
                    case "13":
                        PrikazKupovina();
                        break;
                    case "14":
                        DodavanjeKupovine();
                        break;
                    case "15":
                        BrisanjeKupovine();
                        break;
                    case "16":
                        IzmeniKupovinu();
                        break;
                    case "0":
                        izlazIzProgama = true;  // tek nakon izlaska iz programa (izborom 0 u terminalu) mi se ukloni posetilac u datoteci Posetioci.txt, ne pre
                        break;
                    default:
                        Console.WriteLine("Pogresna opcija");
                        break;
                }
            }

        }

        private static void PrikaziPosetioce()
        {

            Console.WriteLine("Prikaz posetioca:");
            List<Posetilac> posetioci = posetiociServis.DobaviSvePosetioce();
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

        private static void PrikaziAutore()
        {
            Console.WriteLine("Prikaz autora:");

            List<Autor> autori = autoriServis.DobaviSveAutore();

            foreach (Autor autor in autori)
            {
                Console.WriteLine(autor.Ime);
                Console.WriteLine(autor.Prezime);
                Console.WriteLine(autor.DatumRodjenja);
                Console.WriteLine(autor.Adresa.Ulica);
                Console.WriteLine(autor.Adresa.Broj);
                Console.WriteLine(autor.Adresa.Grad);
                Console.WriteLine(autor.Adresa.Drzava);
                Console.WriteLine(autor.Telefon);
                Console.WriteLine(autor.E_mail);
                Console.WriteLine(autor.LK);
                Console.WriteLine(autor.GodineIskustva);
                Console.WriteLine();
            }
        }

        private static void DodajAutora()
        {
            Console.WriteLine("Dodavanje autora:");

            Autor autor = new Autor();

            Console.WriteLine("Unesi ime:");
            autor.Ime = Console.ReadLine();

            Console.WriteLine("Unesi prezime:");
            autor.Prezime = Console.ReadLine();

            Console.WriteLine("Unesi datum rodjenja (yyyy-MM-dd):");
            autor.DatumRodjenja = DateOnly.Parse(Console.ReadLine());

            autor.Adresa = new Adresa();

            Console.WriteLine("Unesi ulicu:");
            autor.Adresa.Ulica = Console.ReadLine();

            Console.WriteLine("Unesi broj:");
            autor.Adresa.Broj = Console.ReadLine();

            Console.WriteLine("Unesi grad:");
            autor.Adresa.Grad = Console.ReadLine();

            Console.WriteLine("Unesi drzavu:");
            autor.Adresa.Drzava = Console.ReadLine();

            Console.WriteLine("Unesi telefon:");
            autor.Telefon = Console.ReadLine();

            Console.WriteLine("Unesi email:");
            autor.E_mail = Console.ReadLine();

            Console.WriteLine("Unesi broj licne karte:");
            autor.LK = Console.ReadLine();

            Console.WriteLine("Unesi godine iskustva:");
            autor.GodineIskustva = int.Parse(Console.ReadLine());

            if (autoriServis.PostojiLiAutor(autor.LK))
            {
                Console.WriteLine("Autor sa tim brojem licne karte vec postoji.");
            }
            else
            {
                autoriServis.DodajAutora(autor);
                Console.WriteLine("Autor je uspesno dodat!");
            }
        }

        private static void IzmeniAutora()
        {
            Console.WriteLine("Unesi broj licne karte autora kojeg zelis da izmenis:");
            string lk = Console.ReadLine();

            Autor autor = autoriServis.DobaviAutora(lk);

            if (autor == null)
            {
                Console.WriteLine("Autor nije pronadjen");
                return;
            }

            Console.WriteLine("Izaberite koje polje zelite da izmenite:");
            Console.WriteLine("1 Izmena imena:");
            Console.WriteLine("2 Izmena prezimena:");
            Console.WriteLine("3 Izmena datuma rodjenja:");
            Console.WriteLine("4 Izmena ulice:");
            Console.WriteLine("5 Izmena broja:");
            Console.WriteLine("6 Izmena grada");
            Console.WriteLine("7 Izmena drzave:");
            Console.WriteLine("8 Izmena telefona");
            Console.WriteLine("9 Izmena email-a");
            Console.WriteLine("10 Izmena godina iskustva");
            Console.WriteLine("0 Nazad");
            string izmena = Console.ReadLine();

            switch (izmena)
            {
                case "1":
                    Console.WriteLine("Unesi novo ime autora:");
                    autor.Ime = Console.ReadLine();
                    Console.WriteLine("Autor je uspesno izmenjen!");
                    break;
                case "2":
                    Console.Write("Unesi novo prezime autora:");
                    autor.Prezime = Console.ReadLine();
                    Console.WriteLine("Autor je uspesno izmenjen!");
                    break;
                case "3":
                    Console.WriteLine("Unesi novi datum rodjenja (yyyy-MM-dd):");
                    autor.DatumRodjenja = DateOnly.Parse(Console.ReadLine());
                    Console.WriteLine("Autor je uspesno izmenjen!");
                    break;
                case "4":
                    Console.WriteLine("Unesi novu ulicu:");
                    autor.Adresa.Ulica = Console.ReadLine();
                    Console.WriteLine("Autor je uspesno izmenjen!");
                    break;
                case "5":
                    Console.WriteLine("Unesi novi broj:");
                    autor.Adresa.Broj = Console.ReadLine();
                    Console.WriteLine("Autor je uspesno izmenjen!");
                    break;
                case "6":
                    Console.WriteLine("Unesi novi grad:");
                    autor.Adresa.Grad = Console.ReadLine();
                    Console.WriteLine("Autor je uspesno izmenjen!");
                    break;
                case "7":
                    Console.WriteLine("Unesi novu drzavu:");
                    autor.Adresa.Drzava = Console.ReadLine();
                    Console.WriteLine("Autor je uspesno izmenjen!");
                    break;
                case "8":
                    Console.WriteLine("Unesi novi telefon:");
                    autor.Telefon = Console.ReadLine();
                    Console.WriteLine("Autor je uspesno izmenjen!");
                    break;
                case "9":
                    Console.WriteLine("Unesi novi email:");
                    autor.E_mail = Console.ReadLine();
                    Console.WriteLine("Autor je uspesno izmenjen!");
                    break;
                case "10":
                    Console.WriteLine("Unesi nove godine iskustva:");
                    autor.GodineIskustva = int.Parse(Console.ReadLine());
                    Console.WriteLine("Autor je uspesno izmenjen!");
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Pogresna opcija");
                    break;
            }

            autoriServis.IzmeniAutora(autor);
        }

        private static void ObrisiAutora()
        {
            Console.WriteLine("Unesi broj licne karte autora kojeg zelis da obrises:");
            string lk = Console.ReadLine();

            if (!autoriServis.PostojiLiAutor(lk))
            {
                Console.WriteLine("Autor nije pronadjen");
                return;
            }

            autoriServis.ObrisiAutora(lk);
            Console.WriteLine("Autor je uspesno obrisan!");
        }
        private static void PrikaziKnjige()
        {

            Console.WriteLine("Prikaz knjiga:");
            List<Knjiga> knjige = knjigeServis.DobaviSveKnjige();
            foreach (Knjiga knjiga in knjige)
            {
                Console.WriteLine(knjiga);
            }
        }

        private static void DodavanjeKnjige()
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

        private static void IzmeniKnjigu()
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

        private static void ObrisiKnjigu()
        {
            Console.WriteLine("Unesi ISBN knjige koju zelis da obrises:");
            string isbn = Console.ReadLine();

            //TODO: dodati kao i kod Posetilac "PostojiLiPosetilac" neku gresku ako treba

            knjigeServis.IzbrisiKnjigu(isbn);
        }

        private static void BrisanjePosetioca()
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

        private static void IzmeniPosetioca()
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

        private static void PrikazKupovina()
        {
            Console.WriteLine("Prikaz kupovina:");
            List<Kupovina> kupovine = kupovineServis.DobaviSveKupovine();
            foreach (Kupovina kupovina in kupovine)
            {
                Console.WriteLine(kupovina);
            }
        }

        private static void DodavanjeKupovine()
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

        private static void BrisanjeKupovine()
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

        private static void IzmeniKupovinu()
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



        private static void PretragaPosetilaca()
        {

        }
    }
}