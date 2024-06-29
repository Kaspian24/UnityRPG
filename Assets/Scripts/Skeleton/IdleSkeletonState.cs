using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the idle state behavior of a skeleton enemy.
/// </summary>
public class IdleSkeletonState : StateMachineBehaviour
{
    Transform player;

    /// <summary>
    /// Initializes the player reference when the state is entered.
    /// </summary>
    /// <param name="animator">The Animator component associated with the skeleton.</param>
    /// <param name="stateInfo">Current state information.</param>
    /// <param name="layerIndex">Layer index.</param>
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    /// <summary>
    /// Checks the distance to the player and transitions to chase state if within range.
    /// </summary>
    /// <param name="animator">The Animator component associated with the skeleton.</param>
    /// <param name="stateInfo">Current state information.</param>
    /// <param name="layerIndex">Layer index.</param>
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector3.Distance(player.position, animator.transform.position);

        if (distance < 20)
        {
            animator.SetBool("isChasing", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //}

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
