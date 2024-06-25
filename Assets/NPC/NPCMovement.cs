using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public Transform[] points;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;

        // Spróbuj umieścić agenta na NavMesh
        PlaceAgentOnNavMesh();

        // Rozpocznij ruch agenta
        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;

        if (agent.isOnNavMesh)
        {
            int randomPoint = Random.Range(0, points.Length);
            agent.destination = points[randomPoint].position;
        }
        else
        {
            Debug.LogWarning("Agent is not on the NavMesh during GotoNextPoint.");
        }
    }

    void Update()
    {
        if (agent.isOnNavMesh)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
        }
        else
        {
            PlaceAgentOnNavMesh();
        }
    }

    void PlaceAgentOnNavMesh()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 5.0f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
            agent.Warp(hit.position);
            Debug.Log("Agent placed on NavMesh at position: " + hit.position);
        }
        else
        {
            
        }
    }
}
