using System;
using System.Collections.Generic;

namespace Algorithms.Heaps
{
    public class MaxHeap<T>
    {
        private readonly List<T> _input;
        private readonly Func<T, T, bool> _keyComparerFunc;

        public MaxHeap(List<T> input, Func<T, T, bool> keyComparerFunc)
        {
            _input = input;
            _keyComparerFunc = keyComparerFunc;
            BuildMaxHeap();
        }

        //Call MaxHeapify for all non-leaf nodes
        private void BuildMaxHeap()
        {
            for (int i = _input.Count / 2; i >= 1; i--)
            {
                MaxHeapify(i);
            }
        }

        public void WriteToConsole()
        {
            _input.WriteToConsole(e => e);
        }

        //Go through every node until you encounter a leaf node. Leaf nodes satisfy the heap invariant by default. Check the heap condition for the remaining nodes. Every node after the first leaf node will also be a leaf node.
        public bool IsMaxHeap()
        {
            for (int i = 0; i < _input.Count; i++)
            {
                if (IsLeafNode(i))
                {
                    break;
                }

                if (!Compare(i, i * 2 + 1) || !Compare(i, i * 2 + 2))
                {
                    return false;
                }
            }

            return true;
        }

        //Swap if necessary to satisfy the heap condition for the current node. If a swap occurred, recurse with the swapped node index, as it might violate the heap condition after the swap
        public void MaxHeapify(int virtualNodeIndex)
        {
            MaxHeapify(_input, virtualNodeIndex);
        }

        public void MaxHeapify(List<T> list, int virtualNodeIndex)
        {
            //the array indexes are zero based whereas the virtual indexes start from one.

            //if node's key is smaller than one of the children, exchange it with the max of the children, so that the heap invariant is satisfied for the node in question, then recurse if swap occurred

            if (Compare(list, virtualNodeIndex - 1, virtualNodeIndex * 2 - 1) &&
                Compare(list, virtualNodeIndex - 1, virtualNodeIndex * 2))
            {
                return;
            }

            if (Compare(list, virtualNodeIndex * 2 - 1, virtualNodeIndex * 2))
            {
                Swap(list, virtualNodeIndex - 1, virtualNodeIndex * 2 - 1);
                MaxHeapify(list, virtualNodeIndex * 2);
            }
            else
            {
                Swap(list, virtualNodeIndex - 1, virtualNodeIndex * 2);
                MaxHeapify(list, virtualNodeIndex * 2 + 1);
            }
        }

        public T ExtractMax()
        {
            return ExtractMax(_input);
        }

        private T ExtractMax(List<T> list)
        {
            //swap last node with root and call max heapify. Return extracted node
            var extractedElement = list[0];
            Swap(list, 0, list.Count - 1);
            list.RemoveAt(list.Count - 1);
            MaxHeapify(list, 1);
            return extractedElement;
        }

        public List<T> Sort()
        {
            var sortedArray = new T[_input.Count];

            //have to work on a copy of the input list
            var copyArray = new T[_input.Count];
            _input.CopyTo(copyArray); //is this a deep copy? Since I dont mutate any of the list's elements, a shallow copy should do
            //nevertheless TODO: have to check if list.CopyTo is shallow or deep
            var copyList = new List<T>(copyArray);

            //extract max until no nodes are left
            var counter = copyList.Count - 1;
            while (counter >= 0)
            {
                var extracted = ExtractMax(copyList);
                sortedArray[counter--] = extracted;
            }

            return new List<T>(sortedArray);
        }

        //insert last element at the very bottom, then compare it with its parent and swap if the new element is bigger.
        //repeat until parent is bigger than new element or we have reached root. (as per the heap condition ofc)
        public void Insert(T newElement)
        {
            _input.Add(newElement);
            var virtualIndex = _input.Count;
            while (virtualIndex / 2 > 0 && Compare(virtualIndex - 1, virtualIndex / 2 - 1))
            {
                Swap(virtualIndex - 1, virtualIndex / 2 - 1);
                virtualIndex /= 2;
            }
        }

        //swap the element to be deleted with the last element. Remove the last element.
        //call maxheapify on the swapped element as it might be violating the heap condition.
        //since nothing else changed, we dont have to call maxheapify for any other element.
        public T Delete(int index)
        {
            Swap(index, _input.Count - 1);

            var deleted = _input[_input.Count - 1];
            _input.RemoveAt(_input.Count - 1);

            MaxHeapify(index+1); //maxheapify expects virtual index
            return deleted;
        }

        //is the left child index out of range? if yes => leaf
        private bool IsLeafNode(int nodeIndex)
        {
            return nodeIndex * 2 + 1 > _input.Count - 1;
        }

        private void Swap(int first, int second)
        {
            Swap(_input, first, second);
        }

        private void Swap(List<T> list, int first, int second)
        {
            var temp = list[first];
            list[first] = list[second];
            list[second] = temp;
        }

        //assumption, the first index will not be out of range, whereas the second might be
        private bool Compare(int first, int second)
        {
            return Compare(_input, first, second);
        }

        private bool Compare(List<T> list, int first, int second)
        {
            if (first < 0 || second < 0)
            {
                throw new Exception("Indexes must be nonnegative");
            }
            return second > list.Count - 1 || _keyComparerFunc(list[first], list[second]);
        }
    }
}
