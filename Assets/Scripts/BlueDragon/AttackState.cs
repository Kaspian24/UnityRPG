using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class representing the attack state in the state machine of the animation.
/// Contains logic to control the Blue Dragon's behavior during an attack.
/// </summary>
public class AttackState : StateMachineBehaviour
{
    float timer;
    Transform player;
    GameObject claw;

    /// <summary>
    /// Called when entering the attack state.
    /// Initializes the timer, finds the player object, and identifies the dragon's claw object.
    /// </summary>
    /// <param name="animator">Animator controlling the character's animations.</param>
    /// <param name="stateInfo">Information about the current animation state.</param>
    /// <param name="layerIndex">Index of the animation layer.</param>
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        claw = GameObject.FindGameObjectWithTag("DragonHand");
    }

    /// <summary>
    /// Called on each frame update while the state is active.
    /// Manages the attack logic, including enabling and disabling the claw's collider, tracking the player, and controlling the distance.
    /// </summary>
    /// <param name="animator">Animator controlling the character's animations.</param>
    /// <param name="stateInfo">Information about the current animation state.</param>
    /// <param name="layerIndex">Index of the animation layer.</param>
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;

        if(timer > 1 && timer < 1.2 ) 
        {
            claw.GetComponent<Collider>().enabled = true;
        }
        else if (timer > 2 && timer < 3.4)
        {
            claw.GetComponent<Collider>().enabled = false;
        }
        else if ( timer > 3.4 )
            timer = 0;

        animator.transform.LookAt(player);
        float distance = Vector3.Distance(player.position, animator.transform.position);

        if (distance > 5)
        {
            animator.SetBool("isAttacking", false);
        }

    }

    /// <summary>
    /// Called when exiting the attack state.
    /// Disables the claw's collider to prevent unintended collisions outside the attack state.
    /// </summary>
    /// <param name="animator">Animator controlling the character's animations.</param>
    /// <param="stateInfo">Information about the current animation state.</param>
    /// <param name="layerIndex">Index of the animation layer.</param>
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        claw.GetComponent<Collider>().enabled = false;
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
