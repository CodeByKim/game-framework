using System;

namespace GameFramework.Fsm
{
    public abstract class FsmState<T> where T : class
    {
        public FsmState()
        {
        }

        public virtual void OnInit(Fsm<T> fsm)
        {
        }

        public virtual void OnEnter(Fsm<T> fsm)
        {
        }

        public virtual void OnUpdate(Fsm<T> fsm, float elapseSeconds, float realElapseSeconds)
        {
        }

        public virtual void OnLeave(Fsm<T> fsm, bool isShutdown)
        {
        }

        public virtual void OnDestroy(Fsm<T> fsm)
        {
        }

        protected void ChangeState<TState>(Fsm<T> fsm) where TState : FsmState<T>
        {
            fsm.ChangeState<TState>();
        }

        protected void ChangeState(Fsm<T> fsm, Type stateType)
        {
            fsm.ChangeState(stateType);
        }
    }
}