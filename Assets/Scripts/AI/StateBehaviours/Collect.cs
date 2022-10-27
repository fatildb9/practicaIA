using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Collect : StateMachineBehaviour
{
    public float limitSeconds = 3f;
    public float seconds = 0;

    public int inventario;

    private NavMeshAgent agentNavMesh;
    float originalVelocity;     //Variable de la velocidad original del agente

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        seconds = 0;
        animator.SetBool("timeToScan", false);

        agentNavMesh = animator.gameObject.GetComponent<NavMeshAgent>();
        originalVelocity = agentNavMesh.speed;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh.velocity = agentNavMesh.velocity * 0;

        if (seconds >= limitSeconds)
        {
            inventario++;

            if (inventario == 3)
            {
                Debug.Log("Me voy a base q estoy lleno");
                animator.SetBool("timeToBase", true);

                inventario = 0;
            }
            else
            {
                Debug.Log("voy a search");
                animator.SetBool("timeToCollect", false);
            }
        }
        else
        {
            seconds = seconds + 1 * Time.deltaTime;
            Debug.Log("collect: " + seconds);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh.speed = originalVelocity;

        seconds = 0;
    }
}
