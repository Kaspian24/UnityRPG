using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the behavior and state of a Blue Dragon character.
/// This class handles health management, damage response, and interaction with other game systems.
/// </summary>
public class BlueDragon : MonoBehaviour
{
    public int HP = 100;
    public Animator animator;

    public HealthBar healthBar;

    /// <summary>
    /// Initializes the Blue Dragon's health and sets up the health bar.
    /// Called when the script instance is being loaded.
    /// </summary>
    public void Start()
    {
        healthBar.setMaxHealth(HP);
        healthBar.setHealth(HP);
    }

    /// <summary>
    /// Handles the logic when the Blue Dragon takes damage.
    /// Reduces health points, updates the health bar, triggers animations, and interacts with game events.
    /// </summary>
    /// <param name="damage">The amount of damage to apply to the Blue Dragon.</param>
    public void TakeDamage(int damage)
    {
        HP -= damage;
        healthBar.setHealth(HP);

        if (HP <= 0 )
        {
            animator.SetTrigger("Die");
            GameEventsManager.Instance.questEvents.EnemyDeath("BlueDragon");
            GameEventsManager.Instance.playerEvents.ExpAdd(100);
            GetComponent<Collider>().enabled = false;
            GetComponent<AudioSource>().enabled = false;
            healthBar.gameObject.SetActive(false);
        }
        else
        {
            animator.SetTrigger("Damage");
        }
    }
}
