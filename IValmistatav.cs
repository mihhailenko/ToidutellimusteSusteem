using System;
using System.Collections.Generic;
using System.Text;

namespace ToidutellimusteSusteem
{
    // Liides määrab, mida iga valmistatav toit peab oskama
    public interface IValmistatav
    {
        void Valmista();
        double ArvutaHind();
    }
}
