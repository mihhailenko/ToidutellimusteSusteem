namespace ToidutellimusteSusteem
{
    public class Klient : Kasutaja
    {
        public override KasutajaRoll Roll
        {
            get { return KasutajaRoll.Klient; }
        }

        public Tellimus Tellimus { get; set; }

        public Klient(string nimi) : base(nimi)
        {
            Tellimus = new Tellimus();
        }
    }
}
