using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServicesLibrary.DataStructures
{
    public enum NodeColor
    {
        Red,
        Black
    }

    public class RedBlackNode<T> where T : IComparable<T>
    {
        public T Data { get; set; }
        public RedBlackNode<T> Left { get; set; }
        public RedBlackNode<T> Right { get; set; }
        public RedBlackNode<T> Parent { get; set; }
        public NodeColor Color { get; set; }

        public RedBlackNode(T data)
        {
            Data = data;
            Color = NodeColor.Red;
        }
    }

    public class RedBlackTree<T> where T : IComparable<T>
    {
        private RedBlackNode<T> root;

        public void Insert(T data)
        {
            var node = new RedBlackNode<T>(data);
            
            if (root == null)
            {
                root = node;
                root.Color = NodeColor.Black;
                return;
            }

            RedBlackNode<T> current = root;
            RedBlackNode<T> parent = null;

            while (current != null)
            {
                parent = current;
                if (data.CompareTo(current.Data) < 0)
                    current = current.Left;
                else
                    current = current.Right;
            }

            node.Parent = parent;
            if (data.CompareTo(parent.Data) < 0)
                parent.Left = node;
            else
                parent.Right = node;

            FixViolation(node);
        }

        private void FixViolation(RedBlackNode<T> node)
        {
            RedBlackNode<T> parent = null;
            RedBlackNode<T> grandParent = null;

            while (node != root && node.Color == NodeColor.Red && node.Parent.Color == NodeColor.Red)
            {
                parent = node.Parent;
                grandParent = parent.Parent;

                if (parent == grandParent.Left)
                {
                    var uncle = grandParent.Right;
                    if (uncle != null && uncle.Color == NodeColor.Red)
                    {
                        grandParent.Color = NodeColor.Red;
                        parent.Color = NodeColor.Black;
                        uncle.Color = NodeColor.Black;
                        node = grandParent;
                    }
                    else
                    {
                        if (node == parent.Right)
                        {
                            RotateLeft(parent);
                            node = parent;
                            parent = node.Parent;
                        }
                        RotateRight(grandParent);
                        NodeColor temp = parent.Color;
                        parent.Color = grandParent.Color;
                        grandParent.Color = temp;
                        node = parent;
                    }
                }
                else
                {
                    var uncle = grandParent.Left;
                    if (uncle != null && uncle.Color == NodeColor.Red)
                    {
                        grandParent.Color = NodeColor.Red;
                        parent.Color = NodeColor.Black;
                        uncle.Color = NodeColor.Black;
                        node = grandParent;
                    }
                    else
                    {
                        if (node == parent.Left)
                        {
                            RotateRight(parent);
                            node = parent;
                            parent = node.Parent;
                        }
                        RotateLeft(grandParent);
                        NodeColor temp = parent.Color;
                        parent.Color = grandParent.Color;
                        grandParent.Color = temp;
                        node = parent;
                    }
                }
            }
            root.Color = NodeColor.Black;
        }

        private void RotateLeft(RedBlackNode<T> node)
        {
            RedBlackNode<T> rightChild = node.Right;
            node.Right = rightChild.Left;

            if (node.Right != null)
                node.Right.Parent = node;

            rightChild.Parent = node.Parent;

            if (node.Parent == null)
                root = rightChild;
            else if (node == node.Parent.Left)
                node.Parent.Left = rightChild;
            else
                node.Parent.Right = rightChild;

            rightChild.Left = node;
            node.Parent = rightChild;
        }

        private void RotateRight(RedBlackNode<T> node)
        {
            RedBlackNode<T> leftChild = node.Left;
            node.Left = leftChild.Right;

            if (node.Left != null)
                node.Left.Parent = node;

            leftChild.Parent = node.Parent;

            if (node.Parent == null)
                root = leftChild;
            else if (node == node.Parent.Left)
                node.Parent.Left = leftChild;
            else
                node.Parent.Right = leftChild;

            leftChild.Right = node;
            node.Parent = leftChild;
        }

        public T Find(T data)
        {
            var current = root;
            while (current != null)
            {
                int comparison = data.CompareTo(current.Data);
                if (comparison == 0)
                    return current.Data;
                current = comparison < 0 ? current.Left : current.Right;
            }
            return default(T);
        }
    }
}
