using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecuritySystem : MonoBehaviour
{
    public GameObject[] SecurityCameras;
    public int currentCamera;
    public bool systemOn;
    private bool screenZoom;
    [SerializeField] private bool isComputer;
    [SerializeField] private GameObject PlayerCamera;
    [SerializeField] private GameObject ZoomCamera;
    [SerializeField] private GameObject textPopup;
    public Camera PlayerView;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            systemOn = true;
            if (isComputer)
            {
                textPopup.SetActive(true);
                StartCoroutine(HidePopup());
                StartCoroutine(UpdateCameras());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            systemOn = false;
            if (isComputer)
            {
                ResetCamera();
            }
        }
    }

    private void ResetCamera()
    {    
        PlayerCamera.SetActive(true);
        ZoomCamera.SetActive(false);
    }

    IEnumerator UpdateCameras()
    {
        while(systemOn)
        {
            ChangeCurrentCamera();

            if (Input.GetKeyDown(KeyCode.E))
            {
                ZoomCamera.SetActive(true);
                PlayerCamera.SetActive(false);
                textPopup.SetActive(false);
            }
            yield return null;
        }

    }

    public void ChangeCurrentCamera()
    {
        if (Input.GetKeyDown("1"))
        {
            SecurityCameras[0].SetActive(true);

            SecurityCameras[1].SetActive(false);
            SecurityCameras[2].SetActive(false);
            SecurityCameras[3].SetActive(false);
        }
        if (Input.GetKeyDown("2"))
        {
            SecurityCameras[1].SetActive(true);

            SecurityCameras[0].SetActive(false);
            SecurityCameras[2].SetActive(false);
            SecurityCameras[3].SetActive(false);            
        }
        if (Input.GetKeyDown("3"))
        {
            SecurityCameras[2].SetActive(true);

            SecurityCameras[0].SetActive(false);
            SecurityCameras[1].SetActive(false);
            SecurityCameras[3].SetActive(false);           
        }
        if (Input.GetKeyDown("4"))
        {
            SecurityCameras[3].SetActive(true);

            SecurityCameras[0].SetActive(false);
            SecurityCameras[1].SetActive(false);
            SecurityCameras[2].SetActive(false);           
        }
    }

    private IEnumerator HidePopup()
    {
        yield return new WaitForSeconds(3f);
        textPopup.SetActive(false);
    }

}
