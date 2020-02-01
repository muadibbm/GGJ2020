﻿using UnityEngine;

[RequireComponent(typeof(Camera))]
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
            transform.position = transform.position - new Vector3(mouseDelta.x, 0f, mouseDelta.y) * Time.deltaTime * speedPanning;
        }
        prevMousePos = gi.MousePosition;
    }
}
