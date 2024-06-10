using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject, 3);

        if(other.tag == "Dragon")
        {
            other.GetComponent<BlueDragon>().TakeDamage(10);
        }
    }

}
