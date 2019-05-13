using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
    /// <summary>
    /// 122. Best Time to Buy and Sell Stock II
    /// Say you have an array for which the ith element is the price of a given stock on day i.
    /// 
    /// Design an algorithm to find the maximum profit. You may complete as many transactions as you like (i.e., buy one and sell one share of the stock multiple times).
    /// 
    /// Note: You may not engage in multiple transactions at the same time (i.e., you must sell the stock before you buy again).
    /// 
    /// Example 1:
    /// 
    /// Input: [7,1,5,3,6,4]
    /// Output: 7
    /// Explanation: Buy on day 2 (price = 1) and sell on day 3 (price = 5), profit = 5-1 = 4.
    /// Then buy on day 4 (price = 3) and sell on day 5 (price = 6), profit = 6-3 = 3.
    /// Example 2:
    /// 
    /// Input: [1,2,3,4,5]
    /// Output: 4
    /// Explanation: Buy on day 1 (price = 1) and sell on day 5 (price = 5), profit = 5-1 = 4.
    /// Note that you cannot buy on day 1, buy on day 2 and sell them later, as you are
    /// engaging multiple transactions at the same time. You must sell before buying again.
    /// Example 3:
    /// 
    /// Input: [7,6,4,3,1]
    /// Output: 0
    /// Explanation: In this case, no transaction is done, i.e. max profit = 0.
    /// </summary>
    public class MaxProfitSolution : ISolution<int, int[]>
    {
        public int MaxProfit(int[] prices)
        {
            Node root = new Node(0, false);
            IList<Node> currentLayer = new List<Node> { root };

            foreach (int price in prices)
            {
                IList<Node> nextLayer = new List<Node>();
                //If you don't have a bond, buy or wait
                //If you do have a bond, sell or wait
                //In the same layer, prune the nodes which are worse than others (Pick the decision chain which results in max profit, keeping in mind future possibilities)
                //If node has asset, compare it with other nodes with asset. Pick the one with max profit in each layer
                //If node does not have asset, compare it with other nodes which do not have an asset. Also the best node without an asset should have a better profit than the best node with an asset, otherwise prune it as well.
                //So at each layer, max of 2 nodes will survive and the others will be pruned.
                foreach (Node node in currentLayer)
                {
                    if (node.HasStock)
                    {
                        //sell
                        Node sellNode = new Node(node.CurrentProfit + price, false);
                        nextLayer.Add(sellNode);

                        //wait
                        nextLayer.Add(node);
                    }
                    else
                    {
                        //buy
                        Node buyNode = new Node(node.CurrentProfit - price, true);
                        nextLayer.Add(buyNode);

                        //wait
                        nextLayer.Add(node);
                    }
                }

                currentLayer = new List<Node>();
                Node bestNodeWithAsset = nextLayer.Where(n => n.HasStock).Max();
                currentLayer.Add(bestNodeWithAsset);
                Node bestNodeWithoutAsset = nextLayer.Where(n => !n.HasStock).Max();
                if (bestNodeWithAsset.CompareTo(bestNodeWithoutAsset) == -1)
                {
                    currentLayer.Add(bestNodeWithoutAsset);
                }
            }

            return currentLayer.Max(n => n.CurrentProfit);
        }

        public int MaxProfit2(int[] prices)
        {
            if (prices.Length <= 1)
            {
                return 0;
            }

            int bestProfitWithAsset = -prices[0];
            int bestProfitWithoutAsset = 0;

            for (int i = 1; i < prices.Length; i++)
            {
                bestProfitWithoutAsset = Math.Max(bestProfitWithAsset + prices[i], bestProfitWithoutAsset);
                bestProfitWithAsset = Math.Max(bestProfitWithAsset, bestProfitWithoutAsset - prices[i]);
            }

            return Math.Max(bestProfitWithoutAsset, bestProfitWithAsset);
        }

        public int Solve(int[] param)
        {
            return MaxProfit2(param);
        }

        private class Node : IComparable<Node>
        {
            public int CurrentProfit { get; }

            public bool HasStock { get; }

            public Node(int currentProfit, bool hasStock)
            {
                CurrentProfit = currentProfit;
                HasStock = hasStock;
            }

            public int CompareTo(Node other)
            {
                if (ReferenceEquals(this, other)) return 0;
                if (ReferenceEquals(null, other)) return 1;
                return CurrentProfit.CompareTo(other.CurrentProfit);
            }
        }
    }
}