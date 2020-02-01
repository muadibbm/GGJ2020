using UnityEngine;

public class GameInput : MonoBehaviour
{
    public bool MouseButtonLeft { get; private set; }
    public bool MouseButtonRight { get; private set; }
    public Vector3 MousePosition { get; private set; }

    private void Update()
    {
        MouseButtonLeft = Input.GetMouseButtonDown(0);
        MouseButtonRight = Input.GetMouseButtonDown(1);
        MousePosition = Input.mousePosition;
    }
}
