using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Alarm : StateMachineBehaviour
{
    private NavMeshAgent agentNavMesh;      //referencia al NavMesh del agente 

    private float originalVelocity = 3.5f;  //velocidad del agente

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh = animator.gameObject.GetComponent<NavMeshAgent>();    //referencia al NavMesh del agente

        agentNavMesh.speed = originalVelocity;      //velocidad del agente
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //IR A BASE
        if (Vector3.Distance(agentNavMesh.transform.position, AIDirector.Instance.baseWaypoint.transform.position) < 0.5f)   //si esta a menos de 0,5 de distancia del waypoint base...
        {
            animator.SetTrigger("timeToWait");              //Se va a waiting
        }
        else
        {
            agentNavMesh.destination = AIDirector.Instance.baseWaypoint.transform.position;      //Mientras no esta en el punto se dirige a el 
        }

        MovimientoArena();      //Llamamos al metodo 
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    //MOVIMIENTO POR LA ARENA
    public void MovimientoArena()       //Método de detección de la arena
    {
        int sandMask = 1 << NavMesh.GetAreaFromName("Sand");                        //Detección del area "Sand"
        NavMeshHit hit;
        if (NavMesh.SamplePosition(agentNavMesh.transform.position, out hit, 0.2f, sandMask))    //Si la posición está tocando el area "Sand"...
        {
            if (agentNavMesh.speed == originalVelocity)             //Y si su velocidad es original e igual a la guardada previamente en el start...
            {
                agentNavMesh.speed = agentNavMesh.speed / 2;        //Divide la velocidad entre 2 (reduce un 50% su velocidad)
            }
        }
        else
        {
            agentNavMesh.speed = originalVelocity;                  //Si no está en el area entonces tendrá su velocidad original 
        }
    }
}
