using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameQuality : MonoBehaviour
{
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
}
