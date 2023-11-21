using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    //Animator variables:
    private Animator _animator;

    private DamageFlash _damageFlash;
    public Vector2 direction;
    public bool IsPlayingAttackAnimation { get; private set; }
    public bool OnAttackEvent { get; set; }

    private static readonly int DirectionX = Animator.StringToHash("DirectionX");
    private static readonly int DirectionY = Animator.StringToHash("DirectionY");
    private static readonly int AttackDirectionX = Animator.StringToHash("AttackDirectionX");
    private static readonly int AttackDirectionY = Animator.StringToHash("AttackDirectionY");
    private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsDead = Animator.StringToHash("IsDead");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _damageFlash = GetComponent<DamageFlash>();
    }

    private void Update()
    {
        // Actualiza la dirección en el Animator Controller
        _animator.SetBool(IsMoving, Mathf.Abs(direction.x) + Mathf.Abs(direction.y) > 0);
        _animator.SetFloat(DirectionX, direction.x);
        _animator.SetFloat(DirectionY, direction.y);
    }

    public void UpdateAttackState(Vector2 direction, bool isAttacking)
    {
        if (isAttacking)
        {
            SetAttackDirection(direction);
        }
        else
        {
            StopAttackAnimation();
        }
    }

    private void SetAttackDirection(Vector2 direction)
    {
        _animator.SetBool(IsAttacking, Mathf.Abs(direction.x) + Mathf.Abs(direction.y) > 0);
        _animator.SetFloat(AttackDirectionX, direction.x);
        _animator.SetFloat(AttackDirectionY, direction.y);

        IsPlayingAttackAnimation = true;
        OnAttackEvent = true;
    }

    private void StopAttackAnimation()
    {
        _animator.SetBool(IsAttacking, false);
        OnAttackEvent = false;
        IsPlayingAttackAnimation = false;
    }

    public void DamagedAnimation()
    {
        //_animator.SetBool(Damaged, true);
        _damageFlash.CallDamageFlash();
    }

    public void ChangePhaseTwo()
    {
        // Cambia el Animator Controller
        _animator.SetBool("PhaseTwo", true);
    }

    public void PlayDeathAnimation()
    {
        _animator.SetTrigger(IsDead);
    }
}