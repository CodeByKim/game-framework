using System;

namespace GameFramework
{
    public struct ReferencePoolInfo
    {
        private readonly Type type;
        private readonly int unusedReferenceCount;
        private readonly int usingReferenceCount;
        private readonly int acquireReferenceCount;
        private readonly int releaseReferenceCount;
        private readonly int addReferenceCount;
        private readonly int removeReferenceCount;

        public Type ReferenceType => this.type;

        public int UnusedReferenceCount => this.unusedReferenceCount;

        public int UsingReferenceCount => this.usingReferenceCount;

        public int AcquireReferenceCount => this.acquireReferenceCount;

        public int ReleaseReferenceCount => this.releaseReferenceCount;

        public int AddReferenceCount => this.addReferenceCount;

        public int RemoveRerferenceCount => this.removeReferenceCount;

        public ReferencePoolInfo(Type type,
                                int unusedReferenceCount,
                                int usingReferenceCount,
                                int acquireReferenceCount,
                                int releaseReferenceCount,
                                int addReferenceCount,
                                int removeReferenceCount)
        {
            this.type = type;
            this.unusedReferenceCount = unusedReferenceCount;
            this.usingReferenceCount = usingReferenceCount;
            this.acquireReferenceCount = acquireReferenceCount;
            this.releaseReferenceCount = releaseReferenceCount;
            this.addReferenceCount = addReferenceCount;
            this.removeReferenceCount = removeReferenceCount;
        }
    }
}