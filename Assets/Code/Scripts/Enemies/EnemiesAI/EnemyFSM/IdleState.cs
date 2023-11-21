using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyState
{
    private readonly EnemyAnimationController _enemyAnimationController;
    private readonly LineOfSight _lineOfSight;
    private float _idleTime = 0f;
    private readonly float _maxIdleTime = 2.9f;

    public IdleState(EnemyFSM fsm) : base(fsm)
    {
        _enemyAnimationController = fsm.GetComponent<EnemyAnimationController>();
        _lineOfSight = fsm.GetComponent<LineOfSight>();
    }

    public override void Enter()
    {
       // _enemyAnimationController.PlayIdleAnimation();
        _idleTime = 0f;
    }

    public override void Update()
    {
        if (_lineOfSight.CanSeePlayer())
        {
            // Update the enemy direction to face the player
            Vector2 direction = (Vector2)_lineOfSight.player.position - (Vector2)_fsm.transform.position;
            _enemyAnimationController.direction = direction;
        }

        _idleTime += Time.deltaTime;
        if (_idleTime >= _maxIdleTime)
        {
            _fsm.ChangeState(new PatrolState(_fsm));
        }
    }

    
}