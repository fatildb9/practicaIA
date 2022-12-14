using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class Search : StateMachineBehaviour
{
    private NavMeshAgent agentNavMesh;      //referencia al Nav Mesh del agente 

    public float limitSeconds = 30f;        //limite de segundos que est? en este estado
    public float seconds = 0;               //variable contador de segundos

    float originalVelocity = 3.5f;          //Variable de la velocidad original del agente

    public int nextWaypoint = 0;        //N?mero de array por el que va el agente

    public Transform objetoScaneado;        //variable para ver que objeto ha escaneado 
    public Transform target;            //variable para seguir el objetivo

    public GameObject[] PatrolPoints;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        if (PatrolPoints.Length == 0)       //Si el array no esta lleno
        {   
            PatrolPoints = AIDirector.Instance.AddWaypoint();       //Llamamos al metodo del director de a?adir puntos
        }


        agentNavMesh = animator.GetComponent<NavMeshAgent>();        //referencia al Nav Mesh del agente
        
        seconds = 0;                                                            //comenzamos con 0 segundos
        originalVelocity = 3.5f;                                                //velocidad inicial del agente
        agentNavMesh.speed = originalVelocity;                                  //asociamos la velocidad al speed del Nav Mesh
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PatrullaAgente();                               //Busca la patrulla
        MovimientoArena();                              //Busca si est? tocando la arena

        //CONTADOR SEGUNDOS PARA IR A CHARGE
        if (seconds >= limitSeconds)                    //Si los segundos son mayores que el limite...
        {
            animator.SetBool("timeToCharge", true);     //Pasa al parametro a true y va al estado de charge
        }
        else
        {
            seconds = seconds + 1 * Time.deltaTime;     //Mientras va contando los segundos
        }

        //RAYCAST PARA DETECTAR TODO LO QUE SE ENCUENTRE EN EL MAPA
        RaycastHit hit;
        Vector3 fwd = agentNavMesh.transform.TransformDirection(Vector3.forward);       //Vector hacia delante 
        Debug.DrawRay(agentNavMesh.transform.position, fwd * 5f, Color.red);            //Muestra el vector anterior
        
        if (Physics.Raycast(agentNavMesh.transform.position, fwd , out hit , 5f))       //Si detecta algo a 5 metros...
        {
            if (objetoScaneado != hit.transform)                                           //Y no es un objeto escaneado previamente
            {   
                if(hit.transform.tag != "noScan")                                                       //Y no es algo que no pueda escanear...
                {
                    //if (agenteScript.transform.name == "Grumpy" && hit.transform.tag == "Rover")        //Si es grumpy y el objeto escaneado es un rover
                    if (agentNavMesh.transform.name == "Grumpy" && hit.transform.tag == "Rover")        //Si es grumpy y el objeto escaneado es un rover
                    {
                        target = hit.transform;                                            //Guardamos la posicion del objeto (rover) en el objetivo
                        animator.SetBool("timeToFollow", true);                                         //Y pasa al estado de Follow
                    }
                    else
                    {
                        objetoScaneado = hit.transform;                                    //Guardamos el objeto en una variable 
                        animator.SetBool("timeToScan", true);                                           //Pasa al estado de Scan
                    }                    
                }
            }        
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        seconds = 0;        //Reseteamos los segundos 
    }

    //METODO DE DETECCI?N DE ARENA  
    public void MovimientoArena()       
    {
        int sandMask = 1 << NavMesh.GetAreaFromName("Sand");                                //Detecci?n del area "Sand"
        NavMeshHit hit;
        if (NavMesh.SamplePosition(agentNavMesh.transform.position, out hit, 0.2f, sandMask))    //Si la posici?n est? tocando el area "Sand"...
        {
            if (agentNavMesh.speed == originalVelocity)                                         //Y si su velocidad es original e igual a la guardada previamente en el start...
            {
                agentNavMesh.speed = agentNavMesh.speed / 2;        //Divide la velocidad entre 2 (reduce un 50% su velocidad)
            }
        }
        else
        {
            agentNavMesh.speed = originalVelocity;                  //Si no est? en el area entonces tendr? su velocidad original 
        }
    }

    //METODO DE PATRULLA Y MOVIMIENTO DEL AGENTE 
    public void PatrullaAgente()
    {
        //si esta en una distancia del 0,5 de cerca del objetivo...
        if (Vector3.Distance(agentNavMesh.transform.position, PatrolPoints[nextWaypoint].transform.position) < 0.5f)
        {
            //se dirigir? al siguiente objetivo 
            nextWaypoint = (nextWaypoint + 1) % PatrolPoints.Length;
        }
        else
        {
            //sino est? dentro de esta distancia seguir? su camino hacia el objetivo
            agentNavMesh.destination = PatrolPoints[nextWaypoint].transform.position;
        }
    }
}
