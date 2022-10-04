using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    //Array con los diferentes objetivos de los agentes
    public Transform[] goalWaypoints;
    //numero de array por el que va el agente
    private int nextGoalWaypoint = 0;


    //referencia al NavMesh de los agentes
    NavMeshAgent agentNavMesh;

    private bool pasarArena;

    void Start()
    {
        nextGoalWaypoint = 0;
        agentNavMesh = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        PatrullaAgente();
        MovimientoArena();
    }

    public void MovimientoArena()
    {        
        // Find nearest point on water.
        int sandMask = 1 << NavMesh.GetAreaFromName("Sand");
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 0.5f, sandMask))
        {
            //Reducción de velocidad de la arena
            pasarArena = true;
            
            if (pasarArena == true)
            {
                agentNavMesh.speed = agentNavMesh.speed / 2;
                pasarArena = false;
            }
            else
            {

            }
            

        }
        else
        {
            //vuelta a la velocidad inicial 
            agentNavMesh.speed = 3.5f;
        }
    }

    public void PatrullaAgente()
    {
        //Patrulla de movimiento del agente
        //si esta en una distancia del 0,2 de cerca del objetivo...
        if (Vector3.Distance(transform.position, goalWaypoints[nextGoalWaypoint].position) < 0.5f)
        {
            //se dirigirá al siguiente objetivo 
            nextGoalWaypoint = (nextGoalWaypoint + 1) % goalWaypoints.Length;
            
        }
        else
        {
            //sino está dentro de esta distancia seguirá su camino hacia el objetivo
            agentNavMesh.destination = goalWaypoints[nextGoalWaypoint].position;
        }
    }
}
