using System;

namespace SharedResourcesShowcase.Services
{
    public class ArrayGeneratorService
    {
        public int[] Generate(int length, int min, int max)
        {
            int[] array = new int[length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = Random.Shared.Next(min, max);
            }
            return array;
        }
    }
}
