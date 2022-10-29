using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class hit : StateMachineBehaviour
{
    private Agente agenteScript;
    private NavMeshAgent agentNavMesh;

    public Transform happyRover;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agenteScript = animator.gameObject.GetComponent<Agente>();
        agentNavMesh = animator.gameObject.GetComponent<NavMeshAgent>();

        agenteScript.transform.name = "Happy";
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        RaycastHit hit;
        Vector3 fwd = agentNavMesh.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(agentNavMesh.transform.position, fwd * 1f, Color.red);
        if (Physics.Raycast(agentNavMesh.transform.position, fwd, out hit, 1f))
        {
            if (hit.transform.tag == "Rover")
            {
                //animator.GetBehaviour<Stunned>();
                animator.SetBool("timeToStunned", true);
                animator.SetBool("timeToBase", true);
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
