using UnityEngine;

public class AIDirector : MonoBehaviour
{
    public GameObject Storm;

    private void StartStorm()
    {
        Storm.SetActive(true);
    }

    private void StopStorm()
    {
        Storm.SetActive(false);
    }
}
