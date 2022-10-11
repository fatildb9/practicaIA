using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Geiser : MonoBehaviour
{
    public float MinRate = 5f;
    public float MaxRate = 15f;
    public ParticleSystem Vapor;

    private NavMeshObstacle obstacle;   //Variable para detectar el componente NavMeshObstacle 

    private void Start()
    {
        obstacle = GetComponent<NavMeshObstacle>();     //Asociamos el componente a la variable 

        StartCoroutine(GeiserEruption());
    }

    private void Update()
    {
        
    }

    private IEnumerator GeiserEruption()
    {
        ParticleSystem.EmissionModule emission = Vapor.emission;
        while (true)
        {
            emission.enabled = false;
            obstacle.carving = false;                                               //Desactivamos el carve de NavMeshObstacle
            yield return new WaitForSeconds(Random.Range(MinRate, MaxRate));
            emission.enabled = true;
            obstacle.carving = true;                                                //Activamos el carve de NavMeshObstacle cuando se active las particulas 
            yield return new WaitForSeconds(Random.Range(MinRate, MaxRate));
        }
    }
}
