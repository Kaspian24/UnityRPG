using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : MonoBehaviour
{
    public int HP = 40;
    public Animator animator;
    public GameObject sword;

    public GameObject potionPrefab;
    public Transform dropPoint;

    public HealthBar healthBar;

    public void Start()
    {
        healthBar.setMaxHealth(HP);
        healthBar.setHealth(HP);
    }

    public void TakeDamage(int damage)
    {
        int randomNumber = Random.Range(1, 3);
        HP -= damage;
        healthBar.setHealth(HP);

        if (HP <= 0)
        {
            DropPotion();

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
            healthBar.gameObject.SetActive(false);
        }   
    }

    private void DropPotion()
    {
        int randomNumber = Random.Range(1, 4);
        if (randomNumber == 1)
        {
            Debug.Log("Drop");
            Instantiate(potionPrefab, dropPoint.position, Quaternion.identity);
        }
    }
}
