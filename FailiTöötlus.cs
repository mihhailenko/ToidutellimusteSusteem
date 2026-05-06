using System;
using System.Collections.Generic;
using System.IO;

namespace ToidutellimusteSusteem
{
    internal static class FailiTöötlus
    {
        internal static void SalvestaToodeFaili(Toode toode, string failiNimi)
        {
            File.AppendAllText(failiNimi, toode.KoostaFailiRida() + Environment.NewLine);
        }

        internal static List<Toode> LaeTootedFailist(string failiNimi)
        {
            List<Toode> tooted = new List<Toode>();

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

                if (!Enum.TryParse(osad[0], out TooteTüüp tüüp))
                {
                    continue;
                }

                string nimi = osad[1];

                osad[2] = osad[2].Replace(".", ",");

                if (!double.TryParse(osad[2], out double hind))
                {
                    continue;
                }

                try
                {
                    if (int.TryParse(osad[3], out int eriomadus))
                    {
                        tooted.Add(new Toode(nimi, tüüp, hind, eriomadus));
                    }
                    else if (bool.TryParse(osad[3], out bool jahVõiEi))
                    {
                        int väärtus = jahVõiEi ? 1 : 0;
                        tooted.Add(new Toode(nimi, tüüp, hind, väärtus));
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
