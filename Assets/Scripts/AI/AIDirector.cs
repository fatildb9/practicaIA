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

        DontDestroyOnLoad(gameObject);  //Sets this to not be destroyed when reloding scene  
    }



    //ARENA
    public GameObject Storm;

    public GameObject[] TotalPatrolPoints;      //Array de todos los puntos del Mapa

    public GameObject[] Rovers;                 //Array de puntos de patrulla
    public Transform baseWaypoint;              //Punto para ir a la base 

    public float tiempoEnLlegar;                //variable del tiempo que tarde en llegar
    public float tiempoDeDuracion;              //variable con el tiempo que dura la tormenta 
    public float tiempoEnAlarmar;               //variable con el tiempo que tarda en alarmar

    public float seconds;                       //variable en la que contar los segundos 
    public bool alarm = true;                   //bool que indica el comienzo del alarmar

    private void Start()
    {
        TotalPatrolPoints = GameObject.FindGameObjectsWithTag("waypoint");      //detecta todos los puntos y los guarda
        Rovers = GameObject.FindGameObjectsWithTag("Rover");                    //detecta todos los rovers y los guarda

        StartCoroutine(TormentaArena());    //Comienza la tormenta de arena
    }

    private void Update()
    {
        //CONTADOR de segundos para alarmar
        if (seconds >= (tiempoEnLlegar - tiempoEnAlarmar) && alarm == true)  //cuando llega al tiempo y el bool es verdadero entra                  
        {
            Alarm();            //Llamamos al método alarm
            alarm = false;      //Booleano para que solo se realice una vez
        }
        else
        {
            seconds = seconds + 1 * Time.deltaTime;     //seguimos contando 
        }
        
    }

    //METODO LISTA DE WAYPOINTS
    public GameObject[] AddWaypoint()
    {
        GameObject[] PatrolPoints = new GameObject[6];                  //Array de los puntos del Mapa que se le va a dar a los rovers
        List<GameObject> PuntosRepetidos = new List<GameObject>();      //Lista en la que guardar todos los puntos 

        PuntosRepetidos.AddRange(TotalPatrolPoints);                    //Añadimos a la lista todos los puntos del array
        
        for (int i = 0; i < 6; i++)                                     //Bucle que coge 6 puntos 
        {
            int randomPoint = Random.Range(0, PuntosRepetidos.Count);   //Numero random
            GameObject waypoint = PuntosRepetidos[randomPoint];         //Cogemos el waypoint de esa posicion

            PatrolPoints[i] = waypoint;                                 //metemos el waapoint en el array del inicio
            PuntosRepetidos.Remove(waypoint);                           //borramos el waypoint de la lista para que no se repita
        }

        return PatrolPoints;        //cuando termine el bucle pasamos la lista con los seis puntos 
    }

    //METODO ALARM
    public void Alarm()
    {
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
        for (int i = 0; i < Rovers.Length; i++)         //Pasa por cada uno de los rovers del array 
        {
            Rovers[i].transform.GetComponent<Animator>().SetTrigger("timeToSearch");        //cambia al estado de Search 
        }
    }

    //ARENA INICIO Y FINAL 
    private void StartStorm()
    {
        Storm.SetActive(true);      //se activa la tomrneta
    }
    private void StopStorm()
    {
        Storm.SetActive(false);     //se desactiva la tormenta
    }
    
    //METODO QUITAR INVENTARIO
    public void ComprobarInventario()
    {
        for (int i = 0; i < Rovers.Length; i++)     //Pasa por cada uno de los rovers
        {
            int baseMask = 1 << NavMesh.GetAreaFromName("Base");                        //Detección del area "Base"
            NavMeshHit hit;
            if (!NavMesh.SamplePosition(Rovers[i].transform.position, out hit, 0.2f, baseMask))    //Si la posición está tocando el area "Base"...
            {
                Rovers[i].transform.GetComponent<Animator>().SetTrigger("timeToWait");        //cambia al estado a wait si no esta en base
                Rovers[i].transform.GetComponent<Animator>().GetBehaviour<Waiting>().QuitarInventario(Rovers[i].transform.GetComponent<Animator>());    //Llama al método para quitar el inventario
            }
        }
    }

    //TORMENTA
    private IEnumerator TormentaArena()
    {
        tiempoEnLlegar = Random.Range(45, 60);      //Detecta el tiempo que tarda en llegar
        tiempoDeDuracion = Random.Range(15, 30);    //Detecta el tiempo que dura la tormenta 
        tiempoEnAlarmar = Random.Range(20, 30);     //Detecta el tiempo que tardará en alarmar

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
        }
    }
}
