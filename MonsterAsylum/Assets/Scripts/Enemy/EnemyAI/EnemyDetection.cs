using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemyDetection : MonoBehaviour
{
    public SphereCollider collider; // Enemy detection radius
    public float fieldOfView = 90f; 
    public LayerMask lineOfSightLayer;

    public delegate void GainSightEvent(PlayerMovement player);
    public GainSightEvent GainSight;
    public delegate void LoseSightEvent(PlayerMovement player);
    public LoseSightEvent LoseSight;

    public AudioSource audioSource; //TEST
    public float threshold = 2f;//TEST


    private Coroutine CheckLineOfSightCoroutine;

    private void Awake()
    {
        collider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player;
        if (other.TryGetComponent<PlayerMovement>(out player))
        {
            if (!CheckLineOfSight(player))
            {
                CheckLineOfSightCoroutine = StartCoroutine(CheckForLineOfSight(player));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerMovement player;
        if (other.TryGetComponent<PlayerMovement>(out player))
        {
            LoseSight?.Invoke(player);
            if (CheckLineOfSightCoroutine != null)
            {
                StopCoroutine(CheckLineOfSightCoroutine);
            }
        }
    }

    private bool CheckLineOfSight(PlayerMovement player)
    {
        Vector3 Direction = (player.transform.position - transform.position).normalized;
        if (Vector3.Dot(transform.forward, Direction) >= Mathf.Cos(fieldOfView))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Direction, out hit, collider.radius, lineOfSightLayer))
            {
                if (hit.transform.GetComponent<PlayerMovement>() != null)
                {
                    GainSight?.Invoke(player); // Player is found
                    return true;
                }  
            }
        }
        return false;
    }


    private IEnumerator CheckForLineOfSight(PlayerMovement player)
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f); 

        while (!CheckLineOfSight(player))
        {
            yield return wait;
        }
    }


    public void UpdateEnemyRadius()
    {
        collider.radius = 10f;
    }


/*
    /// TEST #### AUDIO DETECTION #### TEST ///
    void OnAudioFilterRead(float[] data, int channels)
    {
        float sum = 0f;
        for (int i = 0; i < data.Length; i++)
        {
            sum += Mathf.Abs(data[i]);
        }

        float rms = Mathf.Sqrt(sum / data.Length);
        if (rms > threshold)
        {
            Debug.Log("Player is walking!");
        }
    }
*/

    
}
