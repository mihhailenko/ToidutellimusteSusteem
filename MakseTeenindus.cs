namespace ToidutellimusteSusteem
{
    public class MakseTeenindus
    {
        public bool Maksa(double makstavSumma)
        {
            Console.WriteLine("\nVali makseviis:");
            Console.WriteLine("1. Pangakaart");
            Console.WriteLine("2. Sularaha");
            Console.WriteLine("0. Katkesta");
            Console.Write("Sisesta valik: ");

            if (!int.TryParse(Console.ReadLine(), out int valik))
            {
                Console.WriteLine("Vigane sisend! Palun sisesta number.");
                return false;
            }

            switch (valik)
            {
                case 1:
                    return MaksaKaardiga(makstavSumma);

                case 2:
                    return MaksaSularahas(makstavSumma);

                case 0:
                    Console.WriteLine("Tellimuse vormistamine katkestati.");
                    return false;

                default:
                    Console.WriteLine("Tundmatu makseviis.");
                    return false;
            }
        }

        private bool MaksaKaardiga(double makstavSumma)
        {
            Console.WriteLine($"\nKaardimakse summa: {makstavSumma:F2} €");
            Console.Write("Kinnita kaardimakse (jah/ei): ");
            string vastus = (Console.ReadLine() ?? "").ToLower();

            if (vastus == "jah" || vastus == "j")
            {
                Console.WriteLine("Kaardimakse õnnestus.");
                return true;
            }

            Console.WriteLine("Kaardimakse katkestati.");
            return false;
        }

        private bool MaksaSularahas(double makstavSumma)
        {
            Console.WriteLine($"\nSularahas tuleb tasuda: {makstavSumma:F2} €");
            Console.Write("Sisesta kliendilt saadud summa: ");
            string saadudRahaTekst = Console.ReadLine() ?? "";
            saadudRahaTekst = saadudRahaTekst.Replace(".", ",");

            if (!double.TryParse(saadudRahaTekst, out double saadudRaha))
            {
                Console.WriteLine("Vigane sisend! Palun sisesta number.");
                return false;
            }

            if (saadudRaha < makstavSumma)
            {
                double puuduvSumma = makstavSumma - saadudRaha;
                Console.WriteLine($"Raha ei piisa. Puudu on {puuduvSumma:F2} €.");
                return false;
            }

            double tagasiraha = saadudRaha - makstavSumma;
            Console.WriteLine($"Makse vastu võetud. Tagasiraha: {tagasiraha:F2} €");

            return true;
        }
    }
}
