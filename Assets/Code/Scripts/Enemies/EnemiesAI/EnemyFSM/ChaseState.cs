using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : EnemyState
{
    private readonly EnemyAnimationController _enemyAnimationController;
    private readonly LineOfSight _lineOfSight;
    private readonly float _attackDistance;
    private readonly float _moveSpeed;

    public ChaseState(EnemyFSM fsm) : base(fsm)
    {
        _enemyAnimationController = fsm.GetComponent<EnemyAnimationController>();
        _lineOfSight = fsm.GetComponent<LineOfSight>();
        _attackDistance = fsm.GetComponent<EnemyController>().attackDistance;
        _moveSpeed = fsm.GetComponent<EnemyController>().moveSpeed;
    }

    public override void Enter()
    {
        // Play the chase animation
        //_enemyAnimationController.PlayChaseAnimation();
    }

    public override void Update()
    {
        if (_lineOfSight.CanSeePlayer())
        {
            if (IsPlayerInAttackRange())
            {
                TransitionToAttackState();
            }
            else if (!IsAttacking())
            {
                MoveTowardsPlayer();
            }
        }
        else
        {
            TransitionToPatrolState();
        }
    }

    private bool IsPlayerInAttackRange()
    {
        return Vector2.Distance(_fsm.transform.position, _lineOfSight.player.position) <= _attackDistance;
    }

    private void TransitionToAttackState()
    {
        _fsm.ChangeState(new AttackState(_fsm));
    }

    private bool IsAttacking()
    {
        return _fsm.GetComponent<EnemyController>().isAttacking;
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (Vector2)_lineOfSight.player.position - (Vector2)_fsm.transform.position;
        _fsm.transform.Translate(direction.normalized * (_moveSpeed * Time.deltaTime));
        UpdateEnemyDirection(direction);
    }

    private void UpdateEnemyDirection(Vector2 direction)
    {
        _enemyAnimationController.direction = direction;
    }

    private void TransitionToPatrolState()
    {
        _fsm.ChangeState(new PatrolState(_fsm));
    }
}