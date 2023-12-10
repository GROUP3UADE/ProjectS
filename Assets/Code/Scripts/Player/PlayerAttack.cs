using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerModel playerModel;
    [SerializeField] private Collider2D attackCollider;
    [SerializeField] private Animator animator;
    [SerializeField] private float damage;
    [SerializeField] private float knockForce;
    
    private static readonly int AttackAnimation = Animator.StringToHash("Attack");
    private readonly List<GameObject> _attacked = new();
    
    public void Attack()
    {
        AudioManager.Instance.PlaySwordSound();
        transform.position = playerModel.Forward;

        var rotateDirection = playerModel.Forward - (Vector2)playerModel.transform.position;
        
        float angle = Mathf.Atan2(rotateDirection.y, rotateDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        attackCollider.enabled = true;
        //animator.SetTrigger(AttackAnimation);
        Invoke(nameof(TurnOffCollider), playerModel.AttackTime);
    }

    private void TurnOffCollider()
    {
        attackCollider.enabled = false;
        _attacked.Clear(); // And this is worse
    }

    
}