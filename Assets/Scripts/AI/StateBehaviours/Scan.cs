using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Scan : StateMachineBehaviour
{
    public float limitSeconds = 5f;         //limite de segundos que está en este estado
    public float seconds = 0;               //variable contador de segundos 

    private NavMeshAgent agentNavMesh;      //referencia al Nav Mesh del agente
    private Agente agenteScript;            //referencia al script (contenedor) de agente

    private bool esRoca;                    //variable que decide si es una roca
    private bool esPlanta;                  //variable que decide si es una planta

    private bool startScan;                 //variable para empezar a escanear

   
    Quaternion Inicoangle;                  //variable que obtiene la rotacion 
    public Vector3 startAngleRot;           //angulo inicial del agente 
    public Vector3 finishAngleRot;          //angulo final en el que debe terminar

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh = animator.gameObject.GetComponent<NavMeshAgent>();        //referencia al Nav Mesh del agente
        agenteScript = animator.gameObject.GetComponent<Agente>();              //referencia al script (contenedor) de agente

        seconds = 0;                //reseteo de segundos
        agentNavMesh.speed = 0;     //el agente se para
        
        Inicoangle = agentNavMesh.transform.rotation;               //recogemos en la variable la rotacion 
        startAngleRot = Inicoangle.eulerAngles;                     
        finishAngleRot = startAngleRot + new Vector3(0, 360, 0);    //le sumamos 360 grados al eje y para que de una vuelta

        startScan = false;          //ponemos la variable en false para que no empiece con el scan 
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh.speed = 0;     //para que el agente este parado 

        //SI ES HAPPY DA UNA VUELTA ANTES
        if (agentNavMesh.transform.name == "Happy")
        {
            if (startAngleRot.y >= finishAngleRot.y)        //Si el angulo es mayor o igual que el angulo final...
            {       
                startScan = true;                           //Comienza el scan
            }
            else
            {
                startAngleRot.y++;                                                              //Se va sumando al eje y
                agentNavMesh.transform.rotation = Quaternion.Euler(0, startAngleRot.y, 0);      //Va rotando en el eje y 
                
                //COMPROBAR SI HAY OTRO ROVER MIENTRAS GIRA
                RaycastHit hit;
                Vector3 fwd = agentNavMesh.transform.TransformDirection(Vector3.forward);
                Debug.DrawRay(agentNavMesh.transform.position, fwd * 5f, Color.red);
                if (Physics.Raycast(agentNavMesh.transform.position, fwd, out hit, 5f))
                {
                    if (hit.transform.tag == "Rover")               //Si el rayo de 5 metros detecta un rover...
                    {
                        animator.SetBool("timeToScan", false);      //Sale del estado de Scan
                    }
                }
            }
        }
        else
        {
            startScan = true;       //Si no es Happy pasa a escanear directamente
        }

        //ESTADO DE ESCANEAR
        if (startScan == true)
        {
            //CONTADOR DE SEGUNDOS DE SCAN
            if (seconds >= limitSeconds)            //Si los segundos son iguales o mayores al limite...
            {
                if (agenteScript.transform.name == "Grumpy" || agenteScript.transform.name == "Happy")          //Si es Grumpy o Happy...
                {
                    if (esPlanta == true)                           //Y es planta...
                    {
                        animator.SetBool("timeToCollect", true);    //Pasa al estado de Collect
                    }
                    else
                    {
                        animator.SetBool("timeToScan", false);      //Sino se va del estado de Scan 
                    }
                }
                else if (agenteScript.transform.name == "Dopey")    //Si es Dopey...
                {
                    if (esRoca == true)                             //Si es una roca...
                    {
                        animator.SetBool("timeToCollect", true);    //Pasa al estado de Collect
                    }
                    else
                    {
                        animator.SetBool("timeToScan", false);      //Sino se irá del estado de Scan 
                    }
                }
            }
            else
            {
                seconds = seconds + 1 * Time.deltaTime;             //Va sumando el contador de segundos
            }

            //DETECCIÓN DE OBJETOS A 5 METROS
            RaycastHit hit;
            Vector3 fwd = agentNavMesh.transform.TransformDirection(Vector3.forward);
            Debug.DrawRay(agentNavMesh.transform.position, fwd * 5f, Color.red);
            if (Physics.Raycast(agentNavMesh.transform.position, fwd, out hit, 5f))
            {
                AIDirector.Instance.objetoScaneadoScan = hit.transform;    //Guarda el objeto escaneado en una variable 

                if (agenteScript.transform.name == "Grumpy" || agenteScript.transform.name == "Happy")  //Si es Grumpy o Happy entonces...
                {
                    if (hit.transform.tag == ("Planta"))        //Si contra lo que choca es una planta...
                    {
                        esPlanta = true;    //Detecta que la planta es true
                        esRoca = false;     //Detecta que no es roca
                    }
                    else
                    {
                        esPlanta = false;   //Detecta que no es roca
                        esRoca = false;     //Detecta que no es roca
                    }
                }
                else if (agenteScript.transform.name == "Dopey")    //Si es Dopey...
                {
                    if (hit.transform.tag == ("Rock"))  //Si contra lo que choca es una roca...
                    {
                        esRoca = true;      //Detecta que es roca
                        esPlanta = false;   //Detecta que no es planta
                    }
                    else
                    {
                        esPlanta = false;   //Detecta que no es planta
                        esRoca = false;     //Detecta que no es roca
                    }
                }

            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        seconds = 0;        //reset contador 
        startScan = false;  //reset la variable para que inicie el scan 
    }
}
