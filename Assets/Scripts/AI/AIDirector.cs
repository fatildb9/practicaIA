using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    public GameObject[] TotalPatrolPoints;      //Array de todos los puntos del Mapa

    public GameObject[] Rovers;                 //Array de puntos de patrulla
    public Transform baseWaypoint;              //Punto para ir a la base 

    public bool tormentaActivar; 

    private void Start()
    {
        TotalPatrolPoints = GameObject.FindGameObjectsWithTag("waypoint");
        Rovers = GameObject.FindGameObjectsWithTag("Rover");
    }

    private void Update()
    {
        StartCoroutine(StormFunctionality());
        StartCoroutine(PararStorm());

        /*if (tormentaActivar == true)
        {
            StartCoroutine(StormFunctionality());
        }*/
    }

    //METODO LISTA DE WAYPOINTS
    public GameObject[] AddWaypoint()
    {
        GameObject[] PatrolPoints = new GameObject[6];      //Array de todos los puntos del Mapa
        int numeroRepetido = 0;

        for (int i = 0; i < PatrolPoints.Length; i++)
        {
            int randomPoints = Random.Range(0, TotalPatrolPoints.Length);

            if (numeroRepetido != randomPoints)
            {
                numeroRepetido = randomPoints;
                PatrolPoints[i] = TotalPatrolPoints[randomPoints];
            }
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

    IEnumerator StormFunctionality()
    {
        int tiempoComienzo = Random.Range(45, 61);
        StartStorm();
        yield return new WaitForSeconds(5);
    }

    IEnumerator PararStorm()
    {
        int timpoTerminar = Random.Range(15, 31);
        StopStorm();
        yield return new WaitForSeconds(4);
    }
}
