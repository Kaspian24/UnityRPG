using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseSkeletonState : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.speed = 2.5f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(agent != null)
        {
            agent.SetDestination(player.position);
        }

        float distance = Vector3.Distance(player.position, animator.transform.position);

        if (distance > 20)
        {
            animator.SetBool("isChasing", false);
        }

        int randomAttack = Random.Range(1, 3);

        if (distance < 3)
        {
            if(randomAttack == 1)
            {
                animator.SetBool("isAttacking1", true);
                animator.SetBool("isAttacking2", false);
            }
            else
            {
                animator.SetBool("isAttacking1", false);
                animator.SetBool("isAttacking2", true);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent != null)
        {
            agent.ResetPath(); // Zresetuj trasê agenta, aby przesta³ pod¹¿aæ za celem
            agent.velocity = Vector3.zero; // Zatrzymaj ruch agenta
        }
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
