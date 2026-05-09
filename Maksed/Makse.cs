namespace ToidutellimusteSusteem
{
    public class Makse
    {
        private double summa;

        public double Summa
        {
            get { return summa; }
            set
            {
                if (value >= 0)
                {
                    summa = value;
                }
                else
                {
                    throw new ArgumentException("Makse summa ei tohi olla negatiivne!");
                }
            }
        }

        public MakseStaatus Staatus { get; set; }
        public Makseviis? Makseviis { get; set; }

        public double Lisatasu
        {
            get
            {
                if (Makseviis == null)
                {
                    return 0;
                }

                return Makseviis.Lisatasu;
            }
        }

        public double MakstavSumma
        {
            get { return Summa + Lisatasu; }
        }

        public Makse(double summa)
        {
            Summa = summa;
            Staatus = MakseStaatus.Ootel;
        }

        public Makse(double summa, Makseviis makseviis) : this(summa)
        {
            Makseviis = makseviis;
        }
    }
}
