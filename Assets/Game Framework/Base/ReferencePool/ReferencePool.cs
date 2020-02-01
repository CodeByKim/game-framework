using System;
using System.Collections.Generic;

namespace GameFramework
{
    public static partial class ReferencePool
    {
        private static readonly Dictionary<Type, ReferenceCollection> referenceCollections = new Dictionary<Type, ReferenceCollection>();
        private static bool enableStrictCheck = false;

        public static bool EnableStrictCheck
        {
            get { return enableStrictCheck; }
            set { enableStrictCheck = value; }
        }

        public static int Count => referenceCollections.Count;

        public static ReferencePoolInfo[] GetAllReferencePoolInfos()
        {
            int index = 0;
            ReferencePoolInfo[] results = null;

            lock(referenceCollections)
            {
                results = new ReferencePoolInfo[referenceCollections.Count];
                foreach(var referenceCollection in referenceCollections)
                {
                    results[index] = new ReferencePoolInfo(referenceCollection.Key,
                                                        referenceCollection.Value.UnusedReferenceCount,
                                                        referenceCollection.Value.UsingReferenceCount,
                                                        referenceCollection.Value.AcquireReferenceCount,
                                                        referenceCollection.Value.ReleaseReferenceCount,
                                                        referenceCollection.Value.AddReferenceCount,
                                                        referenceCollection.Value.ReleaseReferenceCount);
                    index += 1;
                }
            }

            return results;
        }

        public static void ClearAll()
        {
            lock(referenceCollections)
            {
                foreach(var referenceCollection in referenceCollections)
                {
                    referenceCollection.Value.RemoveAll();
                }

                referenceCollections.Clear();
            }
        }

        public static T Acquire<T>() where T : class, IReference, new()
        {
            return GetReferenceCollection(typeof(T)).Acquire<T>();
        }

        public static IReference Acquire(Type referenceType)
        {
            return GetReferenceCollection(referenceType).Acquire();
        }

        public static void Release(IReference reference)
        {
            Type referenceType = reference.GetType();
            GetReferenceCollection(referenceType).Release(reference);
        }

        public static void Add<T>(int count) where T : class, IReference, new()
        {
            GetReferenceCollection(typeof(T)).Add<T>(count);
        }

        public static void Add(Type referenceType, int count)
        {
            GetReferenceCollection(referenceType).Add(count);
        }

        public static void Remove<T>(int count) where T : class, IReference
        {
            GetReferenceCollection(typeof(T)).Remove(count);
        }

        public static void Remove(Type referenceType, int count)
        {
            GetReferenceCollection(referenceType).Remove(count);
        }

        public static void RemoveAll<T>() where T : class, IReference
        {
            GetReferenceCollection(typeof(T)).RemoveAll();
        }

        public static void RemoveAll(Type referenceType)
        {
            GetReferenceCollection(referenceType).RemoveAll();
        }

        private static ReferenceCollection GetReferenceCollection(Type referenceType)
        {
            ReferenceCollection referenceCollection = null;

            lock(referenceCollections)
            {
                if(!referenceCollections.TryGetValue(referenceType, out referenceCollection))
                {
                    referenceCollection = new ReferenceCollection(referenceType);
                    referenceCollections.Add(referenceType, referenceCollection);
                }
            }

            return referenceCollection;
        }
    }
}
    