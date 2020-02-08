using System;
using System.Collections.Generic;

namespace GameFramework.Fsm
{
    public sealed class FsmModule : GameFrameworkModule
    {
        private readonly Dictionary<TypeNamePair, FsmBase> fsms;
        private readonly List<FsmBase> tempFsms;

        public override int Prioruty => 60;

        public int Count => this.fsms.Count;

        public FsmModule()
        {
            this.fsms = new Dictionary<TypeNamePair, FsmBase>();
            this.tempFsms = new List<FsmBase>();
        }

        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            this.tempFsms.Clear();

            if(this.fsms.Count <= 0)
            {
                return;
            }

            foreach(var fsm in this.fsms)
            {
                this.tempFsms.Add(fsm.Value);
            }

            foreach(var fsm in this.tempFsms)
            {
                if(fsm.IsDestroyed)
                {
                    continue;
                }

                fsm.Update(elapseSeconds, realElapseSeconds);
            }
        }

        public override void Shutdown()
        {
            foreach(var fsm in this.fsms)
            {
                fsm.Value.Shutdown();
            }

            this.fsms.Clear();
            this.tempFsms.Clear();
        }

        public bool HasFsm<T>() where T : class
        {
            TypeNamePair typeNamePair = new TypeNamePair(typeof(T));
            return this.fsms.ContainsKey(typeNamePair);
        }

        public Fsm<T> GetFsm<T>() where T : class
        {
            TypeNamePair typeNamePair = new TypeNamePair(typeof(T));

            FsmBase fsm = null;
            if(this.fsms.TryGetValue(typeNamePair, out fsm))
            {
                return fsm as Fsm<T>;
            }

            return null;
        }

        public FsmBase[] GetAllFsms()
        {
            FsmBase[] results = new FsmBase[this.fsms.Count];

            int index = 0;
            foreach(var fsm in this.fsms)
            {
                results[index] = fsm.Value;
                index += 1;
            }

            return results;
        }

        public Fsm<T> CreateFsm<T>(T owner, params FsmState<T>[] states) where T : class
        {
            return CreateFsm(string.Empty, owner, states);
        }

        public Fsm<T> CreateFsm<T>(string name, T owner, params FsmState<T>[] states) where T : class
        {
            TypeNamePair typeNamePair = new TypeNamePair(typeof(T), name);
            Fsm<T> fsm = Fsm<T>.Create(name, owner, states);
            this.fsms.Add(typeNamePair, fsm);

            return fsm;
        }

        public Fsm<T> CreateFsm<T>(string name, T owner, List<FsmState<T>> states) where T : class
        {
            TypeNamePair typeNamePair = new TypeNamePair(typeof(T), name);
            Fsm<T> fsm = Fsm<T>.Create(name, owner, states);
            this.fsms.Add(typeNamePair, fsm);

            return fsm;
        }

        public bool DestroyFsm<T>() where T : class
        {
            TypeNamePair typeNamePair = new TypeNamePair(typeof(T));

            FsmBase fsm = null;
            if(this.fsms.TryGetValue(typeNamePair, out fsm))
            {
                return this.fsms.Remove(typeNamePair);
            }

            return false;
        }

        public bool DestroyFsm(FsmBase fsm)
        {
            TypeNamePair typeNamePair = new TypeNamePair(fsm.OwnerType, fsm.Name);

            FsmBase tempFsm = null;
            if (this.fsms.TryGetValue(typeNamePair, out tempFsm))
            {
                return this.fsms.Remove(typeNamePair);
            }

            return false;
        }
    }
}