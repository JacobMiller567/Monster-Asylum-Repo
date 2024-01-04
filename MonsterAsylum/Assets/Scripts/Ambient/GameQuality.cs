using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQuality : MonoBehaviour
{
    private bool highQuality = true;

    public void DecreaseQuality()
    {
        if (highQuality == true)
        {
            QualitySettings.SetQualityLevel(2);
            highQuality = false;
        }
        else
        {
            QualitySettings.SetQualityLevel(6);
            highQuality = true;  
        }
    }
}
