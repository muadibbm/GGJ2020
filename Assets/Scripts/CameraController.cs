using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public float speedPanning;
    public Vector2 boundsPanning;

    private GameInput gi;
    private Vector3 prevMousePos;

    private void Awake()
    {
        gi = GameManager.Instance.GetTool<GameInput>("GameInput");
    }

    private void Update()
    {
        if(gi.MouseButtonRight) {
            Vector3 mouseDelta = gi.MousePosition - prevMousePos;
            Vector3 nextPos = transform.position - new Vector3(mouseDelta.x, 0f, mouseDelta.y) * Time.deltaTime * speedPanning;
            if (Mathf.Abs(nextPos.x) < boundsPanning.x && Mathf.Abs(nextPos.z) < boundsPanning.y)
                transform.position = nextPos;
        }
        prevMousePos = gi.MousePosition;
    }
}
