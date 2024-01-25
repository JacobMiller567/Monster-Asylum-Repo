using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    public Color startColor = Color.white;
    public Color endColor = Color.black;
    [Range(0, 10)] 
    public float speed = 1;
    private Material mat;
    private Renderer ren;

    private void Awake()
    {
        ren = GetComponentInChildren<Renderer>();
        mat = ren.material;
        mat.EnableKeyword("_EMISSION");
    }

    public void Disable()
    {
        speed = 0;
        StartCoroutine(DisableNow());
    }

    private IEnumerator DisableNow()
    {
        yield return new WaitForSeconds(0.5f);
        this.enabled = false;
    }

    private void Update()
    {
        mat.SetColor("_EmissionColor", Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * speed, 1)));
    }
}
