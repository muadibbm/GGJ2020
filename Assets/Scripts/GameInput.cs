using UnityEngine;

public class GameInput : MonoBehaviour
{
    public bool MouseButtonLeft { get; private set; }
    public bool MouseButtonRight { get; private set; }
    public bool MouseButtonLeftDown { get; private set; }
    public bool MouseButtonRightDown { get; private set; }
    public Vector3 MousePosition { get; private set; }
    public Vector2 MouseScrollDelta { get; private set; }

    private void Update() {
        MouseButtonLeft = Input.GetMouseButton(0);
        MouseButtonRight = Input.GetMouseButton(1);
        MouseButtonLeftDown = Input.GetMouseButtonDown(0);
        MouseButtonRightDown = Input.GetMouseButtonDown(1);
        MousePosition = Input.mousePosition;
        MouseScrollDelta = Input.mouseScrollDelta;
        Cursor.visible = MouseButtonRight == false;
    }
}
