using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class PlayerInfo : MonoBehaviour
{

    public static PlayerInfo instance;
    public bool UtilityKey = false;
    public bool MasterKey = false;
    [SerializeField] private GameObject UtilityText;
    [SerializeField] private GameObject MasterText;
    [SerializeField] private EnemyMovement ParasiteMovement;
    [SerializeField] private AudioSource KeyAudio;
    [SerializeField] private GameObject StartDialogue;


    void Start()
    {
        StartDialogue.SetActive(true);
        Destroy(StartDialogue, 5.5f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Door") && other != null)
        {
            if (UtilityKey == true)
            {
                other.gameObject.GetComponent<Doors>().OpenUtilityDoor();
            }
        }
        if (other.gameObject.CompareTag("MainDoor") && other != null)
        {
            if (MasterKey == true)
            {
                other.gameObject.GetComponent<Doors>().OpenMainDoor();
            }
        }
    }


    public void UtilityKeyMessage()
    {
        ParasiteMovement.IncreaseEnemyDifficulty();
        UtilityText.SetActive(true);
        KeyAudio.Play();
        Destroy(UtilityText, 4.5f);
    }
    public void MasterKeyMessage()
    {
        MasterText.SetActive(true);
        ParasiteMovement.BeginHunt();
        KeyAudio.Play();
        Destroy(MasterText, 4.5f);
    }



}
