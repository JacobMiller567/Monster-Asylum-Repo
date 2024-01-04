using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Doors : MonoBehaviour
{
    [SerializeField] private bool MainDoor = false;
    [SerializeField] private bool RightSide = false;
    private AudioSource UnlockSound; 
    private OffMeshLink MeshLink;
    [SerializeField] private MainMenu menu;

    void Start()
    {
        gameObject.isStatic = false;
        UnlockSound = GetComponent<AudioSource>();
        MeshLink = GetComponent<OffMeshLink>();
        MeshLink.enabled = false;
    }

    public void OpenUtilityDoor()
    {
        if (MainDoor == false)
        {
            if (RightSide == false)
            {
                UnlockSound.Play(); 
                gameObject.transform.localRotation = Quaternion.Euler(0, -95, 0);
                gameObject.GetComponent<BoxCollider>().enabled = false;
                MeshLink.enabled = true;
            }

            else 
            {
                UnlockSound.Play();
                gameObject.transform.localRotation = Quaternion.Euler(-180, 95, 0);
                gameObject.GetComponent<BoxCollider>().enabled = false;
                MeshLink.enabled = true;
            }
        }
    }

    public void OpenMainDoor()
    {
        if (MainDoor == true)
        {
            UnlockSound.Play(); 
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.transform.localRotation = Quaternion.Euler(0, -95, 0);
            menu.CutScene();
        }
    }

    
}
