using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dragon")
        {
            GetComponent<Collider>().enabled = false;
            other.GetComponent<BlueDragon>().TakeDamage(10);
        }
        if (other.tag == "Skeleton")
        {
            GetComponent<Collider>().enabled = false;
            other.GetComponent<SkeletonScript>().TakeDamage(10);
        }
    }
}
