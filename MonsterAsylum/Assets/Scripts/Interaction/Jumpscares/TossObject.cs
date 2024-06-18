using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TossObject : MonoBehaviour
{
    [SerializeField] private bool isOnGround;
    private Rigidbody rigidbody;
    private AudioSource audio;
    private bool alreadyTriggered = false;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !alreadyTriggered)
        {
            alreadyTriggered = true;
            ApplyForce();
        }
    }

    private void ApplyForce()
    {
        audio.Play();
        rigidbody.isKinematic = false;
        if (isOnGround)
        {
            rigidbody.AddForce(transform.right * 400f);
        }
        else
        {
            rigidbody.AddTorque(new Vector3(Random.Range(-90, 90), Random.Range(-90, 90), Random.Range(-90, 90)) * 10, ForceMode.Impulse);
        }
        StartCoroutine(HideObject());
    }

    IEnumerator HideObject()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

}
