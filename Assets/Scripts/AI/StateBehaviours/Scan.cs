using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Scan : StateMachineBehaviour
{
    //SCANEA EL OBJETO ENCONTRADO TODO EL RATO (DESTROY EL OBJETO) COMO HAGO PARA QUE PARE DE ESCANEAR LO MISMO  
    public float limitSeconds = 5f;
    public float seconds = 0;

    private NavMeshAgent agentNavMesh;
    float originalVelocity;     //Variable de la velocidad original del agente

    private bool esRoca; 

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh = animator.gameObject.GetComponent<NavMeshAgent>();

        seconds = 0;
        originalVelocity = agentNavMesh.speed;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh.velocity = agentNavMesh.velocity * 0;

        if (seconds >= limitSeconds)
        {
            if (esRoca == true)
            {
                animator.SetBool("timeToCollect", true);
                Debug.Log("lo he visto");
            }
            else
            {
                Debug.Log("voy a search xq no es una roca");
                animator.SetBool("timeToScan", false);
            }
        }
        else
        {
            seconds = seconds + 1 * Time.deltaTime;
            Debug.Log("scan: " + seconds);
        }
       
        RaycastHit hit;
        Vector3 fwd = agentNavMesh.transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(agentNavMesh.transform.position, fwd, out hit, 5f))
        {  
            if (hit.transform.tag == ("Rock"))
            {
                Debug.Log("Entro");
                esRoca = true;
            }
            else
            {
                esRoca = false;
            }
        }
      
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.SetBool("timeToScan", false);
        seconds = 0;
    }
}
