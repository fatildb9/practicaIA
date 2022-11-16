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

    public float tiempoEnLlegar;
    public float tiempoDeDuracion;
    public float tiempoEnAlarmar;
    public float seconds; 

    private void Start()
    {
        TotalPatrolPoints = GameObject.FindGameObjectsWithTag("waypoint");
        Rovers = GameObject.FindGameObjectsWithTag("Rover");

        //StartCoroutine(Alarma());    
        StartCoroutine(TormentaArena());    
    }

    private void Update()
    {
        if (seconds >= (tiempoEnLlegar - tiempoEnAlarmar))                    //Si los segundos son mayores que el limite...
        {
            //animator true alarm
            Debug.Log("Doy la alarma");
        }
        else
        {
            seconds = seconds + 1 * Time.deltaTime;     //Mientras va contando los segundos
        }
    }

    //METODO LISTA DE WAYPOINTS
    /*public GameObject[] AddWaypoint()
    {
        GameObject[] PatrolPoints = new GameObject[6];      //Array de todos los puntos del Mapa
        int numeroRepetido = 9;

        for (int i = 0; i < PatrolPoints.Length; i++)
        {
            for (int randomPoints = 0; randomPoints != numeroRepetido; randomPoints = Random.Range(0, TotalPatrolPoints.Length + 1))
            {
                PatrolPoints[i] = TotalPatrolPoints[randomPoints];
            }
    
        }

        return PatrolPoints;
    }*/

    public GameObject[] AddWaypoint()
    {
        GameObject[] PatrolPoints = new GameObject[6];      //Array de todos los puntos del Mapa

        for (int i = 0; i < PatrolPoints.Length; i++)
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

    /*private IEnumerator Alarma()
    {
        tiempoEnLlegar = Random.Range(45, 60);
        tiempoDeDuracion = Random.Range(15, 30);
        tiempoEnAlarmar = Random.Range(5, 10);

        yield return new WaitForSeconds(tiempoEnAlarmar);
        StartCoroutine(TormentaArena());
        Debug.Log("Alarma");
    }
    private IEnumerator TormentaArena()
    {
        Debug.Log("Entre");
        while (true)
        {
            StopStorm();
            yield return new WaitForSeconds(tiempoEnLlegar);
            StartStorm();
            yield return new WaitForSeconds(tiempoDeDuracion);
        }
    }
     */

    /*private IEnumerator Alarma()
    {
        tiempoEnLlegar = Random.Range(45, 60);
        tiempoDeDuracion = Random.Range(15, 30);
        tiempoEnAlarmar = Random.Range(5, 10);

        yield return new WaitForSeconds(tiempoEnAlarmar);
        StartCoroutine(TormentaArena());
        Debug.Log("Alarma");
    }*/

    private IEnumerator TormentaArena()
    {
        tiempoEnLlegar = Random.Range(45, 60);
        tiempoDeDuracion = Random.Range(15, 30);
        tiempoEnAlarmar = Random.Range(5, 10);

        Debug.Log("Entre");
        while (true)
        {
            StopStorm();
            yield return new WaitForSeconds(tiempoEnLlegar);
            
            
            StartStorm();
            yield return new WaitForSeconds(tiempoDeDuracion);
        }
    }
}
