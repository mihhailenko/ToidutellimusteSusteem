using System;
using System.Collections.Generic;
using System.Text;

namespace ToidutellimusteSusteem
{
    public class Jook : Toode
    {
        public bool Gaseeritud { get; set; }

        public Jook(string nimi, double hind, bool gaseeritud) : base(nimi, hind)
        {
            Gaseeritud = gaseeritud;
        }

        public override void Valmista()
        {
            if (Gaseeritud)
            {
                Console.WriteLine($"Valmistame joogi {Nimi}: jahutame ja lisame gaasi.");
            }
            else
            {
                Console.WriteLine($"Valmistame joogi {Nimi}: jahutame ja valame klaasi.");
            }
        }

        public override double ArvutaHind()
        {
            double lõppHind = Hind;

            if (Gaseeritud)
            {
                lõppHind += .50;
            }

            return lõppHind;
        }

        public override void Kirjelda()
        {
            string gaas = Gaseeritud ? "jah" : "ei";
            Console.WriteLine($"Jook: {Nimi} | Gaseeritud: {gaas}");
        }
    }
}
