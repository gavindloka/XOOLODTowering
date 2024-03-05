using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : StateMachineBehaviour
{
    float timer;
    List<Transform> wayPoints = new List<Transform>();
    NavMeshAgent characterAgent;

    Transform player;
    float chaseRange = 30f;
    float walkRange = 10f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        characterAgent = animator.GetComponent<NavMeshAgent>();
        characterAgent.speed = 7f;
        timer = 0;
        //GameObject go = GameObject.FindGameObjectWithTag("Waypoints");
        //foreach(Transform t in go.transform) wayPoints.Add(t);
        Vector2 randomPoint = Random.insideUnitCircle * walkRange; //return vector2 1 smpe -1
        Vector3 target = animator.transform.position + new Vector3(randomPoint.x, 0, randomPoint.y);
        target.y = animator.transform.position.y;
        characterAgent.SetDestination(target);

        //characterAgent.SetDestination(wayPoints[Random.Range(0,wayPoints.Count)].position);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (characterAgent.remainingDistance <=characterAgent.stoppingDistance)
        {
            Vector2 randomPoint = Random.insideUnitCircle * walkRange; //return vector2 1 smpe -1
            Vector3 target = animator.transform.position + new Vector3(randomPoint.x, 0, randomPoint.y);
            target.y = animator.transform.position.y;
            characterAgent.SetDestination(target);
            //characterAgent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);
        }
        timer += Time.deltaTime;
        if (timer > 7) animator.SetBool("isPatrol", false);

        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance < chaseRange) animator.SetBool("isChasing", true);
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        characterAgent.SetDestination(characterAgent.transform.position);
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
