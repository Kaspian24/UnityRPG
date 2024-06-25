using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            int damageReduced = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().getDefense();
            int damage = 20 - damageReduced;

            GetComponent<Collider>().enabled = false;
            other.GetComponent<FirstPersonController>().TakeDamage(damage <= 0 ? 1 : damage);
        }
    }
}
