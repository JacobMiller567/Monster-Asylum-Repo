using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoteManager : MonoBehaviour
{
   public static NoteManager Instance;

   public GameObject page;
   public TMP_Text noteText;

   private void Awake()
   {
        if (Instance == null)
        {
            Instance = this;
        }
   }

   public void DisplayNote(string note)
   {
        page.SetActive(true);
        noteText.text = note;
   }
   
   public void HideNote()
   {
        page.SetActive(false);
        noteText.text = "";
   }
}
