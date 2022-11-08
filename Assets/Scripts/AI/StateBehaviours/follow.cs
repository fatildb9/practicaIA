using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class follow : StateMachineBehaviour
{
    private Agente agenteScript;        //referencia al script de agente
    private NavMeshAgent agentNavMesh;  //referencia al nav mesh del agente 

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agenteScript = animator.gameObject.GetComponent<Agente>();          //referencia al script de agente
        agentNavMesh = animator.gameObject.GetComponent<NavMeshAgent>();    //referencia al nav mesh del agente
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh.destination = AIDirector.Instance.target.position;    //el agente se mueve al objetivo detectado

        //DETECTA LA DISTANCIA
        RaycastHit hit;
        Vector3 fwd = agentNavMesh.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(agentNavMesh.transform.position, fwd * 1f, Color.red);
        if (Physics.Raycast(agentNavMesh.transform.position, fwd, out hit, 1f))     //Si esta a menos de un metro...
        {
            if (hit.transform.tag == "Rover")               //Y el golpeado es un rover...
            {
                animator.SetBool("timeToFollow", false);    //Salimos del estado de Follow y pasamos al Hit
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
