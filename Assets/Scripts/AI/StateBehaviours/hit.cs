using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class hit : StateMachineBehaviour
{
    private NavMeshAgent agentNavMesh;      //referencia al Nav Mesh del agente

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentNavMesh = animator.gameObject.GetComponent<NavMeshAgent>();        //referencia al Nav Mesh del agente
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //DETECTA SI LE DAMOS AL ROVER
        RaycastHit hit;
        Vector3 fwd = agentNavMesh.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(agentNavMesh.transform.position, fwd * 1f, Color.red);
        if (Physics.Raycast(agentNavMesh.transform.position, fwd, out hit, 1f))     //Si el rover esta a menos de un metro...
        {
            if (hit.transform.tag == "Rover")       //Y es un rover...
            {                
                animator.GetBehaviour<Collect>().inventario = hit.transform.GetComponent<Animator>().GetBehaviour<Collect>().inventario;    //Asociamos su inventario al nuestro 
                hit.transform.GetComponent<Animator>().GetBehaviour<Collect>().inventario = 0;  //al rover golpeado le reseteamos el inventario 
                hit.transform.GetComponent<Animator>().SetTrigger("timeToStunned");             //Activamos el trigger de stunned del rover golpeado
                
                //reset a todos los estados ya que el rover podría estar en cualquiera de ellos
                hit.transform.GetComponent<Animator>().SetBool("timeToScan", false);        
                hit.transform.GetComponent<Animator>().SetBool("timeToCharge", false);
                hit.transform.GetComponent<Animator>().SetBool("timeToCollect", false);
                hit.transform.GetComponent<Animator>().SetBool("timeToBase", false);
                
                animator.SetBool("timeToBase", true);   //el agente se va al estado de base
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
