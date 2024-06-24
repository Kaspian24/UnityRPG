using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : MonoBehaviour
{
    public int HP = 20;
    public Animator animator;

    public void TakeDamage(int damage)
    {
        int randomNumber = Random.Range(1, 3);
        HP -= damage;
        if (HP <= 0)
        {
            if (randomNumber == 1)
            {
                animator.SetTrigger("Die1");
                Destroy(gameObject, 2);
            }
            else
            {
                animator.SetTrigger("Die2");
                Destroy(gameObject, 2);
            }
            GameEventsManager.Instance.questEvents.EnemyDeath("Skeleton");
            GameEventsManager.Instance.playerEvents.ExpAdd(10);
            GetComponent<Collider>().enabled = false;
        }   
    }
}
