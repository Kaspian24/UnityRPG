using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<SphereCollider>() != null)
                {
                    Debug.Log("Interakcja z NPC!");
                }
            }
        }
    }
}
