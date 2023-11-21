using System.Collections;
using UnityEngine;

public class BossFSM : MonoBehaviour, IDamageable, IKnockable
{
    public enum BossState
    {
        PhaseOne,
        PhaseTwo
    }

    public BossState currentState;

    [Header("Projectile Settings")]
    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform firePoint2;
    [SerializeField] private float fireRate;
    [SerializeField] private float projectileAttackRange;
    private float nextFireTime;

    [Header("Health")] private Health _health;

    [Header("Health Bar")] public EnemyHealthBar healthBar;

    [Header("Animation")] private EnemyAnimationController _enemyAnimationController;

    [Header("Rigidbody")] private Rigidbody2D _rigidbody;

    [Header("Knockback")]
    [Range(1, .1f)]
    [SerializeField]
    private float knockBackResistance;

    private Transform player;
    private SpriteRenderer spriteRenderer;
    public Vector2 direction;
    private LootDrop _lootDrop;

    [Header("VFX")][SerializeField] private GameObject bloodSpillPrefab;
    [SerializeField] private EnemyTypes enemytype;

    private void Start()
    {
        currentState = BossState.PhaseOne;
        _health = GetComponent<Health>();
        healthBar.UpdateHealthBar(_health.CurrentHealth, _health.MaxHealth);
        _health.OnDeath += HandleDeath; // Suscribe HandleDeath al evento OnDeath
        _enemyAnimationController = GetComponent<EnemyAnimationController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        _lootDrop = GetComponent<LootDrop>();
    }

    private void Update()
    {
        // Update direction
        direction = (player.position - transform.position).normalized;
        //HealthBar- NEW
        healthBar.UpdateHealthBar(_health.CurrentHealth, _health.MaxHealth);
        switch (currentState)
        {
            case BossState.PhaseOne:
                if (IsPhaseOneComplete())
                {
                    TransitionToState(BossState.PhaseTwo);
                    AudioManager.Instance.PlayTenseMusic();
                }

                break;

            case BossState.PhaseTwo:
                if (IsPhaseTwoComplete())
                {
                }

                break;
        }

        // Check if player is within projectile attack range before firing a projectile
        if (Vector3.Distance(transform.position, player.position) <= projectileAttackRange && Time.time >= nextFireTime)
        {
            FireProjectile();
            nextFireTime = Time.time + 1f / fireRate;
        }

        // Update animation
        _enemyAnimationController.UpdateAttackState(direction, true);
    }

    private void TransitionToState(BossState newState)
    {
        currentState = newState;
        if (newState == BossState.PhaseTwo)
        {
            fireRate *= 2.2f;
            // Llama al m�todo para cambiar el Animator Controller
            _enemyAnimationController.ChangePhaseTwo();
            projectileAttackRange *= 1.5f;
        }
    }

    public void Damage(float damage)
    {
        _health.TakeDamage((int)damage); // Reemplaza EnemyTakeDamage por TakeDamage
        Instantiate(bloodSpillPrefab, transform.position, Quaternion.identity);
        AudioManager.Instance.PlayPunchSound();
        _enemyAnimationController.DamagedAnimation();
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

    private void FireProjectile()
    {
        // Verifica si el jefe est� muerto
        if (_health.IsDead)
        {
            return;
        }

        // Get a reference to the ProjectilePool script
        ProjectilePool projectilePool = FindObjectOfType<ProjectilePool>();

        var position = firePoint.position;
        // Get a projectile from the pool instead of creating a new object
        GameObject projectile = projectilePool.GetProjectile();
        projectile.transform.position = position;
        projectile.transform.rotation = firePoint.rotation;
        Vector2 direction = (player.position - position).normalized;
        projectile.transform.up = direction;

        // Dispara un proyectil desde el segundo punto de fuego
        if (currentState == BossState.PhaseTwo)
        {
            position = firePoint2.position;
            // Get a projectile from the pool instead of creating a new object
            projectile = projectilePool.GetProjectile();
            projectile.transform.position = position;
            projectile.transform.rotation = firePoint2.rotation;
            direction = (player.position - position).normalized;
            projectile.transform.up = direction;
        }
    }

    private void HandleDeath()
    {
        EnemyAnimationController enemyAnimationController = GetComponent<EnemyAnimationController>();
        enemyAnimationController.direction = direction;
        enemyAnimationController.PlayDeathAnimation();
        AudioManager.Instance.PlayZombieSound();
        Invoke("DeactivateBoss", 1.5f);
    }

    private void DeactivateBoss()
    {
        _lootDrop.SpawnPickups();
        GameManager.Instance.KillCountManager.CountUpdate(enemytype);
        gameObject.SetActive(false);
    }

    private bool IsPhaseOneComplete()
    {
        return
            _health.CurrentHealth <=
            _health.MaxHealth * 0.5f; // Reemplaza currentHealth y maxHealth por CurrentHealth y MaxHealth
    }

    private bool IsPhaseTwoComplete()
    {
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, projectileAttackRange);
    }
}