using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stunned : StateMachineBehaviour
{
    public float limitSeconds = 5f;     //variable que guarda el limite de tiempo en el que esta en el estado 
    public float seconds = 0;           //variable contador de tiempo 

    private NavMeshAgent agentNavMesh;  //referencia al Nav Mesh del agente 

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh = animator.gameObject.GetComponent<NavMeshAgent>();    //referencia al Nav Mesh del agente
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh.speed = 0;     //el agente se para

        //CONTADOR DE SEGUNDOS DE STUNNED
        if (seconds >= limitSeconds)        //si el contador llega a mas que el limite... 
        {
            animator.SetTrigger("timeToSearch");    //Activamos el trigger para ir a search
        }
        else
        {
            seconds = seconds + 1 * Time.deltaTime; //Mientras aumentando el contador 
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
