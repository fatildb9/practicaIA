using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject ButtonPrefab;
    public Transform ContainerPanel;
    public CameraFollow Camera;

    private void Start()
    {
        GameObject[] rovers = GameObject.FindGameObjectsWithTag("Rover");
        for (int i = 0; i < rovers.Length; i++)
        {
            GameObject button = Instantiate(ButtonPrefab);
            button.GetComponent<RoverButton>().SetInfo(this, i, rovers[i].name, rovers[i].transform);
            button.transform.SetParent(ContainerPanel);
        }

        if (rovers.Length > 0)
        {
            Camera.SetTarget(rovers[0].transform);
        }
    }

    public void SetCameraTarget(Transform target)
    {
        Camera.SetTarget(target);
    }
}
