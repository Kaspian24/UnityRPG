using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Manages the walking state of a Blue Dragon character within a state machine.
/// This class controls the behavior of the character while it is patrolling, including moving between waypoints and checking for the player's presence.
/// </summary>
public class WalkState : StateMachineBehaviour
{
    float timer;
    List<Transform> wayPoints = new List<Transform>();
    NavMeshAgent agent;
    Transform player;

    /// <summary>
    /// Called when entering the walk state.
    /// Initializes the timer, sets the agent's speed, finds waypoints, and sets an initial destination.
    /// </summary>
    /// <param name="animator">Animator controlling the character's animations.</param>
    /// <param name="stateInfo">Information about the current animation state.</param>
    /// <param name="layerIndex">Index of the animation layer.</param>
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        timer = 0;
        agent.speed = 3.5f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GameObject go = GameObject.FindGameObjectWithTag("WayPoints");

        foreach (Transform t in go.transform) {
            wayPoints.Add(t);
        }

        agent.SetDestination(wayPoints[Random.Range(0,wayPoints.Count)].position);
    }

    /// <summary>
    /// Called on each frame update while the walk state is active.
    /// Moves the character between waypoints and checks for proximity to the player.
    /// </summary>
    /// <param name="animator">Animator controlling the character's animations.</param>
    /// <param name="stateInfo">Information about the current animation state.</param>
    /// <param name="layerIndex">Index of the animation layer.</param>
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(agent.remainingDistance <= agent.stoppingDistance) {
            agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);
        }

        timer += Time.deltaTime;

        if (timer > 10)
        {
            animator.SetBool("isPatrolling", false);
        }

        float distance = Vector3.Distance(player.position, animator.transform.position);

        if (distance < 20)
        {
            animator.SetBool("isChasing", true);
        }
    }

    /// <summary>
    /// Called when exiting the walk state.
    /// Stops the character's movement.
    /// </summary>
    /// <param name="animator">Animator controlling the character's animations.</param>
    /// <param name="stateInfo">Information about the current animation state.</param>
    /// <param name="layerIndex">Index of the animation layer.</param>
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
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
