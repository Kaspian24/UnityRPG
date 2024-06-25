using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSkeletonState : StateMachineBehaviour
{
    float timer;
    Transform player;
    GameObject sword;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SkeletonScript skeletonController = animator.GetComponent<SkeletonScript>();
        sword = skeletonController.sword;
        sword.GetComponent<Collider>().isTrigger = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //sword.GetComponent<Collider>().isTrigger = true;

        animator.transform.LookAt(new Vector3(player.position.x, animator.transform.position.y, player.position.z));
        float distance = Vector3.Distance(player.position, animator.transform.position);

        if (distance > 3)
        {
            animator.SetBool("isAttacking1", false);
            animator.SetBool("isAttacking2", false);
        }

    }

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
