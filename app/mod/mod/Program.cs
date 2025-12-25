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

        // Выделяем чистую логику расчета в отдельный метод
        public double CalculateEndSum()
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
            return money.beginSum * Math.Pow((1 + (money.procent * 0.01) / n), (n * money.age));
        }

        // Старый метод EndSum() оставляем только для UI
        public void EndSum()
        {
            money.endSum = CalculateEndSum();
            Console.WriteLine($"\nКонечная сумма: {money.endSum:N2} руб.");
            Menu();
        }

        // Выделяем логику расчета по годам
        public List<(int year, double currentSum, double plus)> CalculateYearlyGrowth()
        {
            var result = new List<(int year, double currentSum, double plus)>();
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
            for (int i = 0; i < money.age; i++)
            {
                double cur = money.beginSum * Math.Pow((1 + (money.procent * 0.01) / n), (n * (i + 1)));
                double plus = cur - money.currentSum;
                money.plusSum.Add(plus);
                money.currentSum = cur;
                result.Add((i + 1, money.currentSum, plus));
            }
            money.endSum = money.currentSum;

            return result;
        }

        // Старый метод plusEveryYear() оставляем только для UI
        public void plusEveryYear()
        {
            var yearlyData = CalculateYearlyGrowth();

            Console.WriteLine($"Начальная сумма: {money.beginSum} руб \n");
            foreach (var data in yearlyData)
            {
                Console.WriteLine($"| {data.year}\t| {data.currentSum:N2} \t| {data.plus:N2}\t|\n");
            }
            Menu();
        }

        // Остальной код без изменений...
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
                default:
                    Console.WriteLine("Введите номер действия из списка!");
                    vozvrat = true;
                    break;
            }
            return vozvrat;
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