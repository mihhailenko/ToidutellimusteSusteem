using System.Collections.Generic;

namespace ToidutellimusteSusteem
{
    public class Tellimus
    {
        private List<Toode> tooted = new List<Toode>();

        public List<Toode> Tooted
        {
            get { return tooted; }
        }

        public int ToodeteArv
        {
            get { return tooted.Count; }
        }

        public void LisaToode(Toode toode)
        {
            tooted.Add(toode);
        }

        public void Tühjenda()
        {
            tooted.Clear();
        }

        public double ArvutaKoguhind()
        {
            double koguhind = 0;

            foreach (Toode toode in tooted)
            {
                koguhind += toode.ArvutaHind();
            }

            return koguhind;
        }

        public int LeiaSoodustusProtsent()
        {
            double koguhind = ArvutaKoguhind();

            if (koguhind >= 60)
            {
                return 15;
            }

            if (koguhind >= 40)
            {
                return 10;
            }

            if (koguhind >= 25)
            {
                return 5;
            }

            return 0;
        }

        public double ArvutaSoodustusSumma()
        {
            double koguhind = ArvutaKoguhind();
            int soodustusProtsent = LeiaSoodustusProtsent();

            return koguhind * soodustusProtsent / 100;
        }

        public double ArvutaLõpphind()
        {
            return ArvutaKoguhind() - ArvutaSoodustusSumma();
        }
    }
}
