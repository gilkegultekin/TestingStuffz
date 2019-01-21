using System;
using System.Collections.Generic;

namespace Algorithms.BST
{
    public class BST<T>
    {
        private readonly IEnumerable<T> _input;
        private readonly Func<T, T, bool> _keyComparerFunc;
        //private readonly

        public BST(IEnumerable<T> input, Func<T, T, int> keyComparerFunc)
        {
            
        }
    }
}
