using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Geiser : MonoBehaviour
{
    public float MinRate = 5f;
    public float MaxRate = 15f;
    public ParticleSystem Vapor;

    private NavMeshObstacle obstacle;

    private void Start()
    {
        obstacle = GetComponent<NavMeshObstacle>();

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
            obstacle.enabled = false;
            yield return new WaitForSeconds(Random.Range(MinRate, MaxRate));
            emission.enabled = true;
            obstacle.enabled = true;
            yield return new WaitForSeconds(Random.Range(MinRate, MaxRate));
        }
    }
}
