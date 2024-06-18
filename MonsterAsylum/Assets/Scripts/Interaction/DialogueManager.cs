using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    [SerializeField] private GameObject[] dialoguePopups;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void DisplayNextDialogue()
    {
        foreach (GameObject dialogue in dialoguePopups)
        {
            if (dialogue != null)
            {
                dialogue.SetActive(false);
            }
        }
    }
}
