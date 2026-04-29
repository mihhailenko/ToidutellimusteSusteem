using System;
using System.Collections.Generic;
using System.Text;

namespace ToidutellimusteSusteem
{
    public class Burger : Toode
    {
        public bool Juustuga { get; set; }

        public Burger(string nimi, double hind, bool juustuga) : base(nimi, hind)
        {
            Juustuga = juustuga;
        }

        public override void Valmista()
        {
            if (Juustuga)
            {
                Console.WriteLine($"Valmistame burgerit {Nimi}: kukkel, liha, salat ja juust.");
            }
            else
            {
                Console.WriteLine($"Valmistame burgerit {Nimi}: kukkel, liha ja salat.");
            }
        }

        public override double ArvutaHind()
        {
            double lõppHind = Hind;

            if (Juustuga)
            {
                lõppHind += .50;
            }

            return lõppHind;
        }

        public override void Kirjelda()
        {
            string juust = Juustuga ? "jah" : "ei";
            Console.WriteLine($"Burger: {Nimi} | Juustuga: {juust}");
        }
    }
}
