using System;
namespace Algorithms.BST
{
    public class BSTNode<T> where T : IComparable<T>
    {
        private BSTNode<T> _parent;
        private BSTNode<T> _leftChild;
        private BSTNode<T> _rightChild;
        private T _value;

        public BSTNode(T value)
        {
            _value = value;
        }

        //Find the value in the subtree rooted at this node
        public BSTNode<T> Find(T value)
        {
            //compare value with current node
            //if equal, we re done
            //if value smaller than node value, recurse on the left child an vice versa (assuming the child exists)
            //if the necessary child does not exist, return null

            var comparisonResult = _value.CompareTo(value);

            if (comparisonResult == 0)
            {
                return this;
            }

            //the case when the node's value is bigger, have to go left
            if (comparisonResult > 0)
            {
                if (_leftChild != null)
                {
                    return _leftChild.Find(value);
                }

                return null;
            }
            else
            {
                if (_rightChild != null)
                {
                    return _rightChild.Find(value);
                }

                return null;
            }
        }

        //Find the min value in the subtree rooted at this node
        public BSTNode<T> FindMin()
        {
            //go left as much as possible
            var current = this;
            while (current._leftChild != null)
            {
                current = current._leftChild;
            }

            return current;
        }

        //finds the successor to the current node in the BST
        public BSTNode<T> NextLarger()
        {
            //if the current node has a right child, then the successor will be the min node in current node's right subtree
            //otherwise, we have to search the parent chain. The first parent whose value is bigger than the current node will be
            //the successor. If there is no such parent, then the current node has the max value and we return null

            if (_rightChild != null)
            {
                return _rightChild.FindMin();
            }

            var parent = _parent;
            while (parent != null)
            {
                if (parent._value.CompareTo(_value) > 0)
                {
                    break;
                }

                parent = parent._parent;
            }

            return parent;
        }

        //inserts a node into the subtree rooted at the current node
        //if the value already exists in the subtree, do nothing
        public void Insert(BSTNode<T> node)
        {
            //compare node's value with current node's value
            //if they are equal, return
            //if node's value is smaller, check if left child exists. 
            //If it does, recurse on the left child, otherwise insert into that spot.
            //if node's value is bigger, do the same thing with the right child.

            if(node == null)
            {
                return;    
            }

            var comparisonResult = node._value.CompareTo(_value);
            if (comparisonResult == 0)
            {
                return;
            }

            if (comparisonResult > 0) //node's value is bigger
            {
                if (_rightChild != null)
                {
                    _rightChild.Insert(node);
                    return;
                }

                //insert as the right child
                _rightChild = node;
                node._parent = this;
            }
            else //node's value is smaller
            {
                if (_leftChild != null)
                {
                    _leftChild.Insert(node);
                    return;
                }

                //insert as the left child
                _leftChild = node;
                node._parent = this;
            }
        }

        //deletes this node from the bst and returns it
        public BSTNode<T> Delete()
        {
            //if the node is a leaf node, then we can just delete it with no repercussions
            //just dont forget to update its parent also

            //if the node has no right child but a left child, then just rewire the tree such that
            //the left child takes the current node's place

            //if the node has no left child but a right child, then just rewire the tree such that 
            //the right child takes the current node's place

            //if the node has both children, we can exchange the node with its successor (which is bound to be in the right subtree)
            //if the successor is a leaf node, then we can just exchange their places and delete the node

            //if the successor is NOT a leaf node, it must have a right subtree
            //(if it has a left subtree, it can't be the successor by definition)
            //In this case, we have to rewire the tree such that the successor takes the place of the current node
            //and the right child of the successor takes the old place of the successor


            if (_leftChild != null && _rightChild != null)
            {
                var successor = NextLarger();

                var tempValue = _value;
                _value = successor._value;
                successor._value = tempValue;

                return successor.Delete();
            }

            if (_value.CompareTo(_parent._value) > 0) //the current node is right child of its parent
            {
                if (_leftChild != null)
                {
                    _parent._rightChild = _leftChild;
                    _leftChild._parent = _parent;
                }
                else if (_rightChild != null)
                {
                    _parent._rightChild = _rightChild;
                    _rightChild._parent = _parent;
                }
                else
                {
                    _parent._rightChild = null;
                }

                return this;
            }
            else
            {
                if (_leftChild != null)
                {
                    _parent._leftChild = _leftChild;
                    _leftChild._parent = _parent;
                }
                else if (_rightChild != null)
                {
                    _parent._leftChild = _rightChild;
                    _rightChild._parent = _parent;
                }
                else
                {
                    _parent._leftChild = null;
                }
                return this;
            }
        }
    }
}
