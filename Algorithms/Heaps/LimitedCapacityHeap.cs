using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Heaps
{
    public class LimitedCapacityHeap<T> where T : IComparable<T>
    {
        private readonly IList<T> _list;

        private int _currentCapacity;

        private readonly int _maxCapacity;

        private readonly bool _isMinHeap;

        public LimitedCapacityHeap(IList<T> initialList, int maxCapacity, bool isMinHeap = true)
        {
            _isMinHeap = isMinHeap;
            _maxCapacity = maxCapacity;

            if (initialList.Count < maxCapacity)
            {
                _list = initialList;
                _currentCapacity = _list.Count;
                BuildHeap();
            }
            else
            {
                _list = initialList.Take(maxCapacity).ToList();
                _currentCapacity = maxCapacity;
                BuildHeap();

                for (int i = maxCapacity; i < initialList.Count; i++)
                {
                    Insert(initialList[i]);
                }
            }
        }

        //insert last element at the very bottom, then compare it with its parent and swap if the new element is smaller.
        //repeat until parent is smaller than new element or we have reached root. (as per the heap condition ofc)
        public void Insert(T newElement)
        {
            if (_currentCapacity < _maxCapacity)
            {
                _list[_currentCapacity++] = newElement;

                int index = _currentCapacity;
                while (index > 0 && Compare(index, index / 2))
                {
                    Swap(index, index / 2);
                    index /= 2;
                }

                return;
            }

            if (ShouldInsertNewElement(newElement))
            {
                _list[0] = newElement;
                Heapify(1);
            }
        }

        public T Peek()
        {
            return _list[0];
        }

        public T Extract()
        {
            //swap last node with root and call min heapify. Return extracted node
            var extractedElement = _list[0];
            Swap(1, _currentCapacity);
            _list[_currentCapacity - 1] = default;
            Heapify(1);
            return extractedElement;
        }

        public TResult[] ToArray<TResult>(Func<T, TResult> converter)
        {
            return _list.Select(converter).ToArray();
        }

        private bool ShouldInsertNewElement(T newElement)
        {
            if (_isMinHeap)
            {
                return newElement.CompareTo(_list[0]) > 0;
            }

            return newElement.CompareTo(_list[0]) < 0;
        }

        private void BuildHeap()
        {
            //using one-based index
            for (int i = _currentCapacity / 2; i > 0; i--)
            {
                Heapify(i);
            }
        }

        private void Heapify(int index)
        {
            //using one-based index

            //index out of range
            if (index > _currentCapacity)
            {
                return;
            }

            //leaf node
            if (index * 2 > _currentCapacity)
            {
                return;
            }

            //right child does not exist
            if (index * 2 + 1 > _currentCapacity)
            {
                if (Compare(index * 2, index))
                {
                    Swap(index, index * 2);
                    Heapify(index * 2);
                }

                return;
            }

            //both children exist
            if (!Compare(index * 2, index) &&
                !Compare(index * 2 + 1, index))
            {
                return;
            }

            if (Compare(index * 2, index * 2 + 1))
            {
                Swap(index, index * 2);
                Heapify(index * 2);
            }
            else
            {
                Swap(index, index * 2 + 1);
                Heapify(index * 2 + 1);
            }
        }

        private bool LessThan(int first, int second)
        {
            return _list[first - 1].CompareTo(_list[second - 1]) < 0;
        }

        private bool BiggerThan(int first, int second)
        {
            return _list[first - 1].CompareTo(_list[second - 1]) > 0;
        }

        private bool Compare(int first, int second)
        {
            return _isMinHeap ? LessThan(first, second) : BiggerThan(first, second);
        }

        private void Swap(int first, int second)
        {
            T temp = _list[first - 1];
            _list[first - 1] = _list[second - 1];
            _list[second - 1] = temp;
        }
    }
}