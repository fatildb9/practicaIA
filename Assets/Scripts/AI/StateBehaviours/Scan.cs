using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Scan : StateMachineBehaviour
{
    public float limitSeconds = 5f;
    public float seconds = 0;

    private NavMeshAgent agentNavMesh;
    float originalVelocity;     //Variable de la velocidad original del agente

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh = animator.gameObject.GetComponent<NavMeshAgent>();

        seconds = 0;
        originalVelocity = agentNavMesh.speed;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        seconds = seconds + 1 * Time.deltaTime;
        Debug.Log(seconds);

        agentNavMesh.velocity = agentNavMesh.velocity * 0;

        RaycastHit hit;
        Vector3 fwd = agentNavMesh.transform.TransformDirection(Vector3.forward);
        //Ray ray = new Ray(agentNavMesh.transform.position, fwd);

        if (Physics.Raycast(agentNavMesh.transform.position, fwd, out hit, 5f))
        {  
            Debug.Log("Entro");
            if (seconds >= limitSeconds)
            {
                if (hit.transform.tag == ("Rock"))
                {
                    Debug.Log("voy a collect");
                    animator.SetBool("timeToCollect", true);

                    Debug.Log("lo he visto");
                }
                else
                {
                    Debug.Log("voy a search xq no es una roca");
                    animator.SetBool("timeToScan", false);
                }
            }
        }
      
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        seconds = 0;
    }
}
