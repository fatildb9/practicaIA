using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDirector : MonoBehaviour
{
    //PATRON SINGLETON
    public static AIDirector Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)           //If there is no other instance 
        {
            Instance = this;            //if not, set instance to this 
        }
        else if (Instance != this)      //If instance already exists and it´s not this:
        {
            Destroy(gameObject);
        }

        //OPTIONAL
        DontDestroyOnLoad(gameObject);  //Sets this to not be destroyed when reloding scene  
    }

    //ARENA
    public GameObject Storm;

    public Transform target;            //variable para seguir el objetivo
    public GameObject[] PatrolPoints;    //Array de puntos de patrulla
    public GameObject[] Rovers;    //Array de puntos de patrulla
    public int nextWaypoint = 0;        //Número de array por el que va el agente

    public Transform baseWaypoint;      //Punto para ir a la base 

    public Transform objetoScaneado;        //variable para ver que objeto ha escaneado 
    public Transform objetoScaneadoScan;    //variable para ver que objeto ha escaneado en el estado de Scan

    private void Start()
    {
        PatrolPoints = GameObject.FindGameObjectsWithTag("waypoint");
        Rovers = GameObject.FindGameObjectsWithTag("waypoint");
    }
    


    //ARENA
    private void StartStorm()
    {
        Storm.SetActive(true);
    }

    private void StopStorm()
    {
        Storm.SetActive(false);
    }
}
