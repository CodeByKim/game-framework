using System;
using GameFramework.Fsm;

namespace GameFramework.Procedure
{
    public abstract class ProcedureBase : FsmState<ProcedureModule>
    {
        public override void OnInit(Fsm<ProcedureModule> fsm)
        {
            base.OnInit(fsm);
        }

        public override void OnEnter(Fsm<ProcedureModule> fsm)
        {
            base.OnEnter(fsm);
        }

        public override void OnUpdate(Fsm<ProcedureModule> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        }

        public override void OnLeave(Fsm<ProcedureModule> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
        }

        public override void OnDestroy(Fsm<ProcedureModule> fsm)
        {
            base.OnDestroy(fsm);
        }
    }
}