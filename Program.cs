using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

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
            List<IValmistatav> saadavalTooted = LaeTootedFailist(failiNimi);

            // Siia lisatakse ainult kliendi tellimuse tooted
            List<IValmistatav> tellimus = new List<IValmistatav>();

            while (true)
            {
                Console.WriteLine("\n--- TOIDUTELLIMUSTE SÜSTEEM ---");
                Console.WriteLine("1. Loo uus toode ja salvesta faili");
                Console.WriteLine("2. Koosta tellimus");
                Console.WriteLine("3. Kuva saadaval tooted");
                Console.WriteLine("4. Kuva tellimus");
                Console.WriteLine("0. Välju");
                Console.Write("Sisesta oma valik: ");

                if (!int.TryParse(Console.ReadLine(), out int valik))
                {
                    Console.WriteLine("Vigane sisend! Palun sisesta number.");
                    continue;
                }

                if (valik == 0)
                {
                    break;
                }

                try
                {
                    switch (valik)
                    {
                        case 1:
                            LooToodeJaSalvestaFaili(saadavalTooted, failiNimi);
                            break;
                        case 2:
                            KoostaTellimus(saadavalTooted, tellimus);
                            break;
                        case 3:
                            KuvaSaadavalTooted(saadavalTooted);
                            break;
                        case 4:
                            KuvaTellimus(tellimus);
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
            }

            Console.WriteLine("Aitah kasutamise eest! Tulge tagasi mõni kord veel.");
        }

        static void LooToodeJaSalvestaFaili(List<IValmistatav> saadavalTooted, string failiNimi)
        {
            Console.WriteLine("\n--- UUE TOOTE LOOMINE ---");
            Console.WriteLine("1. Burger");
            Console.WriteLine("2. Pizza");
            Console.WriteLine("3. Sushi");
            Console.WriteLine("4. Jook");
            Console.WriteLine("5. Magustoit");
            Console.Write("Vali toote tüüp: ");

            if (!int.TryParse(Console.ReadLine(), out int valik))
            {
                Console.WriteLine("Vigane sisend! Palun sisesta number.");
                return;
            }

            IValmistatav? uusToode = null;

            switch (valik)
            {
                case 1:
                    Console.Write("Sisesta burgeri nimi: ");
                    string burgeriNimi = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(burgeriNimi))
                    {
                        burgeriNimi = "Nimetu burger";
                    }

                    Console.Write("Sisesta burgeri hind: ");
                    string burgeriHindTekst = Console.ReadLine();
                    burgeriHindTekst = burgeriHindTekst.Replace(",", ".");

                    if (!double.TryParse(burgeriHindTekst, out double burgeriHind))
                    {
                        Console.WriteLine("Vigane sisend! Palun sisesta number.");
                        return;
                    }

                    Console.Write("Kas burger on juustuga? (jah/ei): ");
                    string juustuVastus = (Console.ReadLine()).ToLower();
                    bool juustuga = juustuVastus == "jah" || juustuVastus == "j";

                    uusToode = new Burger(burgeriNimi, burgeriHind, juustuga);
                    break;

                case 2:
                    Console.Write("Sisesta pizza nimi: ");
                    string pizzaNimi = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(pizzaNimi))
                    {
                        pizzaNimi = "Nimetu pizza";
                    }

                    Console.Write("Sisesta pizza põhihind: ");
                    string pizzaHindTekst = Console.ReadLine();
                    pizzaHindTekst = pizzaHindTekst.Replace(",", ".");

                    if (!double.TryParse(pizzaHindTekst, NumberStyles.Any, CultureInfo.InvariantCulture, out double pizzaHind))
                    {
                        Console.WriteLine("Vigane sisend! Palun sisesta number.");
                        return;
                    }

                    Console.Write("Sisesta pizza läbimõõt cm: ");

                    if (!int.TryParse(Console.ReadLine(), out int läbimõõt))
                    {
                        Console.WriteLine("Vigane sisend! Palun sisesta täisarv.");
                        return;
                    }

                    uusToode = new Pizza(pizzaNimi, pizzaHind, läbimõõt);
                    break;

                case 3:
                    Console.Write("Sisesta sushi nimi: ");
                    string sushiNimi = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(sushiNimi))
                    {
                        sushiNimi = "Nimetu sushi";
                    }

                    Console.Write("Sisesta ühe tüki hind: ");
                    string sushiHindTekst = Console.ReadLine();
                    sushiHindTekst = sushiHindTekst.Replace(",", ".");

                    if (!double.TryParse(sushiHindTekst, NumberStyles.Any, CultureInfo.InvariantCulture, out double sushiHind))
                    {
                        Console.WriteLine("Vigane sisend! Palun sisesta number.");
                        return;
                    }

                    Console.Write("Sisesta tükkide arv: ");

                    if (!int.TryParse(Console.ReadLine(), out int tükkideArv))
                    {
                        Console.WriteLine("Vigane sisend! Palun sisesta täisarv.");
                        return;
                    }

                    uusToode = new Sushi(sushiNimi, sushiHind, tükkideArv);
                    break;

                case 4:
                    Console.Write("Sisesta joogi nimi: ");
                    string joogiNimi = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(joogiNimi))
                    {
                        joogiNimi = "Nimetu jook";
                    }

                    Console.Write("Sisesta joogi hind: ");
                    string joogiHindTekst = Console.ReadLine();
                    joogiHindTekst = joogiHindTekst.Replace(",", ".");

                    if (!double.TryParse(joogiHindTekst, NumberStyles.Any, CultureInfo.InvariantCulture, out double joogiHind))
                    {
                        Console.WriteLine("Vigane sisend! Palun sisesta number.");
                        return;
                    }

                    Console.Write("Kas jook on gaseeritud? (jah/ei): ");
                    string gaasiVastus = (Console.ReadLine()).ToLower();
                    bool gaseeritud = gaasiVastus == "jah" || gaasiVastus == "j";

                    uusToode = new Jook(joogiNimi, joogiHind, gaseeritud);
                    break;

                case 5:
                    Console.Write("Sisesta magustoidu nimi: ");
                    string magustoiduNimi = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(magustoiduNimi))
                    {
                        magustoiduNimi = "Nimetu magustoit";
                    }

                    Console.Write("Sisesta magustoidu hind: ");
                    string magustoiduHindTekst = Console.ReadLine();
                    magustoiduHindTekst = magustoiduHindTekst.Replace(",", ".");

                    if (!double.TryParse(magustoiduHindTekst, NumberStyles.Any, CultureInfo.InvariantCulture, out double magustoiduHind))
                    {
                        Console.WriteLine("Vigane sisend! Palun sisesta number.");
                        return;
                    }

                    Console.Write("Sisesta kalorite arv: ");

                    if (!int.TryParse(Console.ReadLine(), out int kalorid))
                    {
                        Console.WriteLine("Vigane sisend! Palun sisesta täisarv.");
                        return;
                    }

                    uusToode = new Magustoit(magustoiduNimi, magustoiduHind, kalorid);
                    break;

                default:
                    Console.WriteLine("Tundmatu valik.");
                    return;
            }

            if (uusToode != null)
            {
                saadavalTooted.Add(uusToode);
                SalvestaToodeFaili(uusToode, failiNimi);
                Console.WriteLine("Toode lisatud ja faili salvestatud.");
            }
        }

        static void SalvestaToodeFaili(IValmistatav toode, string failiNimi)
        {
            string rida = "";

            if (toode is Burger burger)
            {
                rida = $"Burger;{burger.Nimi};{burger.Hind.ToString(CultureInfo.InvariantCulture)};{burger.Juustuga}";
            }
            else if (toode is Pizza pizza)
            {
                rida = $"Pizza;{pizza.Nimi};{pizza.Hind.ToString(CultureInfo.InvariantCulture)};{pizza.Läbimõõt}";
            }
            else if (toode is Sushi sushi)
            {
                rida = $"Sushi;{sushi.Nimi};{sushi.Hind.ToString(CultureInfo.InvariantCulture)};{sushi.TükkideArv}";
            }
            else if (toode is Jook jook)
            {
                rida = $"Jook;{jook.Nimi};{jook.Hind.ToString(CultureInfo.InvariantCulture)};{jook.Gaseeritud}";
            }
            else if (toode is Magustoit magustoit)
            {
                rida = $"Magustoit;{magustoit.Nimi};{magustoit.Hind.ToString(CultureInfo.InvariantCulture)};{magustoit.Kalorid}";
            }

            if (rida != "")
            {
                File.AppendAllText(failiNimi, rida + Environment.NewLine);
            }
        }

        static List<IValmistatav> LaeTootedFailist(string failiNimi)
        {
            List<IValmistatav> tooted = new List<IValmistatav>();

            if (!File.Exists(failiNimi))
            {
                return tooted;
            }

            string[] read = File.ReadAllLines(failiNimi);

            foreach (string rida in read)
            {
                if (string.IsNullOrWhiteSpace(rida))
                {
                    continue;
                }

                string[] osad = rida.Split(';');

                if (osad.Length != 4)
                {
                    continue;
                }

                string tüüp = osad[0];
                string nimi = osad[1];

                if (!double.TryParse(osad[2], NumberStyles.Any, CultureInfo.InvariantCulture, out double hind))
                {
                    continue;
                }

                try
                {
                    switch (tüüp)
                    {
                        case "Burger":
                            if (bool.TryParse(osad[3], out bool juustuga))
                            {
                                tooted.Add(new Burger(nimi, hind, juustuga));
                            }
                            break;

                        case "Pizza":
                            if (int.TryParse(osad[3], out int läbimõõt))
                            {
                                tooted.Add(new Pizza(nimi, hind, läbimõõt));
                            }
                            break;

                        case "Sushi":
                            if (int.TryParse(osad[3], out int tükkideArv))
                            {
                                tooted.Add(new Sushi(nimi, hind, tükkideArv));
                            }
                            break;

                        case "Jook":
                            if (bool.TryParse(osad[3], out bool gaseeritud))
                            {
                                tooted.Add(new Jook(nimi, hind, gaseeritud));
                            }
                            break;

                        case "Magustoit":
                            if (int.TryParse(osad[3], out int kalorid))
                            {
                                tooted.Add(new Magustoit(nimi, hind, kalorid));
                            }
                            break;
                    }
                }
                catch (ArgumentException)
                {
                    // Kui failis on vigased andmed, siis seda rida ei lisata nimekirja
                }
            }

            return tooted;
        }

        static void KoostaTellimus(List<IValmistatav> saadavalTooted, List<IValmistatav> tellimus)
        {
            if (saadavalTooted.Count == 0)
            {
                Console.WriteLine("Saadaval tooteid ei ole. Kõigepealt loo mõni toode.");
                return;
            }

            while (true)
            {
                Console.WriteLine("\n--- TELLIMUSE KOOSTAMINE ---");
                KuvaSaadavalTooted(saadavalTooted);
                Console.WriteLine("0. Tagasi menüüsse");
                Console.Write("Vali toode tellimusse: ");

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

                tellimus.Add(saadavalTooted[valik - 1]);
                Console.WriteLine("Toode lisatud tellimusse.");
            }
        }

        static void KuvaSaadavalTooted(List<IValmistatav> saadavalTooted)
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

                if (saadavalTooted[i] is Toode toode)
                {
                    toode.Kirjelda();
                }
            }
        }

        static void KuvaTellimus(List<IValmistatav> tellimus)
        {
            Console.WriteLine("\n--- TELLIMUSE TULEMUSED ---");

            if (tellimus.Count == 0)
            {
                Console.WriteLine("Tellimus on tühi.");
                return;
            }

            double koguhind = 0;
            int järjekorraNumber = 1;

            foreach (Toode toit in tellimus)
            {
                Console.WriteLine($"\n{järjekorraNumber}. toode");
                järjekorraNumber++;


                toit.Kirjelda();
                toit.Valmista();

                double hind = toit.ArvutaHind();
                koguhind += hind;

                Console.WriteLine($"Hind kokku: {hind:F2} €");
            }

            Console.WriteLine($"\nKogu tellimuse hind: {koguhind:F2} €");
        }
    }
}
