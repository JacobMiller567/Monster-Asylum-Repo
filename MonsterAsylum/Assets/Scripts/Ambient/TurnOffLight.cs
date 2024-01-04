using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffLight : MonoBehaviour
{
    public Light light;
    public float lightOnValue = .7f;
    public float lightOffValue = 0.01f;
    public float timer = 8.5f;
    private bool animationPlayed = false;

    [SerializeField] private Animator DoctorAnim;
    [SerializeField] private GameObject textHolder;
    [SerializeField] private GameObject textThanksHolder;
    [SerializeField] private MainMenu menu;


    public void Update()
    {
        if (animationPlayed == false)
        {
            StartCoroutine(LightsOff());
        }
    }

    private IEnumerator LightsOff()
    {
        animationPlayed = true;
        yield return new WaitForSeconds(timer);
        DoctorAnim.SetBool("Surgery", true);
        light.intensity = lightOffValue;
        textHolder.SetActive(false);

        yield return new WaitForSeconds(2f);
        light.intensity = lightOnValue;
        yield return new WaitForSeconds(1f);
        textThanksHolder.SetActive(true);

        yield return new WaitForSeconds(3f);
        menu.Menu();
    }
}
