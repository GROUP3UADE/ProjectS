using System;
using UnityEngine;

public abstract class WeaponPivotBehaviour : MonoBehaviour
{
    #region Serializables

    [SerializeField]
    protected Transform Character;
    [SerializeField]
    protected InputController Input;
    [SerializeField]
    protected WeaponController CurrentWeapon;

    #endregion

    #region Miembros protegidos

    protected Vector2 LookDir { get; private set; }

    #endregion

    #region Metodos Virtuales


    public virtual void Update()
    {
        LookDir = (Input.MousePosition - (Vector2)Character.transform.position).normalized;
        //float angle = Mathf.Atan2(LookDir.y, LookDir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.localPosition = LookDir * 0.06f;
        transform.right = -LookDir;
    }

    /// <summary>
    /// Firma del metodo para atacar. Se debe implementar en la clase hijo que herede esta clase.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public virtual void Attack() => throw new InvalidOperationException();

    public void EquipWeapon(GameObject w)
    {
        Destroy(CurrentWeapon.gameObject);
        CurrentWeapon = Instantiate(w, transform).GetComponent<WeaponController>();
    }

    #endregion
}
