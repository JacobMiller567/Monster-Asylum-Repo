using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Notes : MonoBehaviour
{
    public string noteText = "";
    [SerializeField] private GameObject notePopup;
    [SerializeField] private AudioSource noteSound;
   // [SerializeField] private TextMeshProUGUI interactText;
    private bool isReading = false;
    private bool openNote = false;

    public void ReadNote()
    {
        if (!isReading)
        {
            isReading = true;
            NoteManager.Instance.DisplayNote(noteText);
            noteSound.Play();
            //GameCanvas.Instance.CurrentNote = this;
        }
    }

    public void StopReading()
    {
        isReading = false;
        NoteManager.Instance.HideNote();
    }


/*
    //private void OnTriggerEnter(Collider other)
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E) && !isReading)
            {
                ReadNote();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isReading)
            {
                StopReading();
            }
        }
    }
*/

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isReading)
        {   
            notePopup.SetActive(true);
            //interactText.text = "Press       to read!";
            //isReading = true;
            StartCoroutine(CheckForInput());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            notePopup.SetActive(false);
            isReading = false;
            openNote = false;
            StopReading();
        }
    }

    IEnumerator CheckForInput()
    {
        while (!openNote)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                notePopup.SetActive(false);
                openNote = true;
                ReadNote();
            }
            yield return null;
        }
    }

/*
    IEnumerator CheckForInput()
    {
        while(true)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {

            }
            yield return null;
        }

    }
*/

}
