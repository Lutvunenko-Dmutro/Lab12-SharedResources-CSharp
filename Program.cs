using System;
using System.Text;
using System.Threading.Tasks;
using SharedResourcesShowcase.UI;
using SharedResourcesShowcase.Services;

namespace SharedResourcesShowcase
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // 1. Ініціалізація залежностей (сервісів та UI)
            var consoleView = new ConsoleView();
            var arrayGenerator = new ArrayGeneratorService();
            var concurrencyProcessor = new ConcurrencyProcessingService(consoleView);

            // 2. Логіка застосунку
            consoleView.PrintHeader();

            int[] sharedData = arrayGenerator.Generate(length: 10, min: 0, max: 26);
            consoleView.PrintArray("Початковий масив", sharedData);

            await concurrencyProcessor.ProcessConcurrentlyAsync(sharedData);

            consoleView.PrintFooter();
        }
    }
}