using System;
using GameFramework.Fsm;

namespace GameFramework.Procedure
{
    public sealed class ProcedureModule : GameFrameworkModule
    {
        private FsmModule fsmModule;
        private Fsm<ProcedureModule> procedureFsm;

        public override int Prioruty => -10;

        public ProcedureBase CurrentProcedure => this.procedureFsm.CurrentState as ProcedureBase;

        public float CurrentProcedureTime => this.procedureFsm.CurrentStateTime;

        public ProcedureModule()
        {
            this.fsmModule = null;
            this.procedureFsm = null;
        }

        public void Initialize(FsmModule fsmModule, params ProcedureBase[] procedures)
        {
            this.fsmModule = fsmModule;
            this.procedureFsm = this.fsmModule.CreateFsm(this, procedures);
        }

        public void StartProcedure<T>() where T : ProcedureBase
        {
            this.procedureFsm.Start<T>();
        }

        public void StartProcedure(Type procedureType)
        {
            this.procedureFsm.Start(procedureType);
        }

        public bool HasProcedure<T>() where T : ProcedureBase
        {
            return this.procedureFsm.HasState<T>();
        }

        public ProcedureBase GetProcedure<T>() where T : ProcedureBase
        {
            return this.procedureFsm.GetState<T>();
        }

        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
        }

        public override void Shutdown()
        {
            if(this.fsmModule != null)
            {
                if(this.procedureFsm != null)
                {
                    this.fsmModule.DestroyFsm(this.procedureFsm);
                    this.procedureFsm = null;
                }

                this.fsmModule = null;
            }
        }
    }
}