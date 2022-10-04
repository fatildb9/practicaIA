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

    float originalVelocity;

    void Start()
    {
        nextGoalWaypoint = 0;
        agentNavMesh = GetComponent<NavMeshAgent>();

        originalVelocity = agentNavMesh.speed;
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
        if (NavMesh.SamplePosition(transform.position, out hit, 0.2f, sandMask))
        {
            if (agentNavMesh.speed == originalVelocity)
            {
                agentNavMesh.speed = agentNavMesh.speed / 2;
            }
        }
        else
        {
            //vuelta a la velocidad inicial 
            agentNavMesh.speed = originalVelocity;
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
