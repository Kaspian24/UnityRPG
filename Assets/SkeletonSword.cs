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
            GetComponent<Collider>().enabled = false;
            other.GetComponent<FirstPersonController>().TakeDamage(5);

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


