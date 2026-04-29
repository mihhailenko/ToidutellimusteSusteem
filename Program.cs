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


            // Ühine nimekiri kõikide toitude jaoks
            List<IValmistatav> tellimus = new List<IValmistatav>();

            while (true)
            {
                Console.WriteLine("\n--- TOIDUTELLIMUSTE SÜSTEEM ---");
                Console.WriteLine("1. Lisa toode");
                Console.WriteLine("2. Kuva tellimus");
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
                            LisaToode(tellimus);
                            break;
                        case 2:
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

        static void LisaToode(List<IValmistatav> tellimus)
        {
            Console.WriteLine("\n--- TOOTE LISAMINE ---");
            Console.WriteLine("1. Burger");
            Console.WriteLine("2. Pizza");
            Console.WriteLine("3. Sushi");
            Console.WriteLine("4. Jook");
            Console.WriteLine("5. Magustoit");
            Console.WriteLine("0. Tagasi");
            Console.Write("Vali toode: ");

            if (!int.TryParse(Console.ReadLine(), out int valik))
            {
                Console.WriteLine("Vigane sisend! Palun sisesta number.");
                return;
            }

            if (valik == 0)
            {
                return;
            }

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
                    bool burgeriHindOk = double.TryParse(Console.ReadLine(), out double burgeriHind);

                    if (!burgeriHindOk)
                    {
                        Console.WriteLine("Vigane sisend! Palun sisesta number.");
                        return;
                    }

                    Console.Write("Kas burger on juustuga? (+50 senti) (jah/ei): ");
                    string juustuVastus = Console.ReadLine().ToLower();
                    bool juustuga = juustuVastus == "jah" || juustuVastus == "j";

                    tellimus.Add(new Burger(burgeriNimi, burgeriHind, juustuga));
                    Console.WriteLine("Burger lisatud tellimusse.");
                    break;

                case 2:
                    Console.Write("Sisesta pizza nimi: ");
                    string pizzaNimi = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(pizzaNimi))
                    {
                        pizzaNimi = "Nimetu pizza";
                    }

                    Console.Write("Sisesta pizza põhihind: ");
                    bool pizzaHindOk = double.TryParse(Console.ReadLine(), out double pizzaHind);

                    Console.Write("Sisesta pizza läbimõõt cm: ");
                    bool läbimõõtOk = int.TryParse(Console.ReadLine(), out int läbimõõt);

                    if (pizzaHindOk && läbimõõtOk)
                    {
                        tellimus.Add(new Pizza(pizzaNimi, pizzaHind, läbimõõt));
                        Console.WriteLine("Pizza lisatud tellimusse.");
                    }
                    else
                    {
                        Console.WriteLine("Vigane sisend! Hind ja läbimõõt peavad olema numbrid.");
                    }
                    break;

                case 3:
                    Console.Write("Sisesta sushi nimi: ");
                    string sushiNimi = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(sushiNimi))
                    {
                        sushiNimi = "Nimetu sushi";
                    }

                    Console.Write("Sisesta ühe tüki hind: ");
                    bool sushiHindOk = double.TryParse(Console.ReadLine(), out double sushiHind);

                    Console.Write("Sisesta tükkide arv: ");
                    bool tükkideArvOk = int.TryParse(Console.ReadLine(), out int tükkideArv);

                    if (sushiHindOk && tükkideArvOk)
                    {
                        tellimus.Add(new Sushi(sushiNimi, sushiHind, tükkideArv));
                        Console.WriteLine("Sushi lisatud tellimusse.");
                    }
                    else
                    {
                        Console.WriteLine("Vigane sisend! Hind ja tükkide arv peavad olema numbrid.");
                    }
                    break;

                case 4:
                    Console.Write("Sisesta joogi nimi: ");
                    string joogiNimi = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(joogiNimi))
                    {
                        joogiNimi = "Nimetu jook";
                    }

                    Console.Write("Sisesta joogi hind: ");
                    bool joogiHindOk = double.TryParse(Console.ReadLine(), out double joogiHind);

                    if (!joogiHindOk)
                    {
                        Console.WriteLine("Vigane sisend! Palun sisesta number.");
                        return;
                    }

                    Console.Write("Kas jook on gaseeritud? (+50 senti) (jah/ei): ");
                    string gaasiVastus = Console.ReadLine().ToLower();
                    bool gaseeritud = gaasiVastus == "jah" || gaasiVastus == "j";

                    tellimus.Add(new Jook(joogiNimi, joogiHind, gaseeritud));
                    Console.WriteLine("Jook lisatud tellimusse.");
                    break;

                case 5:
                    Console.Write("Sisesta magustoidu nimi: ");
                    string magustoiduNimi = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(magustoiduNimi))
                    {
                        magustoiduNimi = "Nimetu magustoit";
                    }

                    Console.Write("Sisesta magustoidu hind: ");
                    bool magustoiduHindOk = double.TryParse(Console.ReadLine(), out double magustoiduHind);

                    Console.Write("Sisesta kalorite arv: ");
                    bool kaloridOk = int.TryParse(Console.ReadLine(), out int kalorid);

                    if (magustoiduHindOk && kaloridOk)
                    {
                        tellimus.Add(new Magustoit(magustoiduNimi, magustoiduHind, kalorid));
                        Console.WriteLine("Magustoit lisatud tellimusse.");
                    }
                    else
                    {
                        Console.WriteLine("Vigane sisend! Hind ja kalorid peavad olema numbrid.");
                    }
                    break;

                default:
                    Console.WriteLine("Tundmatu valik.");
                    break;
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
