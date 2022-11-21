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
        else if (Instance != this)      //If instance already exists and it�s not this:
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
    public bool alarm = true;
    public bool wait = true;

    private void Start()
    {
        TotalPatrolPoints = GameObject.FindGameObjectsWithTag("waypoint");
        Rovers = GameObject.FindGameObjectsWithTag("Rover");

        StartCoroutine(TormentaArena());    //Comienza la tormenta de arena
    }

    private void Update()
    {
        //CONTADOR de segundos para alarmar
        if (seconds >= (tiempoEnLlegar - tiempoEnAlarmar) && alarm == true)                    
        {
            Alarm();            //Llamamos al m�todo alarm
            alarm = false;      //Booleano para que solo se realice una vez
        }
        else
        {
            seconds = seconds + 1 * Time.deltaTime;     
        }
        
    }

    //METODO LISTA DE WAYPOINTS
    public GameObject[] AddWaypoint()
    {
        GameObject[] PatrolPoints = new GameObject[6];      //Array de todos los puntos del Mapa
        int numeroRepetido = 0;

        while(numeroRepetido < 6)
        {
            GameObject Waypoint = RandomWaypoint();
            bool Repetido = true; 

            if (Repetido == false)
            {
                //PatrolPoints[];
            }
        }

        return PatrolPoints;
    }

    public GameObject RandomWaypoint()
    {
        int randomPoints = Random.Range(0, TotalPatrolPoints.Length);
        GameObject random = TotalPatrolPoints[randomPoints];

        return random;
    }

    /*public GameObject[] AddWaypoint()
    {
        GameObject[] PatrolPoints = new GameObject[6];      //Array de todos los puntos del Mapa

        for (int i = 0; i < PatrolPoints.Length; i++)
        {
            int randomPoints = Random.Range(0, TotalPatrolPoints.Length);
            PatrolPoints[i] = TotalPatrolPoints[randomPoints];
        }

        return PatrolPoints;
    }*/


    //METODO ALARM
    public void Alarm()
    {
        Debug.Log("Doy la alarma");
        for (int i = 0; i < Rovers.Length; i++)     //Pasa por cada uno de los rovers del array
        {
            Rovers[i].transform.GetComponent<Animator>().SetTrigger("timeToAlarm");         //se pasa al estado de alarm

            //desactiva cualquier bool que pudiese estar 
            Rovers[i].transform.GetComponent<Animator>().SetBool("timeToScan", false);
            Rovers[i].transform.GetComponent<Animator>().SetBool("timeToCharge", false);
            Rovers[i].transform.GetComponent<Animator>().SetBool("timeToCollect", false);
            Rovers[i].transform.GetComponent<Animator>().SetBool("timeToBase", false);
        }
    }

    //METODO PARA VOLVER A SEARCH TRAS WAITING
    public void SearchAgain()
    {
        Debug.Log("Voy a search ");
        for (int i = 0; i < Rovers.Length; i++)         //Pasa por cada uno de los rovers del array 
        {
            Rovers[i].transform.GetComponent<Animator>().SetTrigger("timeToSearch");        //cambia al estado de Search 
        }
    }

    //ARENA INICIO Y FINAL 
    private void StartStorm()
    {
        Storm.SetActive(true);
    }
    private void StopStorm()
    {
        Storm.SetActive(false);
    }
    
    //METODO QUITAR INVENTARIO
    public void ComprobarInventario()
    {
        for (int i = 0; i < Rovers.Length; i++)     //Pasa por cada uno de los rovers
        {
            int baseMask = 1 << NavMesh.GetAreaFromName("Base");                        //Detecci�n del area "Base"
            NavMeshHit hit;
            if (!NavMesh.SamplePosition(Rovers[i].transform.position, out hit, 0.2f, baseMask))    //Si la posici�n est� tocando el area "Base"...
            {
                Rovers[i].transform.GetComponent<Animator>().SetTrigger("timeToWait");        //cambia al estado de Search 
                Debug.Log("una vez");
                Rovers[i].transform.GetComponent<Animator>().GetBehaviour<Waiting>().QuitarInventario(Rovers[i].transform.GetComponent<Animator>());    //Pone su inventario a 0 
            }
        }
    }

    //TORMENTA
    private IEnumerator TormentaArena()
    {
        tiempoEnLlegar = Random.Range(45, 60);      //Detecta el tiempo que tarda en llegar
        tiempoDeDuracion = Random.Range(15, 30);    //Detecta el tiempo que dura la tormenta 
        tiempoEnAlarmar = Random.Range(20, 30);     //Detecta el tiempo que tardar� en alarmar

        Debug.Log("Entre");
        while (true)
        {
            StopStorm();            //La tormenta para 
            yield return new WaitForSeconds(tiempoEnLlegar);    //espera tiempo en llegar

            ComprobarInventario();     //Termina el tiempo y comprueba si esta en la base y quita el inventario 
            StartStorm();           //La tormenta comienza
            yield return new WaitForSeconds(tiempoDeDuracion);  //la tormenta dura 

            SearchAgain();          //Pasa a search de nuevo 
            seconds = 0;            //se resetea el tiempo 
            alarm = true;           //Se resetea el bool de alarma
            wait = true;           //Se resetea el bool de alarma
        }
    }
}
