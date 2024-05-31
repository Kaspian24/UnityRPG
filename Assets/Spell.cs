using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        // SprawdŸ, czy zaklêcie trafi³o w coœ innego ni¿ sam pocisk (np. gracza)
        //if (other.gameObject.tag != "Spell")
        //{
            // Jeœli masz efekt trafienia, instancjuj go w miejscu kolizji
            //if (hitEffect != null)
            //{
            //    Instantiate(hitEffect, transform.position, Quaternion.identity);
            //}

            // Zniszcz obiekt zaklêcia
            Destroy(gameObject, 3);
        //}
    }

}
