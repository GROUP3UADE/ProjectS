using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AttackState : EnemyState
{
    private readonly EnemyAnimationController _enemyAnimationController;
    private readonly LineOfSight _lineOfSight;
    private readonly float _attackDistance;
    private readonly float _attackCooldown;
    private float _timeSinceLastAttack;
    private IDamageable _playerDamageable;
    private IKnockable _playerKnockable;
    private int _damageAmount = 10;
    private float _knockbackForce = 10;

    public AttackState(EnemyFSM fsm) : base(fsm)
    {
        _enemyAnimationController = fsm.EnemyAnimationController;
        _lineOfSight = fsm.LineOfSight;
        _attackDistance = fsm.EnemyController.attackDistance;
        _attackCooldown = fsm.EnemyController.attackCooldown;
        _playerDamageable = fsm.PlayerDamageable;
        _playerKnockable = fsm.PlayerKnockable;
    }

    public override void Enter()
    {
        UpdateAttackDirection();
        SetIsAttacking(true);
        _timeSinceLastAttack = 0f;
    }

    public override void Exit()
    {
        SetIsAttacking(false);
        StopAttackAnimation();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public override void Update()
    {
        _timeSinceLastAttack += Time.deltaTime;

        if (_lineOfSight.CanSeePlayer())
        {
            if (IsPlayerInAttackRange())
            {
                if (CanAttack())
                {
                    UpdateAttackDirection();
                    AttackPlayer();
                }
            }
            else
            {
                TransitionToState(new ChaseState(_fsm));
            }
        }
        else
        {
            TransitionToState(new PatrolState(_fsm));
        }
    }

    private void UpdateAttackDirection()
    {
        Vector2 direction = (Vector2)_lineOfSight.player.position - (Vector2)_fsm.transform.position;
        _enemyAnimationController.UpdateAttackState(direction, true);
    }

    private void SetIsAttacking(bool isAttacking)
    {
        _fsm.EnemyController.isAttacking = isAttacking;
    }

    private void StopAttackAnimation()
    {
        _enemyAnimationController.UpdateAttackState(Vector2.zero, false);
    }

    private bool IsPlayerInAttackRange()
    {
        return Vector2.Distance(_fsm.transform.position, _lineOfSight.player.position) <= _attackDistance;
    }

    private bool CanAttack()
    {
        return _timeSinceLastAttack >= _attackCooldown && _enemyAnimationController.IsPlayingAttackAnimation;
    }

    private void AttackPlayer()
    {
        _damageAmount = _fsm.EnemyController.enemyDamage;

        if (_enemyAnimationController.OnAttackEvent)
        {
            // Cause damage to the player
            _playerDamageable.Damage(_damageAmount);
            _playerKnockable.Knock(_knockbackForce, (_lineOfSight.player.gameObject.transform.position - _fsm.transform.position).normalized);
        }
        _timeSinceLastAttack = 0f;
    }

    private void TransitionToState(EnemyState newState)
    {
        _fsm.ChangeState(newState);
    }
}