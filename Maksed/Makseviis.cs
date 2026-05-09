namespace ToidutellimusteSusteem
{
    public abstract class Makseviis
    {
        private double lisatasu;

        public string Nimetus { get; set; }

        public double Lisatasu
        {
            get { return lisatasu; }
            set
            {
                if (value >= 0)
                {
                    lisatasu = value;
                }
                else
                {
                    throw new ArgumentException("Makse lisatasu ei tohi olla negatiivne!");
                }
            }
        }

        public Makseviis(string nimetus, double lisatasu)
        {
            Nimetus = nimetus;
            Lisatasu = lisatasu;
        }

        public abstract Makse Maksa(double summa);
    }
}
