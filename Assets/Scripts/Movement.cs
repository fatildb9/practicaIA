using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    public Transform[] goalWaypoints;                           //Array con los diferentes objetivos de los agentes
    private int nextGoalWaypoint = 0;                           //Número de array por el que va el agente

    NavMeshAgent agentNavMesh;  //Referencia al NavMesh de los agentes
    float originalVelocity;     //Variable de la velocidad original del agente

    void Start()
    {
        nextGoalWaypoint = 0;                               //Número por el que va el array
        agentNavMesh = GetComponent<NavMeshAgent>();        //Referencia al componente del agente

        originalVelocity = agentNavMesh.speed;              //Guarda la velocidad del agente en la variable
    }

    private void Update()
    {
        PatrullaAgente();       //Busca la patrulla
        MovimientoArena();      //Busca si está tocando la arena

        agentNavMesh.destination = goalWaypoints[nextGoalWaypoint].position;        //recalcula la ruta de manera continua
    }

  
    public void MovimientoArena()       //Método de detección de la arena
    {        
        int sandMask = 1 << NavMesh.GetAreaFromName("Sand");                        //Detección del area "Sand"
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 0.2f, sandMask))    //Si la posición está tocando el area "Sand"...
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
