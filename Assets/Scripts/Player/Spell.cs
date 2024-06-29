using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that handles player`s spell behavior, including collision detection and damage application.
/// </summary>
public class Spell : MonoBehaviour
{
    private Rigidbody rb;
    private Collider col;

    /// <summary>
    /// Initializes Rigidbody and Collider components.
    /// </summary>
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    /// <summary>
    /// Handles collision detection with other colliders.
    /// </summary>
    /// <param name="other">Collider that triggered the collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        int damage = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().getMagic();

        if (other.tag == "Dragon")
        {
            col.enabled = false;
            other.GetComponent<BlueDragon>().TakeDamage(damage);
            StopSpell();
            Destroy(gameObject, 1);
        }
        else if (other.tag == "Skeleton")
        {
            col.enabled = false;
            other.GetComponent<SkeletonScript>().TakeDamage(damage);
            StopSpell();
            Destroy(gameObject, 0.75f);
        }
        else if (other.tag == "Player")
        {
        }
        else
        {
            col.enabled = false;
            StopSpell();
            Destroy(gameObject, 1);
        }
    }

    /// <summary>
    /// Stops the spell's movement and physics.
    /// </summary>
    private void StopSpell()
    {
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }

}
