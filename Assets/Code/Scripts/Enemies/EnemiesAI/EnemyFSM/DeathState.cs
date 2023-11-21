using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : EnemyState
{
    private EnemyAnimationController _enemyAnimationController;
    private HealthController _healthController;
    
    public DeathState(EnemyFSM fsm) : base(fsm)
    {
        _enemyAnimationController = fsm.GetComponent<EnemyAnimationController>();
        _healthController = fsm.GetComponent<HealthController>();
    }
    
    public override void Enter()
    {
        if (_enemyAnimationController != null)
        {
            _enemyAnimationController.PlayDeathAnimation();
        }
        
        if (_healthController != null)
        {
            // ...
        }
    }

    public override void Update()
    {
        // ...
    }

    public override void Exit()
    {
        // ...
    }
}