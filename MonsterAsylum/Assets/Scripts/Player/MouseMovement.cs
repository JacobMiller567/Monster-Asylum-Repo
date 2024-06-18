using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 250f;
    public Transform playerBody;
    public PlayerMovement player;
    public float xRotation = 0f;
    public float yRotation = 0f;
    public float clampAngle = 45f;
    public bool lockCursor = true;
    public Slider sensitivitySlider;
    

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (player.isHiding)
        {
            ClampPlayerView();
        }
        else
        {
            PlayerView();
        }
        AdjustSensitivity();
    }

    public void PlayerView()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }

    public void ClampPlayerView()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -clampAngle, 32f);

        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, -clampAngle, clampAngle);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }


    public void AdjustSensitivity()
    {
        mouseSensitivity = sensitivitySlider.value;
    }

}
