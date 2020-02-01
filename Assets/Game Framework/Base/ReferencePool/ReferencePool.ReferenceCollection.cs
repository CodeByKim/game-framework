using System;
using System.Collections.Generic;

namespace GameFramework
{
    public static partial class ReferencePool
    {
        private sealed class ReferenceCollection
        {
            private readonly Queue<IReference> references;
            private readonly Type referenceType;
            private int usingReferenceCount;
            private int acquireReferenceCount;
            private int releaseReferenceCount;
            private int addReferenceCount;
            private int removeReferenceCount;

            public Type ReferenceType => this.referenceType;

            public int UnusedReferenceCount => this.references.Count;

            public int UsingReferenceCount => this.usingReferenceCount;

            public int AcquireReferenceCount => this.acquireReferenceCount;

            public int ReleaseReferenceCount => this.releaseReferenceCount;

            public int AddReferenceCount => this.addReferenceCount;

            public int RemoveRerferenceCount => this.removeReferenceCount;

            public ReferenceCollection(Type referenceType)
            {
                this.references = new Queue<IReference>();
                this.referenceType = referenceType;
                this.usingReferenceCount = 0;
                this.acquireReferenceCount = 0;
                this.releaseReferenceCount = 0;
                this.addReferenceCount = 0;
                this.removeReferenceCount = 0;
            }

            public T Acquire<T>() where T : class, IReference, new()
            {
                this.usingReferenceCount += 1;
                this.acquireReferenceCount += 1;

                lock(this.references)
                {
                    if(this.references.Count > 0)
                    {
                        return this.references.Dequeue() as T;
                    }
                }

                this.addReferenceCount += 1;
                return new T();
            }

            public IReference Acquire()
            {
                this.usingReferenceCount += 1;
                this.acquireReferenceCount += 1;

                lock(this.references)
                {
                    if(this.references.Count > 0)
                    {
                        return this.references.Dequeue();
                    }
                }

                this.addReferenceCount += 1;
                return Activator.CreateInstance(this.referenceType) as IReference;
            }

            public void Release(IReference reference)
            {
                reference.Clear();

                lock(this.references)
                {
                    this.references.Enqueue(reference);
                }

                this.releaseReferenceCount += 1;
                this.usingReferenceCount -= 1;
            }

            public void Add<T>(int count) where T : class, IReference, new()
            {
                lock(this.references)
                {
                    this.addReferenceCount += count;
                    while(count-- > 0)
                    {
                        this.references.Enqueue(new T());
                    }
                }
            }

            public void Add(int count)
            {
                lock (this.references)
                {
                    this.addReferenceCount += count;
                    while (count-- > 0)
                    {
                        this.references.Enqueue(Activator.CreateInstance(this.referenceType) as IReference);
                    }
                }
            }

            public void Remove(int count)
            {
                lock(this.references)
                {
                    if(count > this.references.Count)
                    {
                        count = this.references.Count;
                    }

                    this.removeReferenceCount += count;

                    while(count-- > 0)
                    {
                        this.references.Clear();
                    }
                }
            }

            public void RemoveAll()
            {
                lock(this.references)
                {
                    this.removeReferenceCount += this.references.Count;
                    this.references.Clear();
                }
            }
        }
    }
}
    