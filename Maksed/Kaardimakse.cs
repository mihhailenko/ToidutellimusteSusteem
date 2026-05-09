namespace ToidutellimusteSusteem
{
    public class Kaardimakse : Makseviis
    {
        public string KaardiViimasedNumbrid { get; set; }

        public Kaardimakse() : base("Pangakaart", 0.20)
        {
            KaardiViimasedNumbrid = "";
        }

        public override Makse Maksa(double summa)
        {
            Makse makse = new Makse(summa, this);

            Console.WriteLine($"\nKaardimakse vahesumma: {summa:F2} €");
            Console.WriteLine($"Kaardimakse lisatasu: {Lisatasu:F2} €");
            Console.WriteLine($"Kaardimakse summa: {makse.MakstavSumma:F2} €");

            Console.Write("Sisesta kaardi 4 viimast numbrit: ");
            string kaardiNumber = Console.ReadLine() ?? "";

            if (kaardiNumber.Length != 4 || !int.TryParse(kaardiNumber, out int _))
            {
                Console.WriteLine("Kaardi numbri lõpp peab olema 4 numbrit.");
                makse.Staatus = MakseStaatus.Ebaõnnestus;
                return makse;
            }

            KaardiViimasedNumbrid = kaardiNumber;

            Console.Write("Kinnita kaardimakse (jah/ei): ");
            string vastus = (Console.ReadLine() ?? "").ToLower();

            if (vastus == "jah" || vastus == "j")
            {
                makse.Staatus = MakseStaatus.Õnnestus;
                Console.WriteLine($"Kaardimakse õnnestus. Kaart: **** {KaardiViimasedNumbrid}");
                return makse;
            }

            makse.Staatus = MakseStaatus.Katkestatud;
            Console.WriteLine("Kaardimakse katkestati.");

            return makse;
        }
    }
}
