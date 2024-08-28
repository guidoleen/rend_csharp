using RD_Enviornment.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD_Enviornment.Processor
{
    internal class LinkedListProcessor<T> : IEnumerable<T>
    {
        private List<Node<object>>? _convertedLinkedList;
        private Node<object>? _tail;
        internal void Add(Node<object> node)
        {
            if (this._tail == node) return;

            if (this._tail == null) // Head node
                this._tail = node;

            else if(this._tail.Next == null)
            {
                var current = this._tail;
                current.Next = node;
                node.Prev = current;
            }
            this._tail = node;
        }
        internal List<Node<object>>? ToList()
        {
            _convertedLinkedList = new List<Node<object>>();

            this.GetNodes(this._tail, (newNode) => {
                _convertedLinkedList?.Add(newNode);
            });

            return this._convertedLinkedList;
        }

        private Node<T>? GetNodes(Node<object>? node, Action <Node<object>>? callBack)
        {
            if(node != null)
            {
                if(callBack != null)
                    callBack(node);
                return this.GetNodes(node.Prev, callBack);
            }
            return null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (this._convertedLinkedList == null)
                return null;
            else
                return (IEnumerator<T>) this._convertedLinkedList;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
