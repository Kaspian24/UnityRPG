using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(speed * Time.deltaTime * movement);
    }
}
