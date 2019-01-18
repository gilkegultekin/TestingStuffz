using System;

namespace Algorithms.Heaps
{
	public class MaxHeap<T>
	{
		private readonly T[] _input;
		private readonly Func<T, T, bool> _keyComparerFunc;

		public MaxHeap(T[] input, Func<T, T, bool> keyComparerFunc)
		{
			_input = input;
			_keyComparerFunc = keyComparerFunc;
			BuildMaxHeap();
		}

		//Call MaxHeapify for all non-leaf nodes
		private void BuildMaxHeap()
		{
			for (int i = _input.Length/2; i >= 1; i--)
			{
				MaxHeapify(i);
			}
		}

		//Go through every node until you encounter a leaf node. Leaf nodes satisfy the heap invariant by default. Check the heap condition for the remaining nodes. Every node after the first leaf node will also be a leaf node.
		public bool IsMaxHeap()
		{
			for (int i = 0; i < _input.Length; i++)
			{
				if (IsLeafNode(i))
				{
					break;
				}

				if (!Compare(i, i*2+1) || !Compare(i, i*2+2))
				{
					return false;
				}
			}

			return true;
		}

		//Swap if necessary to satisfy the heap condition for the current node. If a swap occurred, recurse with the swapped node index, as it might violate the heap condition after the swap
		private void MaxHeapify(int virtualNodeIndex)
		{
			//the array indexes are zero based whereas the virtual indexes start from one.

			//if node's key is smaller than one of the children, exchange it with the max of the children, so that the heap invariant is satisfied for the node in question, then recurse if swap occurred

			if (Compare(virtualNodeIndex - 1, virtualNodeIndex * 2 - 1) &&
			    Compare(virtualNodeIndex - 1, virtualNodeIndex * 2))
			{
				return;
			}

			if (Compare(virtualNodeIndex * 2 - 1, virtualNodeIndex * 2))
			{
				Swap(virtualNodeIndex - 1, virtualNodeIndex * 2 - 1);
				MaxHeapify(virtualNodeIndex * 2);
			}
			else
			{
				Swap(virtualNodeIndex - 1, virtualNodeIndex * 2);
				MaxHeapify(virtualNodeIndex * 2 + 1);
			}
		}

		public T ExtractMax()
		{
			//swap last node with root and call max heapify. Return extracted node

			return default(T);
		}

		public T[] Sort()
		{
			//extract max until no nodes are left

			return new T[0];
		}

		//is the left child index out of range? if yes => leaf
		private bool IsLeafNode(int nodeIndex)
		{
			return nodeIndex * 2 + 1 > _input.Length - 1;
		}

		private void Swap(int first, int second)
		{
			var temp = _input[first];
			_input[first] = _input[second];
			_input[second] = temp;
		}

		//assumption, the first index will not be out of range, whereas the second might be
		private bool Compare(int first, int second)
		{
			return second > _input.Length - 1 || _keyComparerFunc(_input[first], _input[second]);
		}
	}
}
