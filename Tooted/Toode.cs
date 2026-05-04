using System;
using System.Collections.Generic;
using System.Text;

namespace ToidutellimusteSusteem
{
    public abstract class Toode : IValmistatav
    {
        private double hind;

        public string Nimi { get; set; }

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

        public Toode(string nimi, double hind)
        {
            Nimi = nimi;
            Hind = hind;
        }

        // Alamklassid peavad ise kirjeldama, kuidas toode välja näeb
        public abstract void Kirjelda(bool kuvaHind = false);

        // Liidese meetodid, mille sisu tuleb alamklassides
        public abstract void Valmista();
        public abstract double ArvutaHind();
    }
}
