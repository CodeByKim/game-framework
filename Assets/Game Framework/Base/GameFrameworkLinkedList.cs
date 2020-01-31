using System;
using System.Collections;
using System.Collections.Generic;

namespace GameFramework
{
    public sealed class GameFrameworkLinkedList<T> : ICollection<T>, IEnumerable<T>, ICollection, IEnumerable
    {
        private readonly LinkedList<T> linkedList;
        private readonly Queue<LinkedListNode<T>> cachedNodes;

        public int Count => this.linkedList.Count;

        public int CachedNodeCount => this.cachedNodes.Count;

        public LinkedListNode<T> First => this.linkedList.First;

        public LinkedListNode<T> Last => this.linkedList.Last;

        public bool IsReadOnly => ((ICollection<T>)this.linkedList).IsReadOnly;

        public object SyncRoot => ((ICollection)this.linkedList).SyncRoot;

        public bool IsSynchronized => ((ICollection)this.linkedList).IsSynchronized;

        public GameFrameworkLinkedList()
        {
            this.linkedList = new LinkedList<T>();
            this.cachedNodes = new Queue<LinkedListNode<T>>();
        }

        public void Add(T value)
        {
            AddLast(value);
        }

        public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
        {
            LinkedListNode<T> newNode = AcquireNode(value);
            this.linkedList.AddAfter(node, newNode);
            return newNode;
        }

        public void AddAfter(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            this.linkedList.AddAfter(node, newNode);
        }

        public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
        {
            LinkedListNode<T> newNode = AcquireNode(value);
            this.linkedList.AddBefore(node, newNode);
            return newNode;
        }

        public void AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            this.linkedList.AddBefore(node, newNode);
        }

        public LinkedListNode<T> AddFirst(T value)
        {
            LinkedListNode<T> node = AcquireNode(value);
            this.linkedList.AddFirst(node);
            return node;
        }

        public void AddFirst(LinkedListNode<T> node)
        {
            this.linkedList.AddFirst(node);
        }

        public LinkedListNode<T> AddLast(T value)
        {
            LinkedListNode<T> node = AcquireNode(value);
            this.linkedList.AddLast(node);
            return node;
        }

        public void AddLast(LinkedListNode<T> node)
        {
            this.linkedList.AddLast(node);
        }

        public void Clear()
        {
            LinkedListNode<T> current = this.linkedList.First;
            while(current != null)
            {
                ReleaseNode(current);
                current = current.Next;
            }

            this.linkedList.Clear();
        }

        public void ClearCachedNodes()
        {
            this.cachedNodes.Clear();
        }

        public bool Contains(T value)
        {
            return this.linkedList.Contains(value);
        }

        public void CopyTo(T[] array, int index)
        {
            this.linkedList.CopyTo(array, index);
        }

        public void CopyTo(Array array, int index)
        {
            ((ICollection)this.linkedList).CopyTo(array, index);
        }

        public LinkedListNode<T> Find(T value)
        {
            return this.linkedList.Find(value);
        }

        public LinkedListNode<T> FindLast(T value)
        {
            return this.linkedList.FindLast(value);
        }

        public bool Remove(T value)
        {
            LinkedListNode<T> node = this.linkedList.Find(value);
            if(node != null)
            {
                this.linkedList.Remove(node);
                ReleaseNode(node);
                return true;
            }

            return false;
        }

        public void RemoveFirst()
        {
            LinkedListNode<T> first = this.linkedList.First;
            this.linkedList.RemoveFirst();
            ReleaseNode(first);
        }

        public void RemoveLast()
        {
            LinkedListNode<T> last = this.linkedList.Last;
            this.linkedList.RemoveLast();
            ReleaseNode(last);
        }        

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this.linkedList);
        }

        private LinkedListNode<T> AcquireNode(T value)
        {
            LinkedListNode<T> node = null;
            if(this.cachedNodes.Count > 0)
            {
                node = this.cachedNodes.Dequeue();
                node.Value = value;
            }
            else
            {
                node = new LinkedListNode<T>(value);
            }

            return node;
        }

        private void ReleaseNode(LinkedListNode<T> node)
        {
            node.Value = default(T);
            this.cachedNodes.Enqueue(node);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public struct Enumerator : IEnumerator<T>, IEnumerator
        {
            private LinkedList<T>.Enumerator enumerator;

            public T Current => this.enumerator.Current;

            object IEnumerator.Current => this.enumerator.Current;

            public Enumerator(LinkedList<T> linkedList)
            {
                this.enumerator = linkedList.GetEnumerator();
            }

            public void Dispose()
            {
                this.enumerator.Dispose();
            }

            public bool MoveNext()
            {
                return this.enumerator.MoveNext();
            }

            public void Reset()
            {
                ((IEnumerator<T>)this.enumerator).Reset();
            }
        }
    }
}
