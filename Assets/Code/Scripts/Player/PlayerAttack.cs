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
        animator.SetTrigger(AttackAnimation);
        Invoke(nameof(TurnOffCollider), playerModel.AttackTime);
    }

    private void TurnOffCollider()
    {
        attackCollider.enabled = false;
        animator.ResetTrigger(AttackAnimation);
        _attacked.Clear(); // And this is worse
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_attacked.Contains(other.gameObject)) return; // This can't be good
        
        var damageable = other.gameObject.GetComponent<IDamageable>();
        damageable?.Damage(damage);

        GameObject o;
        var knockable = (o = other.gameObject).GetComponent<IKnockable>();
        knockable?.Knock(knockForce, (o.transform.position - transform.position).normalized);
        
        _attacked.Add(other.gameObject);
    }
}