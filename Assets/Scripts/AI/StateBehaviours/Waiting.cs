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

        
        /* MANERA DE QUITAR INVENTARIO
        int baseMask = 1 << NavMesh.GetAreaFromName("Base");                        //Detecci�n del area "Base"
        NavMeshHit hit;
        if (!NavMesh.SamplePosition(agentNavMesh.transform.position, out hit, 0.2f, baseMask))    //Si la posici�n est� tocando el area "Base"...
        {
            animator.GetBehaviour<Collect>().inventario = 0;
            Debug.Log("reset inventario");
        }*/
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh.speed = 0;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh.speed = 3.5f;
    }

    //M�TODO DE QUITAR INVENTARIO
    public void QuitarInventario(Animator animator)
    {
        Debug.Log("INVENTARIO");
        animator.GetBehaviour<Collect>().inventario = 0;
    }
}
