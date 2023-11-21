using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] public float maxHealth = 100;
    [SerializeField] public float currentHealth;

    //ENEMY HEALTHBAR
    [SerializeField] private EnemyHealthBar healthBar;

    //PLAYER HEALTHBAR:
    [SerializeField] private PlayerHealthBar playerHealthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }
        if (playerHealthBar != null)
        {
            playerHealthBar.SetMaxHealth(maxHealth); //Initializes player max health with its current value (health bar reference).
        }
    }

    public void EnemyTakeDamage(float damageAmount)
    {
        print("Entity received" + damageAmount + " damage");
        currentHealth -= damageAmount;
        if ((gameObject.CompareTag("Enemy") || gameObject.CompareTag("EnemyZombie") || gameObject.CompareTag("EnemyBoss")) && healthBar != null)
        {
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void PlayerTakeDamage(float damageAmount)
    {
        // Reproduce el sonido de daño del jugador
        AudioManager.Instance.PlayDamageSound();

        print("Player received " + damageAmount + " damage");
        currentHealth -= damageAmount;

        if (playerHealthBar != null)
        {
            playerHealthBar.SetHealth(currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        print("Healed: " + healAmount);

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // Update player health bar
        if (playerHealthBar != null)
        {
            playerHealthBar.SetHealth(currentHealth);
        }
    }

    private void Die()
    {
        // Verifica si el jugador muere...
        if (GetComponent<PlayerModel>() != null)
        {
            // entonces, pantalla de GameOver.
            GameManager.Instance.GameOver();
        }

        //ZOMBIE ENEMY DEATH
        if (currentHealth <= 0)
        {
            print("Zombie DEATH STATE ENTER");
            // Cambia al estado DeathState
            var enemyFSM = GetComponent<EnemyFSM>();
            if (enemyFSM != null)
            {
                enemyFSM.Die();
            }
        }

        if (gameObject.CompareTag("EnemyZombie"))
        {
            StartCoroutine(DeactivateAfterDelay(1.4f)); // Delay Time for Zombie animation.
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}