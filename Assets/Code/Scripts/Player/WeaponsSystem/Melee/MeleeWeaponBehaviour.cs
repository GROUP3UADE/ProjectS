using UnityEngine;

public class MeleeWeaponBehaviour : MonoBehaviour
{
    private MeleeWeaponModelSO _model;
    void Start()
    {
        _model = GetComponentInParent<MeleeWeaponController>().Model;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("EnemyZombie") || collision.CompareTag("EnemyBoss"))
        {
            collision.GetComponent<IDamageable>().Damage(_model.Damage);
            collision.GetComponent<IKnockable>().Knock(_model.Knockback, collision.transform.position - transform.position);
        }
    }
}
