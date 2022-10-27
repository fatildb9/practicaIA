using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class Charge : StateMachineBehaviour
{
    private NavMeshAgent agentNavMesh;
    private Agente agenteScript;

    public float limitSeconds = 10f;
    public float seconds = 0;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh = animator.gameObject.GetComponent<NavMeshAgent>();
        agenteScript = animator.gameObject.GetComponent<Agente>();

        agentNavMesh.speed = 0;
        seconds = 0;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh.speed = 0;

        if (seconds >= limitSeconds)
        {
            Debug.Log("voy a search");
            animator.SetBool("timeToCharge", false);
        }
        else
        {
            seconds = seconds + 1 * Time.deltaTime;
            Debug.Log(seconds);
        }

        agentNavMesh.velocity = agentNavMesh.velocity * 0;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        seconds = 0;

    }
}
