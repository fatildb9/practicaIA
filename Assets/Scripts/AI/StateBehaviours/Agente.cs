using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agente : MonoBehaviour
{
    public Transform target;            //variable para seguir el objetivo
    public Transform[] PatrolPoints;    //Array de puntos de patrulla
    public int nextWaypoint = 0;        //Número de array por el que va el agente

    public Transform baseWaypoint;      //Punto para ir a la base 

    //public int inventario;
    //public Transform roverGolpeado;

    public Transform objetoScaneado;        //variable para ver que objeto ha escaneado 
    public Transform objetoScaneadoScan;    //variable para ver que objeto ha escaneado en el estado de Scan
}
