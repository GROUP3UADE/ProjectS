using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseOne : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1f;
    private float nextFireTime = 0f;

    private Transform player;
    private HealthController bossHealth;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bossHealth = GetComponent<HealthController>();
    }

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            FireProjectile();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void FireProjectile()
    {
        var position = firePoint.position;
        GameObject projectile = Instantiate(projectilePrefab, position, firePoint.rotation);
        Vector2 direction = (player.position - position).normalized;
        projectile.transform.up = direction;
    }

    public bool IsPhaseComplete()
    {
        return bossHealth.currentHealth <= bossHealth.maxHealth * 0.5f;
    }
}