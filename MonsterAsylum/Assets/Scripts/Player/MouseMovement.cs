using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 250f;
    public Transform playerBody;
    public float xRotation = 0f;
    public bool lockCursor = true;
    public Slider sensitivitySlider;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // even if frame rate is high makes it so that camera doesn't change speeds
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);

        AdjustSensitivity(); // FIX??!
    }


    public void AdjustSensitivity()
    {
        mouseSensitivity = sensitivitySlider.value;
    }

}
