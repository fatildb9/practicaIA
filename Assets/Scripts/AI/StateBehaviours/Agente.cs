using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agente : MonoBehaviour
{
    public Transform target;          //esto es por si quiero seguir a alguien
    public Transform[] PatrolPoints;
    public int nextWaypoint = 0;                 //Número de array por el que va el agente

    public Transform baseWaypoint;
    public Transform objetoScaneado;
    public Transform objetoScaneadoScan;
}
