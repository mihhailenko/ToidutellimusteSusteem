using System;
using System.Collections.Generic;
using System.IO;

namespace ToidutellimusteSusteem
{
    internal static class FailiTöötlus
    {
        internal static void SalvestaToodeFaili(IValmistatav toode, string failiNimi)
        {
            string rida = "";

            if (toode is Burger burger)
            {
                rida = $"Burger;{burger.Nimi};{burger.Hind.ToString().Replace(",", ".")};{burger.Juustuga}";
            }
            else if (toode is Pizza pizza)
            {
                rida = $"Pizza;{pizza.Nimi};{pizza.Hind.ToString().Replace(",", ".")};{pizza.Läbimõõt}";
            }
            else if (toode is Sushi sushi)
            {
                rida = $"Sushi;{sushi.Nimi};{sushi.Hind.ToString().Replace(",", ".")};{sushi.TükkideArv}";
            }
            else if (toode is Jook jook)
            {
                rida = $"Jook;{jook.Nimi};{jook.Hind.ToString().Replace(",", ".")};{jook.Gaseeritud}";
            }
            else if (toode is Magustoit magustoit)
            {
                rida = $"Magustoit;{magustoit.Nimi};{magustoit.Hind.ToString().Replace(",", ".")};{magustoit.Kalorid}";
            }

            if (rida != "")
            {
                File.AppendAllText(failiNimi, rida + Environment.NewLine);
            }
        }

        internal static List<IValmistatav> LaeTootedFailist(string failiNimi)
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

                osad[2] = osad[2].Replace(".", ",");

                if (!double.TryParse(osad[2], out double hind))
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
    }
}
