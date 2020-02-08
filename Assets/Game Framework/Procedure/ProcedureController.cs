using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using GameFramework.Fsm;

namespace GameFramework.Procedure
{
    public sealed class ProcedureController : GameFrameworkController
    {
        private ProcedureModule procedureModule;
        private ProcedureBase entranceProcedure;

        [SerializeField] private string[] availableProcedureTypeNames;
        [SerializeField] private string entranceProcedureTypeName;

        public ProcedureBase CurrentProcedure => this.procedureModule.CurrentProcedure;

        protected override void Awake()
        {
            base.Awake();

            this.procedureModule = ModuleManager.GetModule<ProcedureModule>();
        }

        private IEnumerator Start()
        {
            ProcedureBase[] procedures = new ProcedureBase[this.availableProcedureTypeNames.Length];

            for(int i = 0; i < this.availableProcedureTypeNames.Length; i++)
            {
                Type procedureType = Type.GetType(this.availableProcedureTypeNames[i]);
                procedures[i] = Activator.CreateInstance(procedureType) as ProcedureBase;

                if(this.entranceProcedureTypeName == this.availableProcedureTypeNames[i])
                {
                    this.entranceProcedure = procedures[i];
                }
            }

            this.procedureModule.Initialize(ModuleManager.GetModule<FsmModule>(), procedures);
            yield return new WaitForEndOfFrame();

            this.procedureModule.StartProcedure(this.entranceProcedure.GetType());
        }

        public bool HasProcedure<T>() where T : ProcedureBase
        {
            return this.procedureModule.HasProcedure<T>();
        }

        public ProcedureBase GetProcedure<T>() where T : ProcedureBase
        {
            return this.procedureModule.GetProcedure<T>();
        }
    }
}