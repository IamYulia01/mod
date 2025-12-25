using System.Diagnostics;
using static mod.Program;

namespace mod
{
    public class Program
    {
        public class Money
        {
            public int beginSum { get; set; }
            public double currentSum { get; set; }
            public List<double> plusSum { get; set; }
            public double endSum { get; set; }
            public double procent { get; set; }
            public int age { get; set; }
            public int type { get; set; }
        }
        public Money money { get; set; }
        public bool Menu()
        {
            Console.WriteLine("Выберите, что вы хотите сделать:");
            Console.WriteLine("\t1 - Рассчитать прирост за каждый год");
            Console.WriteLine("\t2 - Рассчитать конечную сумму с приростом");
            Console.WriteLine("\t0 - Выход\n");
            string choice = Console.ReadLine();
            bool vozvrat = true;
            switch (choice)
            {
                case "1":
                    plusEveryYear();
                    vozvrat = true;
                    break;
                case "2":
                    EndSum();
                    vozvrat = true;
                    break;
                case "0":
                    Console.WriteLine("До свидания!");
                    vozvrat = false;
                    break;
                defoult:
                    Console.WriteLine("Введите номер действия из списка!");
                    vozvrat = true;
                    break;
            }
            return vozvrat;

        }
        public void plusEveryYear()
        {
            int n = 0;
            switch (money.type)
            {
                case 1:
                    n = 12;
                    break;
                case 2:
                    n = 4;
                    break;
                case 3:
                    n = 1;
                    break;
            }
            money.currentSum = money.beginSum;
            Console.WriteLine($"Начальная сумма: {money.beginSum} руб \n");
            for (int i = 0; i < money.age; i++)
            {
                double cur = money.beginSum * Math.Pow((1 + (money.procent * 0.01) / n), (12 * (i + 1)));
                double plus = cur - money.currentSum;
                money.plusSum.Add(plus);
                money.currentSum = cur;
                Console.WriteLine($"| {i + 1}\t| {money.currentSum:N2} \t| {plus:N2}\t|\n");

            }
            money.endSum = money.currentSum;
            Menu();
        }
        public void EndSum()
        {
            int n = 0;
            switch (money.type)
            {
                case 1:
                    n = 12;
                    break;
                case 2:
                    n = 4;
                    break;
                case 3:
                    n = 1;
                    break;
            }
            money.endSum = money.beginSum * Math.Pow((1 + (money.procent * 0.01) / n), (12 * money.age));
            Console.WriteLine($"\nКонечная сумма: {money.endSum:N2} руб.");
            Menu();
        }
        public static void Main(string[] args)
        {
            Program program = new Program();
            program.Run();
        }
        public void Run()
        {
            money = new Money();
            money.plusSum = new List<double>();
            bool valid = true;
            do
            {
                Console.WriteLine("Введите начальный капитал: ");
                string begin = Console.ReadLine();
                if (string.IsNullOrEmpty(begin) || !int.TryParse(begin, out int beginSum))
                {
                    valid = false;
                    Console.WriteLine("Данные введены неправильно!");
                }
                else
                {
                    valid = true;
                    money.beginSum = beginSum;
                }
            } while (!valid);
            do
            {
                Console.WriteLine("Введите процентную ставку (%): ");
                string proc = Console.ReadLine();
                if (string.IsNullOrEmpty(proc) ||
                    !float.TryParse(proc, out float procent) ||
                    procent > 100 || procent < 1)
                {
                    valid = false;
                    Console.WriteLine("Данные введены неправильно!");
                }
                else
                {
                    valid = true;
                    money.procent = procent;
                }
            } while (!valid);
            do
            {
                Console.WriteLine("Введите срок (лет): ");
                string srok = Console.ReadLine();
                if (string.IsNullOrEmpty(srok) ||
                    !int.TryParse(srok, out int age) ||
                    age > 50 || age < 1)
                {
                    valid = false;
                    Console.WriteLine("Данные введены неправильно!");
                }
                else
                {
                    valid = true;
                    money.age = age;
                }
            } while (!valid);
            do
            {
                Console.WriteLine("Введите тип капитализации (1-ежемес, 2-ежекварт, 3-ежегодно): ");
                string kap = Console.ReadLine();
                if (string.IsNullOrEmpty(kap) ||
                    !int.TryParse(kap, out int num)
                    || num > 3 || num < 1)
                {
                    valid = false;
                    Console.WriteLine("Данные введены неправильно!");
                }
                else
                {
                    valid = true;
                    money.type = num;
                }
            } while (!valid);
            do { Console.WriteLine(); } while (!Menu());
        }
    }
}




