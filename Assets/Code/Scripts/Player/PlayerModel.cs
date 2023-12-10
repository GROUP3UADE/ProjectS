using System;
using System.Collections;
using UnityEngine;

public class PlayerModel : MonoBehaviour, IKnockable, IDamageable
{
    #region Setups

    [Range(0, 10)][SerializeField] private float baseSpeed = 5f;
    [Range(0, 10)][SerializeField] private float attackTime;
    [Range(1, .1f)][SerializeField] private float knockBackResistance;
    [SerializeField] private PlayerAttack playerAttack;

    #endregion

    #region Miembros privados

    private Rigidbody2D _rigidbody;
    private Vector2 _lookAtDirection;
    private Vector2 _forward;
    private bool _attacking;
    private bool _idle;
    private float _currSpeed;

    #endregion

    #region Miembros publicos

    public Health Health { get; private set; }
    public IInteractable InteractableGo { get; private set; }

    #endregion
    
    //VFX:
    [SerializeField] private GameObject bloodSpillPrefab;

    public bool InventoryFull;

    public Action<Vector2> OnMove = delegate { };
    // public Action OnDamaged = delegate { };
    // public Action OnHealed = delegate { };

    public Vector2 Direction { get; private set; }

    public bool Attacking => _attacking;
    public bool Idle => _idle;
    public float AttackTime => attackTime;
    public Vector2 Forward => _forward;
    public Vector2 LookAtDirection => _lookAtDirection;


    private void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        Health = gameObject.GetComponent<Health>();
        Health.OnConsumed += OnConsumedHandler;
        Health.OnGained += OnGainedHandler;
        Health.OnDeath += OnDeathHandler;
        _currSpeed = baseSpeed;
    }

    private void Start()
    {
        GameManager.Instance.PlayerHealthBar.SetMaxHealth(Health.BaseMaxHealth);
        _currSpeed = baseSpeed;
    }

    public void Move(Vector2 direction)
    {
        //Para el Dialogue system
        if (!this.enabled) return;

        if (_attacking) return;
        Direction = direction * (_currSpeed * Time.deltaTime);
        transform.Translate(Direction);
        _idle = Direction is { x: 0, y: 0 };
    }

    public void LookAt(Vector2 direction)
    {
        _lookAtDirection = direction;
        _forward = (Vector2)transform.position + direction;
    }

    public void Attack()
    {
        _attacking = true;
        Invoke(nameof(StopAttacking), attackTime);
    }

    public void StopAttacking()
    {
        _attacking = false;
    }

    public void PickupItemSO(ItemSO itemSO, int quantity)
    {
        // usa el id para leer la database y asi agregar el item a su inventario
        ItemSO item = GameManager.Instance.ItemDatabase.GetItemSO(itemSO);
        if (item != null)
        {
            GameManager.Instance.PlayerInventory.AddItemSO(item, quantity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere((Vector3)_forward, .1f);
    }

    public void Knock(float knockForce, Vector2 knockDirection)
    {
        if (!gameObject.activeSelf) return;
        _rigidbody.AddForce(knockDirection * knockForce, ForceMode2D.Impulse);
        StartCoroutine(ResetKnock());
    }

    public IEnumerator ResetKnock()
    {
        yield return new WaitForSeconds(knockBackResistance);
        _rigidbody.velocity = Vector2.zero;
    }

    // Estos metodos(damage|heal) se podrian sacar y hacer directos, pero ni ganas
    public void Damage(float damage)
    {
        Health.TakeDamage((int)damage);
        //VFX:
        Instantiate(bloodSpillPrefab, transform.position, Quaternion.identity);
        AudioManager.Instance.PlayDamageSound();
        AudioManager.Instance.PlayPunchSound();
        //SCREEN SHAKE:
        CinemachineShake.Instance.ShakeCamera(5f, 0.1f);
        //OnDamaged.Invoke();
    }

    public void Heal(int healing)
    {
        Health.Heal(healing);
        //OnHealed.Invoke();
    }

    private void OnConsumedHandler()
    {
        // Reaccionar cuando pierde vida
        GameManager.Instance.PlayerHealthBar.SetHealth(Health.CurrentHealth);
    }

    private void OnGainedHandler()
    {
        // Reaccionar cuando gana vida
        GameManager.Instance.PlayerHealthBar.SetHealth(Health.CurrentHealth);
    }

    private void OnDeathHandler()
    {
        // Reaccionar cuando muere
        GameManager.Instance.PlayerHealthBar.SetHealth(Health.CurrentHealth);
        GameManager.Instance.GameOver();
    }

    public void OnCarDriveStartHandler(float vehicleSpeed)
    {
        _currSpeed = vehicleSpeed;
    }

    public void OnCarDriveEndHandler()
    {
        _currSpeed = baseSpeed;
    }

    public void SaveInteractable(IInteractable interactable)
    {
        InteractableGo = interactable;
    }
}