using UnityEngine;
using UnityEngine.UI;

public class RoverButton : MonoBehaviour
{
    public Text TextComponent;
    private Transform Target;
    private CameraController Controller;

    public void SetInfo(CameraController controller, int number, string name, Transform target)
    {
        Controller = controller;
        TextComponent.text = string.Format("{0} {1}", number, name);
        Target = target;
    }

    public void OnClick()
    {
        Controller.SetCameraTarget(Target);
    }
}
