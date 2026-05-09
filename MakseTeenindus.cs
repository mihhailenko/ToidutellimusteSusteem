namespace ToidutellimusteSusteem
{
    public class MakseTeenindus
    {
        public Makse Maksa(double makstavSumma)
        {
            Console.WriteLine("\nVali makseviis:");
            Console.WriteLine("1. Pangakaart");
            Console.WriteLine("2. Sularaha");
            Console.WriteLine("0. Katkesta");
            Console.Write("Sisesta valik: ");

            if (!int.TryParse(Console.ReadLine(), out int valik))
            {
                Console.WriteLine("Vigane sisend! Palun sisesta number.");
                Makse makse = new Makse(makstavSumma);
                makse.Staatus = MakseStaatus.Ebaõnnestus;
                return makse;
            }

            switch (valik)
            {
                case 1:
                    return new Kaardimakse().Maksa(makstavSumma);

                case 2:
                    return new Sularahamakse().Maksa(makstavSumma);

                case 0:
                    Console.WriteLine("Tellimuse vormistamine katkestati.");
                    Makse katkestatudMakse = new Makse(makstavSumma);
                    katkestatudMakse.Staatus = MakseStaatus.Katkestatud;
                    return katkestatudMakse;

                default:
                    Console.WriteLine("Tundmatu makseviis.");
                    Makse ebaõnnestunudMakse = new Makse(makstavSumma);
                    ebaõnnestunudMakse.Staatus = MakseStaatus.Ebaõnnestus;
                    return ebaõnnestunudMakse;
            }
        }
    }
}
