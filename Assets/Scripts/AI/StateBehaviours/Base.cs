using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Base : StateMachineBehaviour
{
    private NavMeshAgent agentNavMesh;
    private Agente agenteScript;

    private float originalVelocity = 3.5f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh = animator.gameObject.GetComponent<NavMeshAgent>();
        agenteScript = animator.gameObject.GetComponent<Agente>();

        agentNavMesh.speed = 3.5f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(agentNavMesh.transform.position, agenteScript.baseWaypoint.position) < 0.5f)
        {
            agenteScript.inventario = 0;
            animator.SetBool("timeToBase", false);
            Debug.Log("llegue a base");
        }
        else
        {
            //sino est� dentro de esta distancia seguir� su camino hacia el objetivo
            agentNavMesh.destination = agenteScript.baseWaypoint.position;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public void MovimientoArena()       //M�todo de detecci�n de la arena
    {
        int sandMask = 1 << NavMesh.GetAreaFromName("Sand");                        //Detecci�n del area "Sand"
        NavMeshHit hit;
        if (NavMesh.SamplePosition(agenteScript.transform.position, out hit, 0.2f, sandMask))    //Si la posici�n est� tocando el area "Sand"...
        {
            if (agentNavMesh.speed == originalVelocity)             //Y si su velocidad es original e igual a la guardada previamente en el start...
            {
                agentNavMesh.speed = agentNavMesh.speed / 2;        //Divide la velocidad entre 2 (reduce un 50% su velocidad)
            }
        }
        else
        {
            agentNavMesh.speed = originalVelocity;                  //Si no est� en el area entonces tendr� su velocidad original 
        }
    }
}
