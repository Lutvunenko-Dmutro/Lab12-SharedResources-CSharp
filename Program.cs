using System;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace SharedResourcesShowcase
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
            Console.Title = "Shared Resources Showcase | .NET Concurrency";

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
                    PrintColored($"[[T0: {numbers[i]}]] ", ConsoleColor.Cyan);
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
                PrintColored($"[[T1: {square}]] ", ConsoleColor.Yellow);
                await Task.Delay(150); // Неблокуюча затримка
            }
        }

        // Допоміжний метод для кольорового та безпечного виводу
        static void PrintColored(string message, ConsoleColor color)
        {
            lock (consoleLock)
            {
                // Перетворюємо ConsoleColor в колір Spectre.Console
                string colorName = color.ToString().ToLower();
                AnsiConsole.Markup($"[{colorName}]{message}[/]");
            }
        }

        static void PrintHeader()
        {
            AnsiConsole.Write(
                new FigletText("Shared Resources")
                    .LeftJustified()
                    .Color(Color.Green));

            AnsiConsole.MarkupLine("[bold green]=============================================[/]");
            AnsiConsole.MarkupLine("[bold green]    Спільний доступ до даних (Task, async/await)[/]");
            AnsiConsole.MarkupLine("[bold green]=============================================[/]\n");
        }

        static void PrintFooter()
        {
            AnsiConsole.MarkupLine("\n\n[bold green]=============================================[/]");
            AnsiConsole.MarkupLine("[bold green]Робота завершена. Натисніть Enter...[/]");
            Console.ReadLine(); // Змінив з ReadKey на ReadLine, щоб коректно працювало в терміналах
        }
    }
}