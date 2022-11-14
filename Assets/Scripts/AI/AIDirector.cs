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

    //public List<GameObject> PatrolPoints = new List<GameObject>();
    public GameObject[] TotalPatrolPoints;      //Array de todos los puntos del Mapa
    public GameObject[] PatrolPoints;      //Array de todos los puntos del Mapa

    public GameObject[] Rovers;                 //Array de puntos de patrulla
    public Transform baseWaypoint;              //Punto para ir a la base 

    private void Start()
    {
        //PatrolPoints = GameObject.FindGameObjectsWithTag("waypoint");
        TotalPatrolPoints = GameObject.FindGameObjectsWithTag("waypoint");
        Rovers = GameObject.FindGameObjectsWithTag("Rover");
    }
    
    public GameObject[] AddWaypoint()
    {
        for (int i = 0; i < 6; i++)
        {
            int randomPoints = Random.Range(0, TotalPatrolPoints.Length);
            PatrolPoints[i] = TotalPatrolPoints[randomPoints]; 
        }

        return PatrolPoints;
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
