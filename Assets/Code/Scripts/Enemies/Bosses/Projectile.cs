using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;
    [SerializeField] private float damageAmount;
    private ProjectilePool projectilePool;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);

        // Set the rotation based on the direction of the force
        var velocity = rb.velocity;
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Get a reference to the ProjectilePool script
        projectilePool = FindObjectOfType<ProjectilePool>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerDetection"))
        {
            other.GetComponent<PlayerModel>().Damage(damageAmount);
            // Return the projectile to the pool.
            projectilePool.ReturnProjectile(gameObject);
        }
    }

}