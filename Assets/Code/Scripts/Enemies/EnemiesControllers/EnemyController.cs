using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable, IKnockable
{
    // Serialized Fields
    [SerializeField] private EnemyTypes enemyType;

    [SerializeField] public float attackDistance = 1f;
    [SerializeField] public int enemyDamage;
    [SerializeField] private float knockBackResistance;
    [SerializeField] private EnemyHealthBar healthBar;
    [SerializeField] private GameObject bloodSpillPrefab;

    // Private Variables
    private EnemyAnimationController _enemyAnimationController;

    private Health _health;
    private Rigidbody2D _rigidbody;
    private LootDrop _lootDrop;

    // Public Variables
    public float moveSpeed = 5f;

    public float attackCooldown = 2f;
    public bool isAttacking;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _enemyAnimationController = GetComponent<EnemyAnimationController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _lootDrop = GetComponent<LootDrop>();
    }

    private void Start()
    {
        _health.OnConsumed += OnDamageHandler;
        _health.OnDeath += OnDeathHandler;

        enemyDamage = enemyType == EnemyTypes.Mutant ? 20 : 10;

        UpdateHealthBar();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }

    public void Damage(float damage)
    {
        _health.TakeDamage((int)damage);
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

    private void OnDamageHandler()
    {
        _enemyAnimationController.DamagedAnimation();
        Instantiate(bloodSpillPrefab, transform.position, Quaternion.identity);
        AudioManager.Instance.PlayPunchSound();
        UpdateHealthBar();
    }

    private void OnDeathHandler()
    {
        healthBar.gameObject.SetActive(false);

        if (enemyType == EnemyTypes.Zombie)
        {
            AudioManager.Instance.PlayZombieSound();
        }
        else if (enemyType == EnemyTypes.Mutant)
        {
            AudioManager.Instance.PlayMutantSound();
        }

        GetComponent<EnemyFSM>()?.Die();

        StartCoroutine(DeactivateAfterDelay(1.4f));
    }

    private IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
        _lootDrop.SpawnPickups();
        GameManager.Instance.KillCountManager.CountUpdate(enemyType);
    }

    private void UpdateHealthBar()
    {
        healthBar.UpdateHealthBar(_health.GetRatio);
    }
}