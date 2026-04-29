using System;
using System.Collections.Generic;
using System.Text;

namespace ToidutellimusteSusteem
{
    public class Sushi : Toode
    {
        private int tükkideArv;

        public int TükkideArv
        {
            get { return tükkideArv; }
            set
            {
                if (value > 0)
                {
                    tükkideArv = value;
                }
                else
                {
                    throw new ArgumentException("Sushi tükkide arv peab olema suurem kui 0!");
                }
            }
        }

        public Sushi(string nimi, double üheTükiHind, int tükkideArv) : base(nimi, üheTükiHind)
        {
            TükkideArv = tükkideArv;
        }

        public override void Valmista()
        {
            Console.WriteLine($"Valmistame sushit {Nimi}: riis, täidis, nori ja lõikame tükkideks.");
        }

        public override double ArvutaHind()
        {
            return Hind * TükkideArv;
        }

        public override void Kirjelda()
        {
            Console.WriteLine($"Sushi: {Nimi} | Tükke: {TükkideArv} | Ühe tüki hind: {Hind} €");
        }
    }
}
