using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the logic for the claw attack of a Blue Dragon.
/// This script manages detecting collisions with the player and applying damage accordingly.
/// </summary>
public class ClawAttack : MonoBehaviour
{
    /// <summary>
    /// Called when another collider enters the trigger collider attached to this object.
    /// Checks if the collider belongs to the player and applies damage considering the player's defense.
    /// </summary>
    /// <param name="other">The collider of the object that entered the trigger.</param>
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
