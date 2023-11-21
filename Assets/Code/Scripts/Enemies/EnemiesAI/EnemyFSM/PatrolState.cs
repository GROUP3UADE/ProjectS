using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyState
{
    private readonly EnemyAnimationController _enemyAnimationController;
    private readonly Transform[] _waypoints;
    private int _currentWaypointIndex;
    private Transform _currentWaypoint;
    private readonly LineOfSight _lineOfSight;
    private readonly float _moveSpeed;

    public PatrolState(EnemyFSM fsm) : base(fsm)
    {
        _enemyAnimationController = fsm.GetComponent<EnemyAnimationController>();
        _waypoints = fsm.GetComponent<EnemyPatrol>().waypoints;
        _lineOfSight = fsm.GetComponent<LineOfSight>();
        _moveSpeed = fsm.GetComponent<EnemyController>().moveSpeed;
    }

    public override void Enter()
    {
        // Play the patrol animation
        //_enemyAnimationController.PlayPatrolAnimation();

        if (_waypoints.Length > 0)
        {
            _currentWaypointIndex = 0;
            _currentWaypoint = _waypoints[_currentWaypointIndex];
        }
    }

    public override void Update()
    {
        if (_lineOfSight.CanSeePlayer())
        {
            TransitionToChaseState();
        }
        else if (_currentWaypoint != null && !IsAttacking())
        {
            MoveTowardsWaypoint();
        }
    }

    private void TransitionToChaseState()
    {
        _fsm.ChangeState(new ChaseState(_fsm));
    }

    private bool IsAttacking()
    {
        return _fsm.GetComponent<EnemyController>().isAttacking;
    }

    private void MoveTowardsWaypoint()
    {
        Vector2 direction = (Vector2)_currentWaypoint.position - (Vector2)_fsm.transform.position;
        _fsm.transform.Translate(direction.normalized * (_moveSpeed * Time.deltaTime));
        UpdateEnemyDirection(direction);

        if (HasReachedCurrentWaypoint(direction))
        {
            ChangeToNextWaypoint();
        }
    }

    private void UpdateEnemyDirection(Vector2 direction)
    {
        _enemyAnimationController.direction = direction;
    }

    private bool HasReachedCurrentWaypoint(Vector2 direction)
    {
        return direction.sqrMagnitude < 0.1f * 0.1f;
    }

    private void ChangeToNextWaypoint()
    {
        _currentWaypointIndex++;
        if (_currentWaypointIndex >= _waypoints.Length)
        {
            _currentWaypointIndex = 0;
        }
        _currentWaypoint = _waypoints[_currentWaypointIndex];
    }
}