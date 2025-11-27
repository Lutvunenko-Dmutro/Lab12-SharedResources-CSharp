using System;
using System.Text;
using System.Threading;

namespace Lab12
{
    class Program
    {
        // Спільний масив
        static int[] numbers = new int[10];
        // Об'єкт для блокування консолі (щоб текст не змішувався)
        static readonly object consoleLock = new object();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "Лабораторна робота №12 | Литвиненко Дмитро | Варіант 8";

            // Генерація масиву
            Random rnd = new Random();
            string arrayStr = "";
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = rnd.Next(0, 26); // 0..25
                arrayStr += numbers[i] + " ";
            }

            // Вивід заголовка
            PrintHeader();
            Console.WriteLine($"Початковий масив: [ {arrayStr}]\n");

            // Створення потоків
            Thread t0 = new Thread(PrintFiltered);
            Thread t1 = new Thread(PrintSquares);

            t0.Start();
            t1.Start();

            t0.Join();
            t1.Join();

            PrintFooter();
        }

        // Т0: Вивести числа > 10 і < 20
        static void PrintFiltered()
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] > 10 && numbers[i] < 20)
                {
                    PrintColored($"[T0: {numbers[i]}] ", ConsoleColor.Cyan);
                    Thread.Sleep(150); // Затримка для візуалізації
                }
            }
        }

        // Т1: Вивести квадрати всіх чисел
        static void PrintSquares()
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                int square = numbers[i] * numbers[i];
                PrintColored($"[T1: {square}] ", ConsoleColor.Yellow);
                Thread.Sleep(150);
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
            Console.WriteLine("    Спільний доступ до даних (масивів)");
            Console.WriteLine("=============================================");
            Console.ResetColor();
        }

        static void PrintFooter()
        {
            Console.WriteLine("\n\n=============================================");
            Console.WriteLine("Робота завершена. Натисніть Enter...");
            Console.ReadKey();
        }
    }
}