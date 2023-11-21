using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseTwo : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] private Transform[] firePoints;
    [SerializeField] private float fireRate = 2f;
    private float nextFireTime = 0f;

    [Header("Invulnerability Settings")]
    [SerializeField] private float invulnerabilityDuration = 2f;

    private bool isInvulnerable = false;

    private Transform player;
    private HealthController bossHealth;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bossHealth = GetComponent<HealthController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Time.time >= nextFireTime)
        {
            FireProjectiles();
            nextFireTime = Time.time + 1f / fireRate;
        }

        if (bossHealth.currentHealth <= bossHealth.maxHealth * 0.5f)
        {
            fireRate *= 2f;
            firePoints = new Transform[firePoints.Length * 2];
            spriteRenderer.color = Color.red;
            StartCoroutine(SetInvulnerable(invulnerabilityDuration));
        }
    }

    private void FireProjectiles()
    {
        foreach (Transform firePoint in firePoints)
        {
            var position = firePoint.position;
            GameObject projectile = Instantiate(projectilePrefab, position, firePoint.rotation);
            Vector2 direction = (player.position - position).normalized;
            projectile.transform.up = direction;
        }
    }

    private IEnumerator SetInvulnerable(float duration)
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(duration);
        isInvulnerable = false;
    }

    public bool IsPhaseComplete()
    {
        return false;
    }
}