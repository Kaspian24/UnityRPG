using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        // Sprawd�, czy zakl�cie trafi�o w co� innego ni� sam pocisk (np. gracza)
        //if (other.gameObject.tag != "Spell")
        //{
            // Je�li masz efekt trafienia, instancjuj go w miejscu kolizji
            //if (hitEffect != null)
            //{
            //    Instantiate(hitEffect, transform.position, Quaternion.identity);
            //}

            // Zniszcz obiekt zakl�cia
            Destroy(gameObject, 3);
        //}
    }

}
