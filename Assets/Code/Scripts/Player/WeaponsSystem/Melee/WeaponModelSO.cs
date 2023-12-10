using UnityEngine;

public class WeaponModelSO : ScriptableObject
{
    #region Gets

    public float Damage => _damage;
    public float AttackSpeed => _attackSpeed;
    public float Knockback => _knockback;

    #endregion

    #region Sets

    [SerializeField]
    private float _damage;
    [SerializeField]
    private float _attackSpeed;
    [SerializeField]
    [Range(0, 5)]
    private float _knockback;

    #endregion
}
