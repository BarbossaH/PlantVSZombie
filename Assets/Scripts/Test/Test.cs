
using System;
using Characters.Zombies;
using Managers;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Camera playerCamera;

    private void Start()
    {
        playerCamera=Camera.main;
    }

    private void Callback()
    {
        Debug.Log("callback is called");
    }
    public void OnMouseDown()
    {
        // playerCamera.GetComponent<CameraController.CameraController>().MoveCamera(Callback);
    }
}
