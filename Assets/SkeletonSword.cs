using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSword : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            int damageReduced = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().getDefense();
            int damage = 5 - damageReduced;

            GetComponent<Collider>().enabled = false;
            other.GetComponent<FirstPersonController>().TakeDamage(damage <= 0 ? 1: damage);

            StartCoroutine(ResetSwordCollider());
        }
    }

    private IEnumerator ResetSwordCollider()
    {
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(1.2f); // Adjust the duration as needed
        GetComponent<Collider>().enabled = true;
    }
}


