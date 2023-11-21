using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    protected EnemyFSM _fsm;

    protected EnemyState(EnemyFSM fsm)
    {
        _fsm = fsm;
    }
    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}