using System;
using Character;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private PlayerModel playerModel;
    [SerializeField] private Animator animator;
    [SerializeField] private InputController _inputController;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Transform blacksmithTransform;

    [SerializeField] private DamageFlash damageFlash;
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Attack = Animator.StringToHash("Attack");

    private Vector2 _viewDirection;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        playerModel.Health.OnConsumed += Damaged;
    }

    private void Update()
    {
        animator.SetBool(Idle, playerModel.Idle);

        var lookAt = (_inputController.MousePosition - (Vector2)transform.position).normalized;

        var x = playerModel.Idle ? playerModel.LookAtDirection.x : playerModel.Direction.x;
        var y = playerModel.Idle ? playerModel.LookAtDirection.y : playerModel.Direction.y;

        animator.SetFloat("XDirection", x);
        animator.SetFloat("YDirection", y);

        animator.SetBool(Attack, playerModel.Attacking);

        if (blacksmithTransform != null)
        {
            float distanceToBlacksmith = Vector3.Distance(transform.position, blacksmithTransform.position);
            float maxAudibleDistance = 20f;

            audioManager.FadeBlacksmithSound(distanceToBlacksmith, maxAudibleDistance);
        }
    }

    private void Damaged()
    {
        damageFlash.CallDamageFlash();
    }
}