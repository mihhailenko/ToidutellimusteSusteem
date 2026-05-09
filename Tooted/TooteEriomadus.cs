namespace ToidutellimusteSusteem
{
    public class TooteEriomadus
    {
        public string Nimetus { get; set; }
        public int Väärtus { get; set; }
        public string Ühik { get; set; }
        public bool KasJahEi { get; set; }

        public TooteEriomadus(string nimetus, int väärtus, string ühik = "", bool kasJahEi = false)
        {
            Nimetus = nimetus;
            Väärtus = väärtus;
            Ühik = ühik;
            KasJahEi = kasJahEi;
        }

        public string KoostaTekst()
        {
            if (KasJahEi)
            {
                return $"{Nimetus}: {(Väärtus == 1 ? "jah" : "ei")}";
            }

            if (string.IsNullOrWhiteSpace(Ühik))
            {
                return $"{Nimetus}: {Väärtus}";
            }

            return $"{Nimetus}: {Väärtus} {Ühik}";
        }

        public static TooteEriomadus Loo(TooteTüüp tüüp, int väärtus)
        {
            switch (tüüp)
            {
                case TooteTüüp.Burger:
                    return new TooteEriomadus("Juustuga", väärtus, "", true);

                case TooteTüüp.Pizza:
                    return new TooteEriomadus("Läbimõõt", väärtus, "cm");

                case TooteTüüp.Sushi:
                    return new TooteEriomadus("Tükke", väärtus);

                case TooteTüüp.Jook:
                    return new TooteEriomadus("Gaseeritud", väärtus, "", true);

                case TooteTüüp.Magustoit:
                    return new TooteEriomadus("Kalorid", väärtus, "kcal");

                default:
                    return new TooteEriomadus("Eriomadus", väärtus);
            }
        }
    }
}
