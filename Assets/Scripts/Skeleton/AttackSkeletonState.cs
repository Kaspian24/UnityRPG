using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The AttackSkeletonState class represents the first attack state for a skeleton character in a state machine.
/// This class handles the behavior of the skeleton during the attack state, including activating and deactivating the sword's collider for hit detection.
/// </summary>
public class AttackSkeletonState : StateMachineBehaviour
{
    float timer;
    Transform player;
    GameObject sword;

    /// <summary>
    /// This method is called when the state machine enters the attack state.
    /// It initializes the timer, retrieves the player's Transform, and prepares the sword for the attack.
    /// </summary>
    /// <param name="animator">The Animator component controlling the skeleton's animations.</param>
    /// <param name="stateInfo">Information about the current animator state.</param>
    /// <param name="layerIndex">The index of the current layer in the animator.</param>
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SkeletonScript skeletonController = animator.GetComponent<SkeletonScript>();
        sword = skeletonController.sword;
        sword.GetComponent<Collider>().isTrigger = false;
    }

    /// <summary>
    /// This method is called on each frame update while the state machine is in the attack state.
    /// It updates the timer, manages the sword's collider state, and checks the distance to the player to potentially transition out of the attack state.
    /// </summary>
    /// <param name="animator">The Animator component controlling the skeleton's animations.</param>
    /// <param name="stateInfo">Information about the current animator state.</param>
    /// <param name="layerIndex">The index of the current layer in the animator.</param>
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;

        if (timer > 0.1 && timer < 1)
        {
            sword.GetComponent<Collider>().isTrigger = true;
        }
        else if (timer > 1 && timer < 2)
        {
            sword.GetComponent<Collider>().isTrigger = false;
        }
        else if (timer > 2)
            timer = 0;

        animator.transform.LookAt(new Vector3(player.position.x, animator.transform.position.y, player.position.z));
        float distance = Vector3.Distance(player.position, animator.transform.position);

        if (distance > 3)
        {
            animator.SetBool("isAttacking1", false);
            animator.SetBool("isAttacking2", false);
        }

    }

    /// <summary>
    /// This method is called when the state machine exits the attack state.
    /// It deactivates the sword's collider to prevent unwanted hit detection outside the attack state.
    /// </summary>
    /// <param name="animator">The Animator component controlling the skeleton's animations.</param>
    /// <param name="stateInfo">Information about the current animator state.</param>
    /// <param name="layerIndex">The index of the current layer in the animator.</param>
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        sword.GetComponent<Collider>().isTrigger = false;
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
