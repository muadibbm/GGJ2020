using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speedPanning;

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
            //Debug.Log(mousePos - prevMousePos);
            transform.Translate(-mouseDelta.x * Time.deltaTime, 0f, mouseDelta.z * Time.deltaTime);
            prevMousePos = gi.MousePosition;
        }
    }
}
