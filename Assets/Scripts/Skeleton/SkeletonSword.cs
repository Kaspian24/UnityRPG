using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles collision with player and deals damage.
/// </summary>
public class SkeletonSword : MonoBehaviour
{
    /// <summary>
    /// Triggered when another collider enters this collider.
    /// </summary>
    /// <param name="other">The collider that entered this collider.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            int damageReduced = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().getDefense();
            int damage = 5 - damageReduced;

            GetComponent<Collider>().isTrigger = false;
            other.GetComponent<FirstPersonController>().TakeDamage(damage <= 0 ? 1: damage);
        }
    }
}