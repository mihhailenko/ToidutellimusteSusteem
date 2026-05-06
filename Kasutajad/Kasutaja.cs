namespace ToidutellimusteSusteem
{
    public enum KasutajaRoll
    {
        Klient,
        Admin
    }

    public abstract class Kasutaja
    {
        public string Nimi { get; set; }
        public abstract KasutajaRoll Roll { get; }

        public Kasutaja(string nimi)
        {
            Nimi = nimi;
        }
    }
}
