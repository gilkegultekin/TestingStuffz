using System;

namespace Algorithms.Tests.Helpers
{
    public static class ArrayHelpers
    {
        public static bool IsSorted<T>(T[] array) where T : IComparable<T>
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (i + 1 < array.Length && array[i].CompareTo(array[i+1]) > 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static int[] GenerateIntegerArray(int min, int max, int count)
        {
            int[] array = new int[count];

            Random rand = new Random(Guid.NewGuid().GetHashCode());

            for (int i = 0; i < count; i++)
            {
                array[i] = rand.Next(min, max);
            }

            return array;
        }
    }
}