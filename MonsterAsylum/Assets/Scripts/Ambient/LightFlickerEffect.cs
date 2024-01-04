using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickerEffect : MonoBehaviour
{
    public new Light light;
    public float minIntensity = 0.2f;
    public float maxIntensity = 1f; 
    [Range(1, 50)]
    public int smoothing = 5;
    public bool disableFlicker = false;

    Queue<float> smoothQueue;
    float lastSum = 0;

    public void Reset() 
    {
        smoothQueue.Clear();
        lastSum = 0;
    }

    void Start() 
    {
        smoothQueue = new Queue<float>(smoothing);
        if (light == null) 
        {
            light = GetComponent<Light>();
        }
    }

    void Update() 
    {
        if (light == null) return;
        while (smoothQueue.Count >= smoothing) 
        {
            lastSum -= smoothQueue.Dequeue();
        }
        float newVal = Random.Range(minIntensity, maxIntensity);
        smoothQueue.Enqueue(newVal);
        lastSum += newVal;

        light.intensity = lastSum / (float)smoothQueue.Count;
    }

    public void ChangeLightFlicker()
    {
        if (disableFlicker == false)
        {
            minIntensity = 0;
        }
        if (disableFlicker == true)
        {
            minIntensity = 1;
        }
    }

}
