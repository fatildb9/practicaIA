using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class Search : StateMachineBehaviour
{
    private NavMeshAgent agentNavMesh;
    private Agente agenteScript;

    public float limitSeconds = 30f;
    public float seconds = 0;

    private int timeToCharge;

    float originalVelocity;     //Variable de la velocidad original del agente

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh = animator.gameObject.GetComponent<NavMeshAgent>();
        agenteScript = animator.gameObject.GetComponent<Agente>();
        
        originalVelocity = agentNavMesh.speed;                          //Guarda la velocidad del agente en la variable 
        seconds = 0;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*for (int i = 0; i < limitSeconds; i++)
        {
            seconds = i * Time.deltaTime;
            Debug.Log(seconds);
        }
        animator.SetFloat("timeToCharge", seconds);*/

        PatrullaAgente();       //Busca la patrulla
        MovimientoArena();      //Busca si est� tocando la arena


        seconds = seconds + 1 * Time.deltaTime;
        Debug.Log(seconds);

        if (seconds >= limitSeconds)
        {
            Debug.Log("voy a charge");
            animator.SetFloat("timeToCharge", seconds);
        }




        /*RAYCAST
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, 10))
            print("There is something in front of the object!");*/
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        seconds = 0; 
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


    //M�todo de patrulla de movimiento del agente 
    public void PatrullaAgente()
    {
        //si esta en una distancia del 0,2 de cerca del objetivo...
        if (Vector3.Distance(agenteScript.transform.position, agenteScript.PatrolPoints[agenteScript.nextWaypoint].position) < 0.2f)
        {
            //se dirigir� al siguiente objetivo 
            agenteScript.nextWaypoint = (agenteScript.nextWaypoint + 1) % agenteScript.PatrolPoints.Length;
            Debug.Log("aaaaaaaa");
        }
        else
        {
            //sino est� dentro de esta distancia seguir� su camino hacia el objetivo
            agentNavMesh.destination = agenteScript.PatrolPoints[agenteScript.nextWaypoint].position;
        }
    }
}