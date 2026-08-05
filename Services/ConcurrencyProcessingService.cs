using System;
using System.Threading.Tasks;
using SharedResourcesShowcase.UI;

namespace SharedResourcesShowcase.Services
{
    public class ConcurrencyProcessingService
    {
        private readonly ConsoleView _view;

        public ConcurrencyProcessingService(ConsoleView view)
        {
            _view = view;
        }

        public async Task ProcessConcurrentlyAsync(int[] data)
        {
            // Створення і запуск асинхронних задач (Task замість Thread)
            Task t0 = Task.Run(() => PrintFilteredAsync(data));
            Task t1 = Task.Run(() => PrintSquaresAsync(data));

            // Очікування завершення обох задач без блокування потоку
            await Task.WhenAll(t0, t1);
        }

        private async Task PrintFilteredAsync(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > 10 && array[i] < 20)
                {
                    _view.PrintColored($"[T0: {array[i]}] ", ConsoleColor.Cyan);
                    await Task.Delay(150); // Неблокуюча затримка для візуалізації
                }
            }
        }

        private async Task PrintSquaresAsync(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int square = array[i] * array[i];
                _view.PrintColored($"[T1: {square}] ", ConsoleColor.Yellow);
                await Task.Delay(150); // Неблокуюча затримка
            }
        }
    }
}
