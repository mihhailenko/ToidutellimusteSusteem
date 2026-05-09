namespace ToidutellimusteSusteem
{
    public class Sularahamakse : Makseviis
    {
        public double SaadudSumma { get; set; }
        public double Tagasiraha { get; set; }

        public Sularahamakse() : base("Sularaha", 0)
        {
            SaadudSumma = 0;
            Tagasiraha = 0;
        }

        public override Makse Maksa(double summa)
        {
            Makse makse = new Makse(summa, this);

            Console.WriteLine($"\nSularahas tuleb tasuda: {makse.MakstavSumma:F2} €");
            Console.Write("Sisesta kliendilt saadud summa: ");
            string saadudRahaTekst = Console.ReadLine() ?? "";
            saadudRahaTekst = saadudRahaTekst.Replace(".", ",");

            if (!double.TryParse(saadudRahaTekst, out double saadudRaha))
            {
                Console.WriteLine("Vigane sisend! Palun sisesta number.");
                makse.Staatus = MakseStaatus.Ebaõnnestus;
                return makse;
            }

            SaadudSumma = saadudRaha;

            if (SaadudSumma < makse.MakstavSumma)
            {
                double puuduvSumma = makse.MakstavSumma - SaadudSumma;
                Console.WriteLine($"Raha ei piisa. Puudu on {puuduvSumma:F2} €.");
                makse.Staatus = MakseStaatus.Ebaõnnestus;
                return makse;
            }

            Tagasiraha = SaadudSumma - makse.MakstavSumma;
            makse.Staatus = MakseStaatus.Õnnestus;

            Console.WriteLine($"Makse vastu võetud. Tagasiraha: {Tagasiraha:F2} €");

            return makse;
        }
    }
}
