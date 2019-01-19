using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
    public static class PeakFinder
    {
        public static PeakFinderResult1D Find1DPeak(IList<int> listToSearch, int startIndex, int endIndex)
        {
            var result = new PeakFinderResult1D();

            if (startIndex > endIndex)
            {
                throw new IndexOutOfRangeException("EndIndex cannot be smaller than StartIndex!");
            }
            if (startIndex == endIndex)
            {
                result.Location = startIndex;
                result.Value = listToSearch[startIndex];
                return result;
            }
            if (endIndex - startIndex == 1)
            {
                if (listToSearch[startIndex] > listToSearch[endIndex])
                {
                    result.Location = startIndex;
                    result.Value = listToSearch[startIndex];
                }
                else
                {
                    result.Location = endIndex;
                    result.Value = listToSearch[endIndex];
                }

                return result;
            }

            var middleIndex = (startIndex + endIndex) / 2;
            var middleValue = listToSearch[middleIndex];
            Console.WriteLine($"Processing {middleIndex}-{middleValue}");
            if (middleValue >= listToSearch[middleIndex - 1] && middleValue >= listToSearch[middleIndex + 1])
            {
                result.Location = middleIndex;
                result.Value = middleValue;
                return result;
            }
            return middleValue < listToSearch[middleIndex - 1] ? Find1DPeak(listToSearch, startIndex, middleIndex - 1) : Find1DPeak(listToSearch, middleIndex + 1, endIndex);
        }

        public static PeakFinderResult2D Find2DPeak(Matrix matrixToSearch, int startColumnIndex, int endColumnIndex)
        {
            var result = new PeakFinderResult2D();

            if (startColumnIndex > endColumnIndex)
            {
                throw new IndexOutOfRangeException("EndIndex cannot be smaller than StartIndex!");
            }
            if (startColumnIndex == endColumnIndex)
            {
                var maxWithLocation = matrixToSearch[startColumnIndex].MaxWithLocation();
                result.Location = new Coordinate {X = maxWithLocation.Item1, Y = startColumnIndex};
                result.Value = maxWithLocation.Item2;
                return result;
            }

            return result;
        }

        public static PeakFinderResult1D Run1DSample()
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            var listSize = random.Next(10, 20);
            Console.WriteLine($"List size: {listSize}");
            var list = IntegerListGenerator.Generate1DList(listSize, 0, 100);
            return Find1DPeak(list, 0, listSize - 1);
        }

        public static PeakFinderResult2D Run2DSample()
        {
            var listSize = 5;
            var list = IntegerListGenerator.Generate1DList(listSize, 0, 100);
            var matrix = new Matrix
            {
                list
            };
            return Find2DPeak(matrix, 0, 1);
        }
    }

	public static class EnumerableExtensions
	{
        public static void WriteToConsole<TInput, TKey>(this IEnumerable<TInput> array, Func<TInput, TKey> keySelectorFunc)
		{
			foreach (var element in array)
			{
				Console.Write($"{keySelectorFunc(element)} ");
			}
            Console.WriteLine();
            Console.WriteLine("----------------------");
		}
	}

    public static class ListExtensions
    {
        public static Tuple<int, int> MaxWithLocation(this List<int> list)
        {
            var location = 0;
            var maxValue = -1;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] > maxValue)
                {
                    location = i;
                    maxValue = list[i];
                }
            }

            return new Tuple<int, int>(location, maxValue);
        }
    }

    public static class IntegerListGenerator
    {
        public static List<int> Generate1DList(int listSize, int minValue, int maxValue)
        {
            var list = new List<int>(listSize);
            var random = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < listSize; i++)
            {
                list.Add(random.Next(minValue, maxValue));
                Console.Write($"{list[i]} - ");
            }

            Console.WriteLine();
            return list;
        }

		//brute force
	    public static List<int> Generate1DListWithDistinctValues(int listSize, int minValue, int maxValue)
	    {
		    var list = new List<int>(listSize);
		    var random = new Random(Guid.NewGuid().GetHashCode());
		    for (int i = 0; i < listSize; i++)
		    {
			    int next;
			    do
			    {
				    next = random.Next(minValue, maxValue);

			    } while (list.Contains(next));
				
			    list.Add(next);
			    //Console.Write($"{list[i]} - ");
		    }

		    //Console.WriteLine();
		    return list;
	    }

		public static Matrix GenerateSquareMatrix(int matrixSize, int minValue, int maxValue)
        {
            var matrix = new Matrix();
            for (int i = 0; i < matrixSize; i++)
            {
                var curList = Generate1DList(matrixSize, minValue, maxValue);
                matrix.Add(curList);
            }

            return matrix;
        }
    }

    public class Matrix : List<List<int>>
    {

    }

    public struct Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return $"{X}-{Y}";
        }
    }

    public struct PeakFinderResult2D
    {
        public Coordinate Location { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return $"{Location}-{Value}";
        }
    }

    public struct PeakFinderResult1D
    {
        public int Location { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return $"{Location}-{Value}";
        }
    }
}