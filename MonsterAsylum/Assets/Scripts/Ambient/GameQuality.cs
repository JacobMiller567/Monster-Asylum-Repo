using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameQuality : MonoBehaviour
{
    public float ambientIntensity = 0f;
    public float reflectionIntensity = .4f;
    [SerializeField] private int targetFPS = 100;

    private void Awake()
    {
        Application.targetFrameRate = targetFPS;
        RenderSettings.ambientIntensity = ambientIntensity;
        RenderSettings.reflectionIntensity = reflectionIntensity;
    }

    public void ChangeGameQuality(TMP_Dropdown option)
    {
        switch (option.value)
        {
            case 0:
                MediumQuality();
                break;
            case 1:
                HighQuality();
                break;
            case 2:
                LowQuality();
                break;
        }
    }
    private void MediumQuality()
    {
        QualitySettings.SetQualityLevel(3);
    }
    private void HighQuality()
    {
        QualitySettings.SetQualityLevel(5);
    }
    private void LowQuality()
    {
        QualitySettings.SetQualityLevel(1);
    }

    public void ChangeFPS(TMP_Dropdown option)
    {
        switch (option.value)
        {
            case 0:
                LowFPS();
                break;
            case 1:
                MediumFPS();
                break;
            case 2:
                HighFPS();
                break;
            case 3:
                UnlimitedFPS();
                break;
        }
    }
    private void LowFPS()
    {
        targetFPS = 30;
        Application.targetFrameRate = 30;
    }
    private void MediumFPS()
    {
        targetFPS = 60;
        Application.targetFrameRate = 60;
    }
    private void HighFPS()
    {
        targetFPS = 100;
        Application.targetFrameRate = 100;
    }
    private void UnlimitedFPS()
    {
        targetFPS = -1;
        Application.targetFrameRate = -1;
    }
}
