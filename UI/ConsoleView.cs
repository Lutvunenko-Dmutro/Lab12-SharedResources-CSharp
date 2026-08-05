using System;
using Spectre.Console;

namespace SharedResourcesShowcase.UI
{
    public class ConsoleView
    {
        private readonly object _consoleLock = new object();

        public void PrintHeader()
        {
            Console.Title = "Shared Resources Showcase | .NET Concurrency";

            AnsiConsole.Write(
                new FigletText("Shared Resources")
                    .LeftJustified()
                    .Color(Color.Green));

            AnsiConsole.MarkupLine("[bold green]=============================================[/]");
            AnsiConsole.MarkupLine("[bold green]    Спільний доступ до даних (Task, async/await)[/]");
            AnsiConsole.MarkupLine("[bold green]=============================================[/]\n");
        }

        public void PrintArray(string prefix, int[] array)
        {
            string arrayStr = string.Join(" ", array);
            Console.WriteLine($"{prefix}: [ {arrayStr} ]\n");
        }

        public void PrintColored(string message, ConsoleColor color)
        {
            lock (_consoleLock)
            {
                // Перетворюємо ConsoleColor в колір Spectre.Console
                string colorName = color.ToString().ToLower();
                // Екрануємо квадратні дужки для Spectre.Console
                string escapedMessage = Markup.Escape(message);
                AnsiConsole.Markup($"[{colorName}]{escapedMessage}[/]");
            }
        }

        public void PrintFooter()
        {
            AnsiConsole.MarkupLine("\n\n[bold green]=============================================[/]");
            AnsiConsole.MarkupLine("[bold green]Робота завершена. Натисніть Enter...[/]");
            Console.ReadLine();
        }
    }
}
