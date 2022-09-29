using System.Collections;
using UnityEngine;

public class Geiser : MonoBehaviour
{
    public float MinRate = 5f;
    public float MaxRate = 15f;
    public ParticleSystem Vapor;

    private void Start()
    {
        StartCoroutine(GeiserEruption());
    }

    private IEnumerator GeiserEruption()
    {
        ParticleSystem.EmissionModule emission = Vapor.emission;
        while (true)
        {
            emission.enabled = false;
            yield return new WaitForSeconds(Random.Range(MinRate, MaxRate));
            emission.enabled = true;
            yield return new WaitForSeconds(Random.Range(MinRate, MaxRate));
        }
    }
}
