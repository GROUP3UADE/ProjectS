using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    private EnemyState _currentState;

    public EnemyAnimationController EnemyAnimationController { get; private set; }
    public LineOfSight LineOfSight { get; private set; }
    public EnemyController EnemyController { get; private set; }
    public IDamageable PlayerDamageable { get; private set; }
    public IKnockable PlayerKnockable { get; private set; }

    private void Awake()
    {
        EnemyAnimationController = GetComponent<EnemyAnimationController>();
        LineOfSight = GetComponent<LineOfSight>();
        EnemyController = GetComponent<EnemyController>();
        PlayerDamageable = LineOfSight.player.GetComponent<IDamageable>();
        PlayerKnockable = LineOfSight.player.GetComponent<IKnockable>();
    }

    private void Start()
    {
        //initial state
        ChangeState(new IdleState(this));
    }

    private void Update()
    {
        // Update the current state
        _currentState.Update();
    }

    public void ChangeState(EnemyState newState)
    {
        // Exit the current state
        if (_currentState != null)
        {
            _currentState.Exit();
        }

        // Enter the new state
        _currentState = newState;
        _currentState.Enter();
    }
    
    public void Die()
    {
        ChangeState(new DeathState(this));
    }

}