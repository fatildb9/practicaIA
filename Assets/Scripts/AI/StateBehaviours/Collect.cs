using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class Collect : StateMachineBehaviour
{
    public float limitSeconds = 3f;
    public float seconds = 0;

    public int inventario;

    private NavMeshAgent agentNavMesh;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        seconds = 0;
        animator.SetBool("timeToScan", false);

        agentNavMesh = animator.gameObject.GetComponent<NavMeshAgent>();
        agentNavMesh.speed = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh.speed = 0;

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
        animator.SetBool("timeToCollect", false);
        seconds = 0;
    }
}
