using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialogueText;
    [SerializeField] private bool isMainDoor;
    [SerializeField] private float destroyTime;
    private bool textWasDisplayed = false;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !textWasDisplayed)
        {
            dialogueText.SetActive(true);
            textWasDisplayed = true;
            Destroy(dialogueText, destroyTime);

            if (isMainDoor == false)
            {
                Destroy(gameObject, destroyTime + 0.5f);
            }
        }
    }
}
