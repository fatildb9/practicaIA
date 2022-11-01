using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class Collect : StateMachineBehaviour
{
    public float limitSeconds = 3f;         //variable que cuenta el limitador de segundos que esta en el estado 
    public float seconds = 0;               //variable contador de segundos

    private NavMeshAgent agentNavMesh;      //referencia al Nav Mesh del agente

    public int inventario = 0;              //variable contador de inventario

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        seconds = 0;                                //reset de segundos del contador 
        animator.SetBool("timeToScan", false);      //el estado Scan termina

        agentNavMesh = animator.gameObject.GetComponent<NavMeshAgent>();    //referencia al Nav Mesh del agente
        agentNavMesh.speed = 0;                                             //el agente se para
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh.speed = 0;         //el agente se para

        //CONTADOR DE SEGUNDOS EN EL ESTADO COLLECT
        if (seconds >= limitSeconds)    //si el contador es mayor o igual al limite de segundos
        {
            inventario++;               //sumamos uno al inventario

            if (inventario == 3)        //Si en el inventario hay 3...
            {
                animator.SetBool("timeToBase", true);       //entonces paasa al estado de base 
            }
            else
            {
                animator.SetBool("timeToCollect", false);   //sino termina el estado de collect y vuelve a Scan 
            }
        }
        else
        {
            seconds = seconds + 1 * Time.deltaTime;         //contador de tiempo
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("timeToCollect", false);   //termina el estado de collect
        seconds = 0;                                //reset de los segundos
    }
}
