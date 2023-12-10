using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeapon", menuName = "Scriptables/MeleeWeapon")]
public class MeleeWeaponModelSO : WeaponModelSO
{
    #region Gets

    public float Angle => _angle;

    #endregion

    #region Sets

    [SerializeField]
    private float _angle;

    #endregion
}