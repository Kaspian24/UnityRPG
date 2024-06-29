using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the idle state of a Blue Dragon character in a state machine.
/// This class handles the behavior of the character while it is in the idle state, such as waiting and transitioning to other states based on conditions.
/// </summary>
public class IdleState : StateMachineBehaviour
{
    float timer;
    Transform player;

    /// <summary>
    /// Called when entering the idle state.
    /// Initializes the timer and finds the player's transform.
    /// </summary>
    /// <param name="animator">Animator controlling the character's animations.</param>
    /// <param name="stateInfo">Information about the current animation state.</param>
    /// <param name="layerIndex">Index of the animation layer.</param>
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    /// <summary>
    /// Called on each frame update while the idle state is active.
    /// Updates the timer, checks the distance to the player, and transitions to other states if conditions are met.
    /// </summary>
    /// <param name="animator">Animator controlling the character's animations.</param>
    /// <param name="stateInfo">Information about the current animation state.</param>
    /// <param name="layerIndex">Index of the animation layer.</param>
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if(timer > 5)
        {
            animator.SetBool("isPatrolling", true);
        }

        float distance = Vector3.Distance(player.position, animator.transform.position);

        if(distance < 20) {
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
