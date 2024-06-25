using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueDragon : MonoBehaviour
{
    public int HP = 100;
    public Animator animator;

    public HealthBar healthBar;

    public void Start()
    {
        healthBar.setMaxHealth(HP);
        healthBar.setHealth(HP);
    }

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
            healthBar.gameObject.SetActive(false);
        }
        else
        {
            animator.SetTrigger("Damage");
        }
    }
}
