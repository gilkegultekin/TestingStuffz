using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Heaps
{
    /// <summary>
    /// 973. K Closest Points to Origin
    /// We have a list of points on the plane.  Find the K closest points to the origin (0, 0).
    /// 
    /// (Here, the distance between two points on a plane is the Euclidean distance.)
    /// 
    /// You may return the answer in any order.  The answer is guaranteed to be unique (except for the order that it is in.)
    /// 
    /// 
    /// 
    /// Example 1:
    /// 
    /// Input: points = [[1,3],[-2,2]], K = 1
    /// Output: [[-2,2]]
    /// Explanation: 
    /// The distance between (1, 3) and the origin is sqrt(10).
    /// The distance between (-2, 2) and the origin is sqrt(8).
    /// Since sqrt(8) &lt; sqrt(10), (-2, 2) is closer to the origin.
    /// We only want the closest K = 1 points from the origin, so the answer is just [[-2,2]].
    /// Example 2:
    /// 
    /// Input: points = [[3,3],[5,-1],[-2,4]], K = 2
    /// Output: [[3,3],[-2,4]]
    /// (The answer [[-2,4],[3,3]] would also be accepted.)
    /// 
    /// 
    /// Note:
    /// 
    /// 1 &lt;= K &lt;= points.length &lt;= 10000
    /// -10000 &lt; points[i][0] &lt; 10000
    /// -10000 &lt; points[i][1] &lt; 10000
    /// </summary>
    public class KClosestPointsToOriginSolution : ISolution<int[][], int[][], int>
    {
        public int[][] KClosest(int[][] points, int K)
        {
            if (points.Length <= K)
            {
                return points;
            }

            //quick sort until you get k elements in the first half, the order of the elements in the resulting array is not important
            int start = 0;
            int end = points.Length - 1;
            int pivotIndex = 0;

            while (pivotIndex != K)
            {
                pivotIndex = DivideViaPivot(points, start, end);

                if (pivotIndex > K)
                {
                    //divide left half
                    end = pivotIndex - 1;
                }
                else if (pivotIndex < K)
                {
                    //divide right half
                    start = pivotIndex + 1;
                }
            }

            return points.AsMemory(0, K).ToArray();
        }

        private int DivideViaPivot(int[][] array, int start, int end)
        {
            if (start == end)
            {
                return start;
            }

            if (end == start + 1)
            {
                if (ComparePoints(array[start], array[end]) > 0)
                {
                    Swap(array, start, end);
                    return end;
                }

                return start;
            }

            int[] pivot = array[start];
            int leftPointer = start + 1;
            int rightPointer = end;

            while (leftPointer < rightPointer)
            {
                while (leftPointer < array.Length && ComparePoints(array[leftPointer], pivot) < 1)
                {
                    leftPointer++;
                }

                while (rightPointer >= 0 && ComparePoints(array[rightPointer], pivot) > 0)
                {
                    rightPointer--;
                }

                if (leftPointer >= rightPointer)
                {
                    break;
                }

                Swap(array, leftPointer, rightPointer);
            }

            if (rightPointer > start)
            {
                Swap(array, start, rightPointer);
            }

            return rightPointer;
        }

        private void Swap(int[][] array, int first, int second)
        {
            int[] temp = array[first];
            array[first] = array[second];
            array[second] = temp;
        }

        private int ComparePoints(int[] p1, int[] p2)
        {
            return p1[0] * p1[0] + p1[1] * p1[1] - p2[0] * p2[0] - p2[1] * p2[1];
        }

        public int[][] KClosestLimitedCapacityHeap(int[][] points, int K)
        {
            List<TwoDimensionalPoint> allPoints = points.Select(a => new TwoDimensionalPoint(a[0], a[1])).ToList();

            //TODO: Use a limited max heap and only insert new element if the distance to the origin is smaller than the max in the heap.
            //This way each insert will take O(log K) in the worst case. Brute force sorting has O(N log N) complexity whereas this approach's runtime is (K/2 * log K) + (N-K)*log K ~= O(N log K)

            LimitedCapacityHeap<TwoDimensionalPoint> heap = new LimitedCapacityHeap<TwoDimensionalPoint>(allPoints, K, false);

            return heap.ToArray(p => new[] { p.X, p.Y });
        }

        public int[][] KClosestBruteForceSort(int[][] points, int K)
        {
            TwoDimensionalPoint[] euclideanPoints = points.Select(a => new TwoDimensionalPoint(a[0], a[1])).ToArray();

            Array.Sort(euclideanPoints);

            return euclideanPoints.Take(K).Select(p => new[] { p.X, p.Y }).ToArray();
        }

        public int[][] Solve(int[][] param1, int param2)
        {
            return KClosest(param1, param2);
        }

        private struct TwoDimensionalPoint : IComparable<TwoDimensionalPoint>
        {
            public int X { get; }

            public int Y { get; }

            public TwoDimensionalPoint(int x, int y)
            {
                X = x;
                Y = y;
            }

            private int CalculateDistanceToOriginSquared()
            {
                int result = (int)(Math.Pow(X, 2) + Math.Pow(Y, 2));
                return result;
            }

            public int CompareTo(TwoDimensionalPoint other)
            {
                int result = CalculateDistanceToOriginSquared().CompareTo(other.CalculateDistanceToOriginSquared());
                return result;
            }
        }
    }
}