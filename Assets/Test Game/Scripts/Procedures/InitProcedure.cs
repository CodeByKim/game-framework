using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GameFramework.Fsm;
using GameFramework.Procedure;

public class InitProcedure : ProcedureBase
{
    public override void OnEnter(Fsm<ProcedureModule> fsm)
    {
        base.OnEnter(fsm);
        Debug.Log("OnEnter => InitProcedure");
    }

    public override void OnUpdate(Fsm<ProcedureModule> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        Debug.Log("OnUpdate => InitProcedure");
        ChangeState<MainProcedure>(fsm);
    }
}
