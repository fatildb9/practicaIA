using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stunned : StateMachineBehaviour
{
    public float limitSeconds = 5f;
    public float seconds = 0;

    private NavMeshAgent agentNavMesh;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh = animator.gameObject.GetComponent<NavMeshAgent>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("estoySTUNNED");
        agentNavMesh.velocity = agentNavMesh.velocity * 0;

        if (seconds >= limitSeconds)
        {
            Debug.Log("voy a search q me han pegado");
            animator.SetBool("timeToStunned", false);
        }
        else
        {
            seconds = seconds + 1 * Time.deltaTime;
            Debug.Log("stunned: " + seconds);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
