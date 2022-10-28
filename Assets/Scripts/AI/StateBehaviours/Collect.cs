using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class Collect : StateMachineBehaviour
{
    public float limitSeconds = 3f;
    public float seconds = 0;

    private NavMeshAgent agentNavMesh;
    private Agente agenteScript;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        seconds = 0;
        animator.SetBool("timeToScan", false);

        agentNavMesh = animator.gameObject.GetComponent<NavMeshAgent>();
        agenteScript = animator.gameObject.GetComponent<Agente>();
        agentNavMesh.speed = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh.speed = 0;

        if (seconds >= limitSeconds)
        {
            agenteScript.inventario++;

            if (agenteScript.inventario == 3)
            {
                Debug.Log("Me voy a base q estoy lleno");
                animator.SetBool("timeToBase", true);
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
