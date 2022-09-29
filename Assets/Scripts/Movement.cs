using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    public Transform Goal;

    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = Goal.position;
    }
}
