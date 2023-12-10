using System;
using UnityEngine;

public abstract class WeaponController : MonoBehaviour
{
    // Use this for initialization
    public virtual void Start() { }

    /// <summary>
    /// Se tiene que sobreescribir por la clase hijo.
    /// </summary>
    public virtual void Attack() => throw new InvalidOperationException();
}
