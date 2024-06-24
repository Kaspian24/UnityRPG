using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueDragon : MonoBehaviour
{
    public int HP = 100;
    public Animator animator;

    public void TakeDamage(int damage)
    {
        HP -= damage;
        if(HP <= 0 )
        {
            animator.SetTrigger("Die");
            GameEventsManager.Instance.questEvents.EnemyDeath("BlueDragon");
            GameEventsManager.Instance.playerEvents.ExpAdd(100);
            GetComponent<Collider>().enabled = false;
        }
        else
        {
            animator.SetTrigger("Damage");
        }
    }
}
