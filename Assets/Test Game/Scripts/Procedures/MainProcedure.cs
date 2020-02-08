using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GameFramework.Procedure;
using GameFramework.Fsm;

public class MainProcedure : ProcedureBase
{
    public override void OnEnter(Fsm<ProcedureModule> fsm)
    {
        base.OnEnter(fsm);
        Debug.Log("OnEnter => MainProcedure");
    }

    public override void OnUpdate(Fsm<ProcedureModule> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        Debug.Log("OnUpdate => MainProcedure");        
    }
}
