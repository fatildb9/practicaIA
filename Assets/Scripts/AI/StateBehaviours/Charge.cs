using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class Charge : StateMachineBehaviour
{
    private NavMeshAgent agentNavMesh;  //referencia al Nav mesh del agente

    public float limitSeconds = 10f;    //variable limitador de segundos del estado 
    public float seconds = 0;           //variable contador de segundos


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh = animator.gameObject.GetComponent<NavMeshAgent>();    //referencia al Nav mesh del agente

        agentNavMesh.speed = 0;     //hacemos que pare el rover
        seconds = 0;                //reset contador de segundos 
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //CONTADOR DE SEGUNDOS DEL CHARGE 
        if (seconds >= limitSeconds)                        //Si el contador es mayor o igual al limitador...
        {
            animator.SetBool("timeToCharge", false);        //Se va del estado de charge 
        }
        else
        {
            seconds = seconds + 1 * Time.deltaTime;         //contador de segundos
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        seconds = 0;        //reset contador de segundos
    }
}
