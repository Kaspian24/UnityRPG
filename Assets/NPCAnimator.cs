using UnityEngine;
using UnityEngine.AI;

public class NPCAnimationController : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        animator.SetFloat("MoveSpeed", agent.velocity.magnitude);

        Vector3 velocity = agent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float direction = Mathf.Atan2(localVelocity.x, localVelocity.z) * Mathf.Rad2Deg;
        animator.SetFloat("Direction", direction);
    }
}
