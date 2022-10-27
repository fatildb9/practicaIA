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

    private bool esRoca;
    private bool esPlanta;
    private Transform objetoScaneado; 

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh = animator.gameObject.GetComponent<NavMeshAgent>();
        agenteScript = animator.gameObject.GetComponent<Agente>();

        seconds = 0;
        agentNavMesh.speed = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh.speed = 0;

        if (seconds >= limitSeconds)
        {
            if (agenteScript.transform.name == "Grumpy" || agenteScript.transform.name == "Happy")
            {
                if (esPlanta == true)
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
            else if (agenteScript.transform.name == "Dopey")
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
            agenteScript.objetoScaneadoScan = hit.transform;

            if (agenteScript.transform.name == "Grumpy" || agenteScript.transform.name == "Happy")
            {
                if (hit.transform.tag == ("Planta"))
                {
                    Debug.Log("PLANTAAA");
                    esPlanta = true;
                    esRoca = false;
                }
                else
                {
                    esPlanta = false;
                    esRoca = false;
                }
            }
            else if (agenteScript.transform.name == "Dopey")
            {
                if (hit.transform.tag == ("Rock"))
                {
                    Debug.Log("Entro");
                    esRoca = true;
                    esPlanta = false;
                }
                else
                {
                    esPlanta = false;
                    esRoca = false;
                }
            }

        }
    }
    
    /* esto es algo de la rotacion
    x += Time.deltaTime* 10;
        transform.rotation = Quaternion.Euler(x,0,0);*/

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        seconds = 0;
    }
}
