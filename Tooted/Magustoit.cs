using System;
using System.Collections.Generic;
using System.Text;

namespace ToidutellimusteSusteem
{
    public class Magustoit : Toode
    {
        private int kalorid;

        public int Kalorid
        {
            get { return kalorid; }
            set
            {
                if (value >= 0)
                {
                    kalorid = value;
                }
                else
                {
                    throw new ArgumentException("Kalorite arv ei tohi olla negatiivne!");
                }
            }
        }

        public Magustoit(string nimi, double hind, int kalorid) : base(nimi, hind)
        {
            Kalorid = kalorid;
        }

        public override void Valmista()
        {
            Console.WriteLine($"Valmistame magustoitu {Nimi}: segame koostisosad ja kaunistame enne serveerimist.");
        }

        public override double ArvutaHind()
        {
            double lõppHind = Hind;

            if (Kalorid > 500)
            {
                lõppHind = Hind * 1.2;
            }

            return lõppHind;
        }

        public override void Kirjelda()
        {
            Console.WriteLine($"Magustoit: {Nimi} | Kalorid: {Kalorid} kcal");
        }
    }
}
