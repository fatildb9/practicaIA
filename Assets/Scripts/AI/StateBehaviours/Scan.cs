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
    private Agente agenteScript;
    float originalVelocity;     //Variable de la velocidad original del agente

    private bool esRoca;
    private Transform objetoScaneado; 

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh = animator.gameObject.GetComponent<NavMeshAgent>();
        agenteScript = animator.gameObject.GetComponent<Agente>();

        seconds = 0;
        originalVelocity = agentNavMesh.speed;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh.velocity = agentNavMesh.velocity * 0;

        RaycastHit hit;
        Vector3 fwd = agentNavMesh.transform.TransformDirection(Vector3.forward);

        if (seconds >= limitSeconds)
        {
            if (Physics.Raycast(agentNavMesh.transform.position, fwd, out hit, 5f) && objetoScaneado != hit.transform)
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

                objetoScaneado = hit.transform;
            }

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

        
      
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        seconds = 0;
    }
}
