using System;

namespace GameFramework.Fsm
{
    public abstract class FsmBase
    {
        private string name;

        public string Name
        {
            get { return this.name; }
            set { this.name = value ?? string.Empty; }
        }

        public string FullName => new TypeNamePair(OwnerType, this.name).ToString();

        public abstract Type OwnerType
        {
            get;
        }

        public abstract int FsmStateCount
        {
            get;
        }

        public abstract bool IsRunning
        {
            get;
        }

        public abstract bool IsDestroyed
        {
            get;
        }

        public abstract string CurrentStateName
        {
            get;
        }

        public abstract float CurrentStateTime
        {
            get;
        }

        public FsmBase()
        {
            this.name = string.Empty;
        }

        public abstract void Update(float elapseSeconds, float realElapseSeconds);

        public abstract void Shutdown();
    }
}