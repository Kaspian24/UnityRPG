using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles collision of a player`s sword with different enemy types and deals damage.
/// </summary>
public class Sword : MonoBehaviour
{
    /// <summary>
    /// Triggered when another collider enters this collider.
    /// </summary>
    /// <param name="other">The collider that entered this collider.</param>
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
