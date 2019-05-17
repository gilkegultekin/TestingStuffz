using System;
using System.Linq;

namespace Algorithms.Heaps
{
    //TODO: analyze and refactor
    public class LimitedMinHeap<T> where T : IComparable<T>
    {
        private readonly T[] _array;

        private readonly T[] _initialArray;

        private readonly int _limit;

        private int _currentArrayLength;

        public LimitedMinHeap(T[] array, int limit)
        {
            _initialArray = array;
            Array.Sort(_initialArray);
            _limit = limit;

            if (array.Length < limit)
            {
                _array = new T[limit];
                _initialArray.CopyTo(_array, 0);
                _currentArrayLength = _initialArray.Length;
            }
            else
            {
                _array = _initialArray.Reverse().Take(limit).ToArray();
                _currentArrayLength = limit;
            }

            BuildMinHeap();
        }

        //Call MaxHeapify for all non-leaf nodes
        private void BuildMinHeap()
        {
            for (int i = _currentArrayLength / 2; i >= 1; i--)
            {
                MinHeapify(_array, i, _currentArrayLength);
            }
        }

        public void MinHeapify(T[] array, int virtualNodeIndex, int arrayLength)
        {
            //the array indexes are zero based whereas the virtual indexes start from one.
            if (virtualNodeIndex > arrayLength)
            {
                return;
            }

            //leaf node
            if (virtualNodeIndex * 2 - 1 >= arrayLength)
            {
                return;
            }

            //right child does not exist
            if (virtualNodeIndex * 2 >= arrayLength)
            {
                if (Compare(array, virtualNodeIndex * 2 - 1, virtualNodeIndex - 1))
                {
                    Swap(array, virtualNodeIndex - 1, virtualNodeIndex * 2 - 1);
                    MinHeapify(array, virtualNodeIndex * 2, arrayLength);
                }

                return;
            }

            //if node's key is bigger than one of the children, exchange it with the min of the children, so that the heap invariant is satisfied for the node in question, then recurse if swap occurred
            if (!Compare(array, virtualNodeIndex * 2 - 1, virtualNodeIndex - 1) &&
                !Compare(array, virtualNodeIndex * 2, virtualNodeIndex - 1))
            {
                return;
            }

            if (Compare(array, virtualNodeIndex * 2 - 1, virtualNodeIndex * 2))
            {
                Swap(array, virtualNodeIndex - 1, virtualNodeIndex * 2 - 1);
                MinHeapify(array, virtualNodeIndex * 2, arrayLength);
            }
            else
            {
                Swap(array, virtualNodeIndex - 1, virtualNodeIndex * 2);
                MinHeapify(array, virtualNodeIndex * 2 + 1, arrayLength);
            }
        }

        public T PeekMin()
        {
            return _array[0];
        }

        public T ExtractMin()
        {
            return ExtractMin(_array, _currentArrayLength);
        }

        private T ExtractMin(T[] array, int arrayLength)
        {
            //swap last node with root and call min heapify. Return extracted node
            var extractedElement = array[0];
            Swap(array, 0, arrayLength - 1);
            array[arrayLength - 1] = default;
            MinHeapify(array, 1, --_currentArrayLength);
            return extractedElement;
        }

        //insert last element at the very bottom, then compare it with its parent and swap if the new element is smaller.
        //repeat until parent is smaller than new element or we have reached root. (as per the heap condition ofc)
        public void Insert(T newElement)
        {
            if (_currentArrayLength < _limit)
            {
                _array[_currentArrayLength] = newElement;

                int index = _currentArrayLength;
                while (index > 0 && Compare(index, (index-1)/2))
                {
                    Swap(index, (index - 1) / 2);
                    index = (index - 1) / 2;
                }

                _currentArrayLength++;
                return;
            }

            if (_array[0].CompareTo(newElement) >= 0)
            {
                return;
            }

            _array[0] = newElement;
            MinHeapify(_array, 1, _currentArrayLength);
        }

        private void Swap(int first, int second)
        {
            Swap(_array, first, second);
        }

        private void Swap(T[] array, int first, int second)
        {
            if (first < 0 || second < 0)
            {
                throw new IndexOutOfRangeException("Indexes must be nonnegative");
            }

            if (first >= array.Length || second >= array.Length)
            {
                throw new IndexOutOfRangeException();
            }

            var temp = array[first];
            array[first] = array[second];
            array[second] = temp;
        }

        private bool Compare(int first, int second)
        {
            return Compare(_array, first, second);
        }

        private bool Compare(T[] array, int first, int second)
        {
            if (first < 0 || second < 0)
            {
                throw new IndexOutOfRangeException("Indexes must be nonnegative");
            }

            if (first >= array.Length || second >= array.Length)
            {
                throw new IndexOutOfRangeException();
            }

            return array[first].CompareTo(array[second]) < 0;
        }
    }
}