using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<Collider>().enabled = false;
            other.GetComponent<FirstPersonController>().TakeDamage(50);
        }
    }
}
