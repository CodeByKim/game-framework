using System;
using System.Collections.Generic;

namespace GameFramework.Fsm
{
    public sealed class Fsm<T> : FsmBase, IReference where T : class
    {
        private T owner;
        private readonly Dictionary<Type, FsmState<T>> states;
        private readonly Dictionary<string, Variable> datas;
        private FsmState<T> currentState;
        private float currentStateTime;
        private bool isDestroyed;

        public T Owner => this.owner;

        public override Type OwnerType => typeof(T);

        public override int FsmStateCount => this.states.Count;

        public override bool IsRunning => this.currentState != null;

        public override bool IsDestroyed => this.isDestroyed;

        public override string CurrentStateName => this.currentState != null ? this.currentState.GetType().FullName : null;

        public override float CurrentStateTime => this.currentStateTime;

        public FsmState<T> CurrentState => this.currentState;

        public Fsm()
        {
            this.owner = null;
            this.states = new Dictionary<Type, FsmState<T>>();
            this.datas = new Dictionary<string, Variable>();
            this.currentState = null;
            this.currentStateTime = 0f;
            this.isDestroyed = true;
        }

        public static Fsm<T> Create(string name, T owner, params FsmState<T>[] states)
        {
            Fsm<T> fsm = ReferencePool.Acquire<Fsm<T>>();
            fsm.Name = name;
            fsm.owner = owner;
            fsm.isDestroyed = false;

            foreach(var state in states)
            {
                Type stateType = state.GetType();
                fsm.states.Add(stateType, state);
                state.OnInit(fsm);
            }

            return fsm;
        }

        public static Fsm<T> Create(string name, T owner, List<FsmState<T>> states)
        {
            Fsm<T> fsm = ReferencePool.Acquire<Fsm<T>>();
            fsm.Name = name;
            fsm.owner = owner;
            fsm.isDestroyed = false;

            foreach (var state in states)
            {
                Type stateType = state.GetType();
                fsm.states.Add(stateType, state);
                state.OnInit(fsm);
            }

            return fsm;
        }

        public void Clear()
        {
            if(this.currentState != null)
            {
                this.currentState.OnLeave(this, true);
            }

            foreach(var state in states)
            {
                state.Value.OnDestroy(this);
            }

            Name = null;
            this.owner = null;
            this.states.Clear();
            this.datas.Clear();
            this.currentState = null;
            this.currentStateTime = 0f;
            this.isDestroyed = true;
        }

        public void Start<TState>() where TState : FsmState<T>
        {
            FsmState<T> state = GetState<TState>();

            this.currentStateTime = 0f;
            this.currentState = state;
            this.currentState.OnEnter(this);
        }

        public void Start(Type stateType)
        {
            FsmState<T> state = GetState(stateType);

            this.currentStateTime = 0f;
            this.currentState = state;
            this.currentState.OnEnter(this);
        }

        public bool HasState<TState>() where TState : FsmState<T>
        {
            return this.states.ContainsKey(typeof(TState));
        }

        public bool HasState(Type stateType)
        {
            return this.states.ContainsKey(stateType);
        }

        public TState GetState<TState>() where TState : FsmState<T>
        {
            FsmState<T> state = null;
            if(this.states.TryGetValue(typeof(TState), out state))
            {
                return state as TState;
            }

            return null;
        }

        public FsmState<T> GetState(Type stateType)
        {
            FsmState<T> state = null;
            if (this.states.TryGetValue(stateType, out state))
            {
                return state;
            }

            return null;
        }

        public FsmState<T>[] GetAllStates()
        {            
            FsmState<T>[] results = new FsmState<T>[this.states.Count];

            int index = 0;
            foreach (var state in this.states)
            {
                results[index] = state.Value;
                index += 1;
            }

            return results;
        }

        public bool HasData(string name)
        {
            return this.datas.ContainsKey(name);
        }

        public TData GetData<TData>(string name) where TData : Variable
        {
            return GetData(name) as TData;
        }

        public Variable GetData(string name)
        {
            Variable data = null;
            if(this.datas.TryGetValue(name, out data))
            {
                return data;
            }

            return null;
        }

        public void SetData<TData>(string name, TData data) where TData : Variable
        {
            this.datas[name] = data;
        }

        public void SetData(string name, Variable data)
        {
            this.datas[name] = data;
        }

        public bool RemoveData(string name)
        {
            return this.datas.Remove(name);
        }

        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            if(this.currentState == null)
            {
                return;
            }

            this.currentStateTime += elapseSeconds;
            this.currentState.OnUpdate(this, elapseSeconds, realElapseSeconds);
        }

        public override void Shutdown()
        {
            ReferencePool.Release(this);
        }

        public void ChangeState<TState>() where TState : FsmState<T>
        {
            ChangeState(typeof(TState));
        }

        public void ChangeState(Type stateType)
        {
            FsmState<T> state = GetState(stateType);

            this.currentState.OnLeave(this, false);
            this.currentStateTime = 0f;
            this.currentState = state;
            this.currentState.OnEnter(this);
        }
    }
}