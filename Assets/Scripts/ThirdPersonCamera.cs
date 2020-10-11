using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform target, playerTransform;
    [SerializeField] private float distanceToObject = 5.0f;
    [SerializeField] private float rotationSpeed = 1.0f;

    private Camera camera;
    private Transform cameraTransform;

    private float mouseX, mouseY = 0.0f;

    private Vector3 direction;
    private Quaternion rotation;

    private float savedX, savedY = 0.0f;

    private Quaternion savedRotation;
    private bool wasClicked = false;


    private void Start()
    {
        camera = Camera.main;
        cameraTransform = camera.transform;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        direction = new Vector3(0, 0, -distanceToObject);
        cameraTransform.position = target.position + (rotation * direction);
        cameraTransform.LookAt(target);
    }

    private void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;

        if (!wasClicked)
        {
            mouseY = Mathf.Clamp(mouseY, 0, 60);
        }
        else
        {
            mouseY = Mathf.Clamp(mouseY, -15, 90);
        }
    }

    private void LateUpdate()
    {
        rotation = Quaternion.Euler(mouseY, mouseX, 0);

        if (Input.GetMouseButton(1))
        {
            wasClicked = true;
            RotateCamera(rotation);
        }
        else
        {
            if (wasClicked)
            {
                target.rotation =
                    Quaternion.Lerp(target.rotation, savedRotation, Time.deltaTime * 6f);
                if(target.rotation == savedRotation)
                {
                    wasClicked = false;
                    mouseX = savedX;
                    mouseY = savedY;
                }
            }
            else
            {
                savedRotation = target.rotation;
                SaveCurrentMouseInput();
                RotateCamera(rotation);
                RotatePlayer();
            }
        }
    }

    private void SaveCurrentMouseInput()
    {
        savedX = mouseX;
        savedY = mouseY;
    }

    private void RotatePlayer()
    {
        playerTransform.rotation = Quaternion.Euler(0, mouseX, 0);
    }

    private void RotateCamera(Quaternion _rotation)
    {
        target.rotation = _rotation;
    }
}
