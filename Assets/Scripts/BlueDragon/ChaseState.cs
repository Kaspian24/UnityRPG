using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Defines the behavior of a Blue Dragon when it is in the chase state.
/// This class is responsible for making the character chase the player using the NavMeshAgent.
/// </summary>
public class RunState : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;

    /// <summary>
    /// Called when entering the running state.
    /// Initializes the NavMeshAgent, finds the player, and sets the agent's speed.
    /// </summary>
    /// <param name="animator">Animator controlling the character's animations.</param>
    /// <param name="stateInfo">Information about the current animation state.</param>
    /// <param name="layerIndex">Index of the animation layer.</param>
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.speed = 5.5f;
    }

    /// <summary>
    /// Called on each frame update while the running state is active.
    /// Updates the destination of the NavMeshAgent to the player's position and checks the distance to manage state transitions.
    /// </summary>
    /// <param name="animator">Animator controlling the character's animations.</param>
    /// <param name="stateInfo">Information about the current animation state.</param>
    /// <param name="layerIndex">Index of the animation layer.</param>
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(player.position);

        float distance = Vector3.Distance(player.position, animator.transform.position);

        if (distance > 20)
        {
            animator.SetBool("isChasing", false);
        }
        if(distance < 5)
        {
            animator.SetBool("isAttacking", true);
        }
    }

    /// <summary>
    /// Called when exiting the running state.
    /// Stops the NavMeshAgent's movement by setting its destination to its current position.
    /// </summary>
    /// <param name="animator">Animator controlling the character's animations.</param>
    /// <param name="stateInfo">Information about the current animation state.</param>
    /// <param name="layerIndex">Index of the animation layer.</param>
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(animator.transform.position);

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
