﻿using System;
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
            TwoDimensionalPoint[] euclideanPoints = points.Select(a => new TwoDimensionalPoint(a[0], a[1])).ToArray();

            Array.Sort(euclideanPoints);

            return euclideanPoints.Take(K).Select(p => new[] { p.X, p.Y }).ToArray();
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