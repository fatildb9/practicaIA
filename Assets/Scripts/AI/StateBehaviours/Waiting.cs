using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Waiting : StateMachineBehaviour
{
    private NavMeshAgent agentNavMesh;  //referencia al Nav mesh del agente

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh = animator.gameObject.GetComponent<NavMeshAgent>();    //referencia al Nav mesh del agente

        agentNavMesh.speed = 0;     //hacemos que pare el rover

        int baseMask = 1 << NavMesh.GetAreaFromName("Base");                        //Detección del area "Base"
        NavMeshHit hit;
        if (!NavMesh.SamplePosition(agentNavMesh.transform.position, out hit, 0.2f, baseMask))    //Si la posición está tocando el area "Base"...
        {
            animator.GetBehaviour<Collect>().inventario = 0;
            Debug.Log("reset inventario");
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
