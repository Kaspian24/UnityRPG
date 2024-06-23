using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    private Rigidbody rb;
    private Collider col;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dragon")
        {
            col.enabled = false;
            other.GetComponent<BlueDragon>().TakeDamage(10);
            StopSpell();
            Destroy(gameObject, 3);
        }
        else if (other.tag == "Skeleton")
        {
            col.enabled = false;
            other.GetComponent<SkeletonScript>().TakeDamage(10);
            StopSpell();
            Destroy(gameObject, 0.75f);
        }
        else
        {
            Destroy(gameObject, 3);
        }
    }

    private void StopSpell()
    {
        if (rb != null)
        {
            rb.velocity = Vector3.zero; // Zatrzymaj ruch
            rb.isKinematic = true; // Wy��cz fizyk�
        }
    }

}
