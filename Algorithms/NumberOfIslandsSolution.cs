using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
    /// <summary>
    /// Given a 2d grid map of '1's (land) and '0's (water), count the number of islands. An island is surrounded by water and is formed by connecting adjacent lands horizontally or vertically.
    /// You may assume all four edges of the grid are all surrounded by water.
    /// 
    /// Example 1:
    /// 
    /// Input:
    /// 11110
    /// 11010
    /// 11000
    /// 00000
    /// 
    /// Output: 1
    /// Example 2:
    /// 
    /// Input:
    /// 11000
    /// 11000
    /// 00100
    /// 00011
    /// 
    /// Output: 3
    /// </summary>
    public class NumberOfIslandsSolution: ISolution<int, char[][]>
    {
        public int NumIslands(char[][] grid)
        {
            int islandCount = 0;
            int maxIslandId = 0;
            Dictionary<ValueTuple<int, int>, int> islandParts =  new Dictionary<(int, int), int>();

            for (int i = 0; i < grid.Length; i++)
            {
                char[] row = grid[i];
                //process rows
                for (int j = 0; j < row.Length; j++)
                {
                    if (row[j] != '1')
                    {
                        continue;
                    }

                    bool connectedFromLeft = false;
                    bool connectedFromTop = false;
                    int connectedIslandIdFromTop = -1;
                    int connectedIslandIdFromLeft = -1;

                    //determine if its part of an existing island or the start of one
                    //check the top and the left neighbor. See if they are in islandParts array
                    if (i > 0)
                    {
                        connectedFromTop = islandParts.TryGetValue((i - 1, j), out connectedIslandIdFromTop);
                    }

                    if (j > 0)
                    {
                        connectedFromLeft = islandParts.TryGetValue((i, j - 1), out connectedIslandIdFromLeft);
                    }

                    //if it has no connection to an existing island, create a new island
                    if (!connectedFromTop && !connectedFromLeft)
                    {
                        islandCount++;
                        maxIslandId++;
                        islandParts.Add((i,j), maxIslandId);
                    }
                    //if it has connection to two island parts (which could belong to the same island)
                    else if (connectedFromTop && connectedFromLeft)
                    {
                        islandParts.Add((i, j), connectedIslandIdFromTop);
                        //check if the two island parts match. If they don't match, unite them
                        if (connectedIslandIdFromTop != connectedIslandIdFromLeft)
                        {
                            //the island from the left should belong to the island from the top
                            //have to create a new collection for the soon to be updated islands, since I cant modify a collection while enumerating it
                            foreach (KeyValuePair<(int, int), int> islandPart in islandParts.Where(p => p.Value == connectedIslandIdFromLeft).ToList())
                            {
                                islandParts[islandPart.Key] = connectedIslandIdFromTop;
                            }

                            islandCount--;
                        }
                    }
                    else
                    {
                        islandParts.Add((i, j), connectedFromTop ? connectedIslandIdFromTop : connectedIslandIdFromLeft);
                    }
                }
            }

            return islandCount;
        }

        public int Solve(char[][] param)
        {
            return NumIslands(param);
        }
    }
}