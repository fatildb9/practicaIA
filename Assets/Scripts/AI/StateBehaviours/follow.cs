using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class follow : StateMachineBehaviour
{
    private Agente agenteScript;
    private NavMeshAgent agentNavMesh;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agenteScript = animator.gameObject.GetComponent<Agente>();
        agentNavMesh = animator.gameObject.GetComponent<NavMeshAgent>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh.destination = agenteScript.target.position;


        RaycastHit hit;
        Vector3 fwd = agentNavMesh.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(agentNavMesh.transform.position, fwd * 1f, Color.red);
        if (Physics.Raycast(agentNavMesh.transform.position, fwd, out hit, 1f))
        {
            if (hit.transform.tag == "Rover")
            {
                animator.SetBool("timeToFollow", false);
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
