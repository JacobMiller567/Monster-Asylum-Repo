using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float mouseSensitivity = 250f;
    public Transform playerBody;
    public float xRotation = 0f;
    public float yRotation = 0f;
    public bool lockCursor = true;
    public float clampAngle = 45f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -clampAngle, 32f);

        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, -clampAngle, clampAngle);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

}
