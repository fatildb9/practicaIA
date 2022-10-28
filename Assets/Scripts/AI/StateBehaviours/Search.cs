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

    float originalVelocity = 3.5f;     //Variable de la velocidad original del agente
    private Transform objetoScaneado;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh = animator.gameObject.GetComponent<NavMeshAgent>();
        agenteScript = animator.gameObject.GetComponent<Agente>();
        
        seconds = 0;
        originalVelocity = 3.5f;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        PatrullaAgente();       //Busca la patrulla
        MovimientoArena();      //Busca si está tocando la arena

        if (seconds >= limitSeconds)
        {
            Debug.Log("voy a charge");
            animator.SetBool("timeToCharge", true);
        }
        else
        {
            seconds = seconds + 1 * Time.deltaTime;
            //Debug.Log(seconds);
        }

        RaycastHit hit;
        Vector3 fwd = agentNavMesh.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(agentNavMesh.transform.position, fwd * 5f, Color.red);
        if (Physics.Raycast(agentNavMesh.transform.position, fwd , out hit , 5f))
        {
            if (agenteScript.objetoScaneado != hit.transform)
            {
                if(hit.transform.tag != "noScan")
                {
                    if (agenteScript.transform.name == "Grumpy" && hit.transform.tag == "Rover")
                    {
                        agenteScript.target = hit.transform;
                        animator.SetBool("timeToFollow", true);
                    }
                    else
                    {
                        agenteScript.objetoScaneado = hit.transform;
                        Debug.Log("HE VISTO ALGO");
                        animator.SetBool("timeToScan", true);
                    }                    
                }
            }        
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        seconds = 0; 
    }




    public void MovimientoArena()       //Método de detección de la arena
    {
        int sandMask = 1 << NavMesh.GetAreaFromName("Sand");                        //Detección del area "Sand"
        NavMeshHit hit;
        if (NavMesh.SamplePosition(agenteScript.transform.position, out hit, 0.2f, sandMask))    //Si la posición está tocando el area "Sand"...
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


    //Método de patrulla de movimiento del agente 
    public void PatrullaAgente()
    {
        //si esta en una distancia del 0,2 de cerca del objetivo...
        if (Vector3.Distance(agentNavMesh.transform.position, agenteScript.PatrolPoints[agenteScript.nextWaypoint].position) < 0.5f)
        {
            //se dirigirá al siguiente objetivo 
            agenteScript.nextWaypoint = (agenteScript.nextWaypoint + 1) % agenteScript.PatrolPoints.Length;
            Debug.Log("aaaaaaaa");
        }
        else
        {
            //sino está dentro de esta distancia seguirá su camino hacia el objetivo
            agentNavMesh.destination = agenteScript.PatrolPoints[agenteScript.nextWaypoint].position;
        }

    }
}
