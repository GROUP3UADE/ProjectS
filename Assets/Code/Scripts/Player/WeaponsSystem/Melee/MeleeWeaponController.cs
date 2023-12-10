using UnityEngine;

public class MeleeWeaponController : WeaponController
{
    #region Serializables

    [SerializeField]
    private Transform _pivot;
    [SerializeField]
    private Transform _weapon;
    [SerializeField]
    private MeleeWeaponModelSO _weaponModelSO;

    #endregion

    #region Miembros privados

    private Quaternion _initRotation;

    #endregion

    #region Miembros publicos

    public MeleeWeaponModelSO Model => _weaponModelSO;

    #endregion

    #region Eventos de Unity

    public override void Start()
    {
        _initRotation = _weapon.localRotation;
        gameObject.SetActive(false);
    }

    void Update()
    {
        Attack();
        if (_weapon.localRotation.eulerAngles.z >= _initRotation.eulerAngles.z + _weaponModelSO.Angle) gameObject.SetActive(false);
    }

    void OnEnable() => _initRotation = _weapon.localRotation;

    void OnDisable() => _weapon.localRotation = _initRotation;

    

    #endregion

    #region Metodos publicos

    public override void Attack() => _weapon.RotateAround(_pivot.position, Vector3.forward, _weaponModelSO.AttackSpeed * Time.deltaTime);

    
    #endregion
        
}