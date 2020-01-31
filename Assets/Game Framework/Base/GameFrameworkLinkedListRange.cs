using System;
using System.Collections;
using System.Collections.Generic;

namespace GameFramework
{
    public class GameFrameworkLinkedListRange<T> : IEnumerable<T>, IEnumerable
    {
        private readonly LinkedListNode<T> first;
        private readonly LinkedListNode<T> terminal;

        public bool IsValid => this.first != null && this.terminal != null && this.first != this.terminal;

        public LinkedListNode<T> First => this.first;

        public LinkedListNode<T> Terminal => this.terminal;

        public int Count
        {
            get
            {
                if(!IsValid)
                {
                    return 0;
                }

                int count = 0;
                for(var current = this.first; current != null && current != this.terminal; current = current.Next)
                {
                    count += 1;
                }

                return count;
            }
        }

        public GameFrameworkLinkedListRange(LinkedListNode<T> first, LinkedListNode<T> terminal)
        {
            this.first = first;
            this.terminal = terminal;
        }


        public bool Contains(T value)
        {
            for (var current = this.first; current != null && current != this.terminal; current = current.Next)
            {
                if(current.Value.Equals(value))
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public struct Enumerator : IEnumerator<T>, IEnumerator
        {
            private readonly GameFrameworkLinkedListRange<T> gameFrameworkLinkedListRange;
            private LinkedListNode<T> current;
            private T currentValue;

            public T Current => this.currentValue;

            object IEnumerator.Current => this.currentValue;

            public Enumerator(GameFrameworkLinkedListRange<T> range)
            {
                this.gameFrameworkLinkedListRange = range;
                this.current = this.gameFrameworkLinkedListRange.first;
                this.currentValue = default(T);
            }

            public void Dispose()
            {                
            }

            public bool MoveNext()
            {
                if(this.current == null || this.current == this.gameFrameworkLinkedListRange.terminal)
                {
                    return false;
                }

                this.currentValue = this.current.Value;
                this.current = this.current.Next;
                return true;
            }

            public void Reset()
            {
                this.current = this.gameFrameworkLinkedListRange.first;
                this.currentValue = default(T);
            }
        }
    }
}

