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
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh.speed = 0;     //el rover se queda parado 
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh.speed = 3.5f;  //al salir ponemos la velocidad original del rover
    }

    //MÉTODO DE QUITAR INVENTARIO(quita el invenario) 
    public void QuitarInventario(Animator animator)
    {
        animator.GetBehaviour<Collect>().inventario = 0;    //accedemos a la variable de inventario y la reseteamos 
    }
}
