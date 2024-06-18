using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayKeyPad : MonoBehaviour
{
    public GameObject keyPad;
    public Combination combination;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !combination.codeCracked)
        {
            keyPad.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            keyPad.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
