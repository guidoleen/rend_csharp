using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD_Enviornment.Types
{
    internal class Node<T>
    {
        internal Node<T>? Next { get; set; }
        internal Node<T>? Prev { get; set; }

        internal Node(T value)
        {
            this.Value = value;
        }

        public Node(Node<T> left, Node<T> right)
        {
            Next = left;
            Prev = right;
        }

        internal T Value 
        { 
            get; 
            set; 
        }
    }
}
