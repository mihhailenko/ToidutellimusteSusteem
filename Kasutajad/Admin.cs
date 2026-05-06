namespace ToidutellimusteSusteem
{
    public class Admin : Kasutaja
    {
        public override KasutajaRoll Roll
        {
            get { return KasutajaRoll.Admin; }
        }

        public Admin(string nimi) : base(nimi)
        {
        }
    }
}
