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

    public GameObject[] PatrolPoints;    //Array de puntos de patrulla
    public GameObject[] Rovers;    //Array de puntos de patrulla
    public Transform baseWaypoint;      //Punto para ir a la base 

    private void Start()
    {
        PatrolPoints = GameObject.FindGameObjectsWithTag("waypoint");
        Rovers = GameObject.FindGameObjectsWithTag("Rover"); 
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
