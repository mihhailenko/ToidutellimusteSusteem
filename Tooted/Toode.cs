namespace ToidutellimusteSusteem
{
    public enum TooteTüüp
    {
        Burger,
        Pizza,
        Sushi,
        Jook,
        Magustoit
    }

    public class Toode : IValmistatav
    {
        private double hind;
        private TooteEriomadus eriomadus = TooteEriomadus.Loo(TooteTüüp.Burger, 0);

        public string Nimi { get; set; }
        public TooteTüüp Tüüp { get; set; }

        public double Hind
        {
            get { return hind; }
            set
            {
                if (value >= 0)
                {
                    hind = value;
                }
                else
                {
                    throw new ArgumentException("Hind ei tohi olla negatiivne!");
                }
            }
        }

        // Eriomaduse sisu sõltub toote tüübist: juust, läbimõõt, tükid, gaas või kalorid
        public TooteEriomadus Eriomadus
        {
            get { return eriomadus; }
            set
            {
                if (KasEriomadusSobib(value.Väärtus))
                {
                    eriomadus = value;
                }
                else
                {
                    throw new ArgumentException("Toote lisaandmed ei sobi valitud tüübiga!");
                }
            }
        }

        public Toode(string nimi, TooteTüüp tüüp, double hind, TooteEriomadus eriomadus)
        {
            Nimi = nimi;
            Tüüp = tüüp;
            Hind = hind;
            Eriomadus = eriomadus;
        }

        public void Kirjelda(bool kuvaHind = false)
        {
            string hindTekst = kuvaHind ? $" | Hind: {ArvutaHind():F2} €" : "";
            Console.WriteLine($"{Tüüp}: {Nimi} | {Eriomadus.KoostaTekst()}{KoostaLisatekst()}{hindTekst}");
        }

        public void Valmista()
        {
            switch (Tüüp)
            {
                case TooteTüüp.Burger:
                    if (Eriomadus.Väärtus == 1)
                    {
                        Console.WriteLine($"Valmistame burgerit {Nimi}: kukkel, liha, salat ja juust.");
                    }
                    else
                    {
                        Console.WriteLine($"Valmistame burgerit {Nimi}: kukkel, liha ja salat.");
                    }
                    break;

                case TooteTüüp.Pizza:
                    Console.WriteLine($"Valmistame pizzat {Nimi}: paneme põhja, kastme, lisandid ja küpsetame ahjus.");
                    break;

                case TooteTüüp.Sushi:
                    Console.WriteLine($"Valmistame sushit {Nimi}: riis, täidis, nori ja lõikame tükkideks.");
                    break;

                case TooteTüüp.Jook:
                    if (Eriomadus.Väärtus == 1)
                    {
                        Console.WriteLine($"Valmistame joogi {Nimi}: jahutame ja lisame gaasi.");
                    }
                    else
                    {
                        Console.WriteLine($"Valmistame joogi {Nimi}: jahutame ja valame klaasi.");
                    }
                    break;

                case TooteTüüp.Magustoit:
                    Console.WriteLine($"Valmistame magustoitu {Nimi}: segame koostisosad ja kaunistame enne serveerimist.");
                    break;
            }
        }

        public double ArvutaHind()
        {
            switch (Tüüp)
            {
                case TooteTüüp.Burger:
                case TooteTüüp.Jook:
                    if (Eriomadus.Väärtus == 1)
                    {
                        return Hind + 0.50;
                    }
                    return Hind;

                case TooteTüüp.Pizza:
                    return Hind + Eriomadus.Väärtus * 0.15;

                case TooteTüüp.Sushi:
                    return Hind * Eriomadus.Väärtus;

                case TooteTüüp.Magustoit:
                    if (Eriomadus.Väärtus > 500)
                    {
                        return Hind * 1.2;
                    }
                    return Hind;

                default:
                    return Hind;
            }
        }

        public string KoostaLisatekst()
        {
            switch (Tüüp)
            {
                case TooteTüüp.Sushi:
                    return $" | Ühe tüki hind: {Hind:F2} €";

                default:
                    return "";
            }
        }

        public string KoostaFailiRida()
        {
            return $"{Tüüp};{Nimi};{Hind.ToString().Replace(",", ".")};{Eriomadus.Väärtus}";
        }

        private bool KasEriomadusSobib(int väärtus)
        {
            switch (Tüüp)
            {
                case TooteTüüp.Burger:
                case TooteTüüp.Jook:
                    return väärtus == 0 || väärtus == 1;

                case TooteTüüp.Pizza:
                case TooteTüüp.Sushi:
                    return väärtus > 0;

                case TooteTüüp.Magustoit:
                    return väärtus >= 0;

                default:
                    return false;
            }
        }
    }
}
