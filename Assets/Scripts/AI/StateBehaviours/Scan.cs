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

    public float startAngle;
    public float angleRotation;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh = animator.gameObject.GetComponent<NavMeshAgent>();
        agenteScript = animator.gameObject.GetComponent<Agente>();

        seconds = 0;
        agentNavMesh.speed = 0;
        
        startAngle = agentNavMesh.transform.rotation.y;
        //angleRotation = startAngle + 360;
        angleRotation = -startAngle;
        Debug.Log(startAngle);
        Debug.Log(angleRotation);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh.speed = 0;

        if (agentNavMesh.transform.name == "Happy")
        {
            if (startAngle >= angleRotation)
            {
                Debug.Log("VUELTICAAAAA");
            }
            else
            {
                float x = angleRotation % limitSeconds;
                //float x = startAngle +360;
                //x += Time.deltaTime * 5;
                //agenteScript.transform.rotation = Quaternion.Euler(0, x, 0);
                agentNavMesh.angularSpeed = x;
                agentNavMesh.transform.Rotate(new Vector3(0, 360f * Time.deltaTime, 0));
                startAngle = agentNavMesh.transform.rotation.y;
                Debug.Log(startAngle);

            }



            /*float angleRotation = startAngle + 360;
            float angle = angleRotation % limitSeconds;

            if (startAngle == angleRotation)
            {

            }
            else
            {
                startAngle += seconds * angle;
                agenteScript.transform.rotation = Quaternion.Euler(0, startAngle, 0);
            }*/

            //agenteScript.transform.rotation += Time.deltaTime * limitSeconds;
            //giro += Time.deltaTime * limitSeconds;
            //agenteScript.transform.Rotate(new Vector3(0, velocityRotation, 0));

            //agenteScript.transform.rotation = Quaternion.Euler(0, angleRotation , 0);
        }
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
            //Debug.Log("scan: " + seconds);
        }

        RaycastHit hit;
        Vector3 fwd = agentNavMesh.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(agentNavMesh.transform.position, fwd * 5f, Color.red);
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

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        seconds = 0;
    }
}
