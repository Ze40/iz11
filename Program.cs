namespace iz11
{
    class Program
    {


        static void Main()
        {
            while (true) {
                Console.WriteLine("Введите номер задания или q для выхода");
                string key = Console.ReadLine();
                if (key == null || key == "q" || key == "Q") break;
                switch (key) {
                    case "1":
                        Task1 task1 = new Task1();
                        break;
                    case "2":
                        Task2 task2 = new Task2();
                        break;
                    case "3":
                        Task3 task3 = new Task3();
                        break;
                    case "4":
                        Task4 task4 = new Task4();
                        break;
                    case "5":
                        break;
                    case "6":
                        Task6 task6 = new Task6();
                        break;
                    case "7":
                        Task7 task7 = new Task7();
                        break;
                    default:
                        Console.WriteLine("Нет такого задания");
                        break;
                }
            }
        }
    }
}