using System;
using System.Text;
using System.Threading.Tasks;

namespace Lab12
{
    class Program
    {
        // Спільний масив
        static int[] numbers = new int[10];
        // Об'єкт для блокування консолі (щоб текст не змішувався)
        static readonly object consoleLock = new object();

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "Лабораторна робота №12 | Литвиненко Дмитро | Варіант 8 (Модернізована)";

            // Генерація масиву
            Random rnd = Random.Shared; // Оновлено на сучасний і потокобезпечний Random.Shared
            string arrayStr = "";
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = rnd.Next(0, 26); // 0..25
                arrayStr += numbers[i] + " ";
            }

            // Вивід заголовка
            PrintHeader();
            Console.WriteLine($"Початковий масив: [ {arrayStr}]\n");

            // Створення і запуск асинхронних задач (Task замість Thread)
            Task t0 = Task.Run(() => PrintFilteredAsync());
            Task t1 = Task.Run(() => PrintSquaresAsync());

            // Очікування завершення обох задач без блокування потоку
            await Task.WhenAll(t0, t1);

            PrintFooter();
        }

        // Т0: Вивести числа > 10 і < 20
        static async Task PrintFilteredAsync()
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] > 10 && numbers[i] < 20)
                {
                    PrintColored($"[T0: {numbers[i]}] ", ConsoleColor.Cyan);
                    await Task.Delay(150); // Неблокуюча затримка для візуалізації
                }
            }
        }

        // Т1: Вивести квадрати всіх чисел
        static async Task PrintSquaresAsync()
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                int square = numbers[i] * numbers[i];
                PrintColored($"[T1: {square}] ", ConsoleColor.Yellow);
                await Task.Delay(150); // Неблокуюча затримка
            }
        }

        // Допоміжний метод для кольорового та безпечного виводу
        static void PrintColored(string message, ConsoleColor color)
        {
            lock (consoleLock)
            {
                Console.ForegroundColor = color;
                Console.Write(message);
                Console.ResetColor();
            }
        }

        static void PrintHeader()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=============================================");
            Console.WriteLine("    ЛАБОРАТОРНА РОБОТА №12");
            Console.WriteLine("    Спільний доступ до даних (Task, async/await)");
            Console.WriteLine("=============================================");
            Console.ResetColor();
        }

        static void PrintFooter()
        {
            Console.WriteLine("\n\n=============================================");
            Console.WriteLine("Робота завершена. Натисніть Enter...");
            Console.ReadLine(); // Змінив з ReadKey на ReadLine, щоб коректно працювало в терміналах
        }
    }
}