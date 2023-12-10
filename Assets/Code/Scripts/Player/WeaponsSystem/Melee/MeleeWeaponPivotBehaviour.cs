using UnityEngine;

public class MeleeWeaponPivotBehaviour : WeaponPivotBehaviour
{
    /// <summary>
    /// Para el arma inicial. Va a recibir un prefab serializado que hay que instanciar.
    /// </summary>
    void Start()
    {
        CurrentWeapon = Instantiate(CurrentWeapon, transform);
        Input.PrimaryFireEventStarted += Attack;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Attack() { if (!CurrentWeapon.gameObject.activeSelf) CurrentWeapon.gameObject.SetActive(true); }

    public void EquipWeapon(MeleeWeaponController w)
    {
        Destroy(CurrentWeapon.gameObject);
        CurrentWeapon = Instantiate(w, transform);
    }
}