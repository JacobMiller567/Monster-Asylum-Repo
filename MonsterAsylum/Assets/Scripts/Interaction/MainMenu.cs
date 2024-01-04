using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    { 
        SceneManager.LoadScene("Asylum");
    }

    public void Options()
    {

    }

    public void Credits()
    {

    }

    public void CutScene()
    { 
        SceneManager.LoadScene("EndCutScene");
    }

    public void Menu()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
