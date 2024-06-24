using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        int damage =  GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().getStrenght();

        if (other.tag == "Dragon")
        {
            GetComponent<Collider>().enabled = false;
            other.GetComponent<BlueDragon>().TakeDamage(damage);
        }
        if (other.tag == "Skeleton")
        {
            GetComponent<Collider>().enabled = false;
            other.GetComponent<SkeletonScript>().TakeDamage(damage);
        }
    }
}
