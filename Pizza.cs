using System;
using System.Collections.Generic;
using System.Text;

namespace ToidutellimusteSusteem
{
    public class Pizza : Toode
    {
        private int läbimõõt;

        public int Läbimõõt
        {
            get { return läbimõõt; }
            set
            {
                if (value > 0)
                {
                    läbimõõt = value;
                }
                else
                {
                    throw new ArgumentException("Pizza läbimõõt peab olema suurem kui 0!");
                }
            }
        }

        public Pizza(string nimi, double hind, int läbimõõt) : base(nimi, hind)
        {
            Läbimõõt = läbimõõt;
        }

        public override void Valmista()
        {
            Console.WriteLine($"Valmistame pizzat {Nimi}: paneme põhja, kastme, lisandid ja küpsetame ahjus.");
        }

        public override double ArvutaHind()
        {
            return Hind + Läbimõõt * 0.15;
        }

        public override void Kirjelda()
        {
            Console.WriteLine($"Pizza: {Nimi} | Läbimõõt: {Läbimõõt} cm");
        }
    }
}
