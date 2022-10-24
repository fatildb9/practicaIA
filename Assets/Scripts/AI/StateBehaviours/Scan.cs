using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Scan : StateMachineBehaviour
{
    public float limitSeconds = 5f;
    public float seconds = 0;

    private Search searchScript;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        seconds = 0;
        searchScript = animator.GetComponent<Search>(); //NO FUNCIONA PORQ NO LO REFERENCIA A UN STATE MACHINE
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (seconds >= limitSeconds)
        {
            if (searchScript.hit.transform.tag == ("Rock"))
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
        else
        {
            seconds = seconds + 1 * Time.deltaTime;
            //Debug.Log(seconds);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        seconds = 0;
    }
}
