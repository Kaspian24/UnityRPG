using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemy;
    public Color color;

    private void Awake()
    {
        GetComponent<Renderer>().material.color = color;
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            return;
        }
        GameEventsManager.Instance.questEvents.EnemyDeath(enemy);
        GetComponent<Renderer>().material.color = Color.cyan;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
        {
            return;
        }
        GetComponent<Renderer>().material.color = color;
    }
}
