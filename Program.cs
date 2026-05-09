using System;
using System.Collections.Generic;

namespace ToidutellimusteSusteem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string failiNimi = "tooted.txt";

            // Need tooted loetakse failist ja neid saab kasutada tellimuse koostamiseks
            List<Toode> saadavalTooted = FailiTöötlus.LaeTootedFailist(failiNimi);

            // Kasutajad hoiavad rolliga seotud infot ja kliendi tellimust
            Klient klient = new Klient("Darja");
            Admin admin = new Admin("Artjom");
            MakseTeenindus makseTeenindus = new MakseTeenindus();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n--- TOIDUTELLIMUSTE SÜSTEEM ---");
                Console.WriteLine("Vali roll:");
                Console.WriteLine($"1. {klient.Roll} - telli toitu ja maksa");
                Console.WriteLine($"2. {admin.Roll} - lisa ja vaata menüü tooteid");
                Console.WriteLine("0. Välju");
                Console.WriteLine($"\nMenüüs tooteid: {saadavalTooted.Count} | Ostukorvis: {klient.Tellimus.ToodeteArv}");

                Console.Write("Sisesta oma valik: ");

                if (!int.TryParse(Console.ReadLine(), out int valik))
                {
                    Console.WriteLine("Vigane sisend! Palun sisesta number.");
                    continue;
                }

                switch (valik)
                {
                    case 1:
                        AvaKliendiMenüü(klient, saadavalTooted, makseTeenindus);
                        break;

                    case 2:
                        AvaAdminiMenüü(admin, saadavalTooted, failiNimi);
                        break;

                    case 0:
                        Console.WriteLine("Aitäh kasutamise eest! Tulge tagasi mõni kord veel.");
                        return;

                    default:
                        Console.WriteLine("Tundmatu valik.");
                        Console.WriteLine("\nVajuta Enter, et jätkata...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void AvaKliendiMenüü(Klient klient, List<Toode> saadavalTooted, MakseTeenindus makseTeenindus)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n--- KLIENDI MENÜÜ ---");
                Console.WriteLine($"Ostukorvis: {klient.Tellimus.ToodeteArv} toodet");
                Console.WriteLine("1. Lisa toode ostukorvi");
                Console.WriteLine("2. Vaata saadaval tooteid");
                Console.WriteLine("3. Vaata ostukorvi");
                Console.WriteLine("4. Vormista ja maksa");
                Console.WriteLine("0. Tagasi peamenüüsse");
                Console.Write("Sisesta valik: ");

                if (!int.TryParse(Console.ReadLine(), out int valik))
                {
                    Console.WriteLine("Vigane sisend! Palun sisesta number.");
                    continue;
                }

                if (valik == 0)
                {
                    return;
                }

                try
                {
                    switch (valik)
                    {
                        case 1:
                            KoostaTellimus(saadavalTooted, klient.Tellimus);
                            break;

                        case 2:
                            KuvaSaadavalTooted(saadavalTooted);
                            break;

                        case 3:
                            KuvaTellimus(klient.Tellimus);
                            break;

                        case 4:
                            VormistaJaMaksaTellimus(klient, makseTeenindus);
                            break;

                        default:
                            Console.WriteLine("Tundmatu valik.");
                            break;
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Andmete sisestamine ebaõnnestus: {ex.Message}");
                }

                Console.WriteLine("\nVajuta Enter, et jätkata...");
                Console.ReadLine();
            }
        }

        static void AvaAdminiMenüü(Admin admin, List<Toode> saadavalTooted, string failiNimi)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n--- ADMINI MENÜÜ ---");
                Console.WriteLine($"Menüüs tooteid: {saadavalTooted.Count}");
                Console.WriteLine("1. Lisa uus toode menüüsse");
                Console.WriteLine("2. Vaata menüü tooteid");
                Console.WriteLine("0. Tagasi peamenüüsse");
                Console.Write("Sisesta valik: ");

                if (!int.TryParse(Console.ReadLine(), out int valik))
                {
                    Console.WriteLine("Vigane sisend! Palun sisesta number.");
                    continue;
                }

                if (valik == 0)
                {
                    return;
                }

                try
                {
                    switch (valik)
                    {
                        case 1:
                            LooToodeJaSalvestaFaili(saadavalTooted, failiNimi);
                            break;

                        case 2:
                            KuvaSaadavalTooted(saadavalTooted);
                            break;

                        default:
                            Console.WriteLine("Tundmatu valik.");
                            break;
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Andmete sisestamine ebaõnnestus: {ex.Message}");
                }

                Console.WriteLine("\nVajuta Enter, et jätkata...");
                Console.ReadLine();
            }
        }

        static void LooToodeJaSalvestaFaili(List<Toode> saadavalTooted, string failiNimi)
        {
            Console.Clear();

            Console.WriteLine("\n--- UUE TOOTE LOOMINE ---");
            TooteTüüp? valitudTüüp = ValiTooteTüüp();

            if (valitudTüüp == null)
            {
                Console.WriteLine("Tundmatu valik.");
                return;
            }

            TooteTüüp tüüp = valitudTüüp.Value;
            string tüüpVäiksega = tüüp.ToString().ToLower();

            Console.Write($"Sisesta {tüüpVäiksega} nimi: ");
            string nimi = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(nimi))
            {
                nimi = "Nimetu " + tüüpVäiksega;
            }

            Console.Write("Sisesta toote hind: ");
            string hindTekst = Console.ReadLine() ?? "";
            hindTekst = hindTekst.Replace(".", ",");

            if (!double.TryParse(hindTekst, out double hind))
            {
                Console.WriteLine("Vigane sisend! Palun sisesta number.");
                return;
            }

            // Lisaandmete tähendus sõltub toote tüübist
            TooteEriomadus eriomadus = KüsiEriomadus(tüüp);

            Toode uusToode = new Toode(nimi, tüüp, hind, eriomadus);

            saadavalTooted.Add(uusToode);
            FailiTöötlus.SalvestaToodeFaili(uusToode, failiNimi);
            Console.WriteLine("Toode lisatud ja faili salvestatud.");
        }

        static TooteTüüp? ValiTooteTüüp()
        {
            Console.WriteLine("1. Burger");
            Console.WriteLine("2. Pizza");
            Console.WriteLine("3. Sushi");
            Console.WriteLine("4. Jook");
            Console.WriteLine("5. Magustoit");
            Console.Write("Vali toote tüüp: ");

            if (!int.TryParse(Console.ReadLine(), out int valik))
            {
                Console.WriteLine("Vigane sisend! Palun sisesta number.");
                return null;
            }

            switch (valik)
            {
                case 1:
                    return TooteTüüp.Burger;

                case 2:
                    return TooteTüüp.Pizza;

                case 3:
                    return TooteTüüp.Sushi;

                case 4:
                    return TooteTüüp.Jook;

                case 5:
                    return TooteTüüp.Magustoit;

                default:
                    return null;
            }
        }

        static TooteEriomadus KüsiEriomadus(TooteTüüp tüüp)
        {
            switch (tüüp)
            {
                case TooteTüüp.Burger:
                    Console.Write("Kas burger on juustuga? (jah/ei): ");
                    string juustuVastus = (Console.ReadLine() ?? "").ToLower();
                    int juustuga = juustuVastus == "jah" || juustuVastus == "j" ? 1 : 0;
                    return TooteEriomadus.Loo(tüüp, juustuga);

                case TooteTüüp.Pizza:
                    Console.Write("Sisesta pizza läbimõõt cm: ");

                    if (!int.TryParse(Console.ReadLine(), out int läbimõõt))
                    {
                        throw new ArgumentException("Sisestatud väärtus peab olema täisarv.");
                    }

                    return TooteEriomadus.Loo(tüüp, läbimõõt);

                case TooteTüüp.Sushi:
                    Console.Write("Sisesta tükkide arv: ");

                    if (!int.TryParse(Console.ReadLine(), out int tükkideArv))
                    {
                        throw new ArgumentException("Sisestatud väärtus peab olema täisarv.");
                    }

                    return TooteEriomadus.Loo(tüüp, tükkideArv);

                case TooteTüüp.Jook:
                    Console.Write("Kas jook on gaseeritud? (jah/ei): ");
                    string gaasiVastus = (Console.ReadLine() ?? "").ToLower();
                    int gaseeritud = gaasiVastus == "jah" || gaasiVastus == "j" ? 1 : 0;
                    return TooteEriomadus.Loo(tüüp, gaseeritud);

                case TooteTüüp.Magustoit:
                    Console.Write("Sisesta kalorite arv: ");

                    if (!int.TryParse(Console.ReadLine(), out int kalorid))
                    {
                        throw new ArgumentException("Sisestatud väärtus peab olema täisarv.");
                    }

                    return TooteEriomadus.Loo(tüüp, kalorid);

                default:
                    return TooteEriomadus.Loo(tüüp, 0);
            }
        }

        static void KoostaTellimus(List<Toode> saadavalTooted, Tellimus tellimus)
        {
            Console.Clear();

            if (saadavalTooted.Count == 0)
            {
                Console.WriteLine("Saadaval tooteid ei ole. Kõigepealt loo mõni toode.");
                return;
            }

            while (true)
            {
                Console.WriteLine("\n--- TELLIMUSE KOOSTAMINE ---");
                Console.WriteLine($"Ostukorvis: {tellimus.ToodeteArv} toodet");

                if (tellimus.ToodeteArv > 0)
                {
                    KuvaOstukorviSisu(tellimus);
                }

                KuvaSaadavalTooted(saadavalTooted);
                Console.WriteLine("0. Tagasi menüüsse");
                Console.Write("Vali järgmine toode või 0 tagasi: ");

                if (!int.TryParse(Console.ReadLine(), out int valik))
                {
                    Console.WriteLine("Vigane sisend! Palun sisesta number.");
                    continue;
                }

                if (valik == 0)
                {
                    break;
                }

                if (valik < 1 || valik > saadavalTooted.Count)
                {
                    Console.WriteLine("Sellist toodet ei ole.");
                    continue;
                }

                Toode valitudToode = saadavalTooted[valik - 1];
                tellimus.LisaToode(valitudToode);

                Console.Clear();
                Console.WriteLine($">>> {valitudToode.Tüüp} \"{valitudToode.Nimi}\" lisatud ostukorvi.");
            }
        }

        static void KuvaOstukorviSisu(Tellimus tellimus)
        {
            Console.WriteLine("Ostukorvi sisu:");

            int järjekorraNumber = 1;

            foreach (Toode toode in tellimus.Tooted)
            {
                double hind = toode.ArvutaHind();
                Console.WriteLine($"{järjekorraNumber}. {toode.Tüüp} \"{toode.Nimi}\" - {hind:F2} €");
                järjekorraNumber++;
            }

            Console.WriteLine($"Kokku: {tellimus.ArvutaKoguhind():F2} €");
        }

        static void KuvaSaadavalTooted(List<Toode> saadavalTooted)
        {
            Console.WriteLine("\n--- SAADAVAL TOOTED ---");

            if (saadavalTooted.Count == 0)
            {
                Console.WriteLine("Saadaval tooteid ei ole.");
                return;
            }

            for (int i = 0; i < saadavalTooted.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                saadavalTooted[i].Kirjelda(true);
            }
        }

        static void KuvaTellimus(Tellimus tellimus)
        {
            Console.WriteLine("\n--- TELLIMUSE TULEMUSED ---");

            if (tellimus.ToodeteArv == 0)
            {
                Console.WriteLine("Tellimus on tühi.");
                return;
            }

            KuvaTellimuseTooted(tellimus);
            KuvaTellimuseKokkuvote(tellimus);
        }

        static void KuvaTellimuseTooted(Tellimus tellimus)
        {
            int järjekorraNumber = 1;

            foreach (Toode toit in tellimus.Tooted)
            {
                Console.WriteLine($"\n{järjekorraNumber}. toode");
                järjekorraNumber++;

                toit.Kirjelda();
                toit.Valmista();

                double hind = toit.ArvutaHind();
                Console.WriteLine($"Hind kokku: {hind:F2} €");
            }
        }

        static double KuvaTellimuseKokkuvote(Tellimus tellimus)
        {
            double koguhind = tellimus.ArvutaKoguhind();
            int soodustusProtsent = tellimus.LeiaSoodustusProtsent();
            double soodustusSumma = tellimus.ArvutaSoodustusSumma();
            double lõpphind = tellimus.ArvutaLõpphind();

            Console.WriteLine($"\nTellimuse vahesumma: {koguhind:F2} €");

            if (soodustusProtsent > 0)
            {
                Console.WriteLine($"Soodustus: {soodustusProtsent}% (-{soodustusSumma:F2} €)");
            }
            else
            {
                Console.WriteLine("Soodustus puudub.");
            }

            Console.WriteLine($"Maksmisele kuulub: {lõpphind:F2} €");

            return lõpphind;
        }

        static void VormistaJaMaksaTellimus(Klient klient, MakseTeenindus makseTeenindus)
        {
            Console.Clear();
            Console.WriteLine("\n--- TELLIMUSE VORMISTAMINE ---");

            if (klient.Tellimus.ToodeteArv == 0)
            {
                Console.WriteLine("Tellimus on tühi. Lisa enne maksmist mõni toode.");
                return;
            }

            KuvaTellimuseTooted(klient.Tellimus);
            double makstavSumma = KuvaTellimuseKokkuvote(klient.Tellimus);

            Makse makse = makseTeenindus.Maksa(makstavSumma);
            klient.Tellimus.LisaMakse(makse);

            if (klient.Tellimus.OnMakstud())
            {
                Console.WriteLine("\nTellimus on makstud ja kööki saadetud.");
                Console.WriteLine("Aitäh ostu eest!");
                klient.Tellimus.Tühjenda();
            }
            else
            {
                Console.WriteLine("\nMakse jäi lõpetamata. Tellimus jäi ostukorvi alles.");
            }
        }
    }
}
