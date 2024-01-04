using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{
    [SerializeField] private Transform PickupTransform;
    [SerializeField] private GameObject Player;
    [SerializeField] private float PickupRange = 1.1f;
    public float UpdateRate = 0.2f;
    public bool isMaster;
    private bool keyFound = false;
    public bool UtilityKeyFound = false;
    public bool MasterKeyFound = false;
    [SerializeField] private GameObject InteractText;
    private bool activeText = false;

    public void Start()
    {
        StartCoroutine(KeyLocation());
    }


    private IEnumerator KeyLocation() 
    {
        WaitForSeconds wait = new WaitForSeconds(UpdateRate);
        while (keyFound == false) 
        {
           if (Vector3.Distance(gameObject.transform.position, PickupTransform.transform.position) < PickupRange)
            {
                if (!isMaster)
                {
                    activeText = true;
                    Player.GetComponent<PlayerMovement>().inKeyRadius = true;
                    InteractText.SetActive(activeText);

                    if (Player.GetComponent<PlayerInfo>().UtilityKey == true)
                    {
                        InteractText.SetActive(false);
                        Destroy(gameObject);
                    }
                }
                else
                {
                    activeText = true;
                    Player.GetComponent<PlayerMovement>().inKeyRadius = true;
                    InteractText.SetActive(activeText);
                    //Debug.Log("Key is Here");

                    if (Player.GetComponent<PlayerInfo>().MasterKey == true)
                    {
                        InteractText.SetActive(false);
                        Destroy(gameObject);
                    }

                }
            }
            else if (activeText == true && (gameObject.transform.position - PickupTransform.transform.position).sqrMagnitude > PickupRange)
            {
                activeText = false;
                InteractText.SetActive(activeText);

                if (Player.GetComponent<PlayerMovement>().inKeyRadius == true)
                {
                    Player.GetComponent<PlayerMovement>().inKeyRadius = false;
                }
            }
            yield return wait;
        }
    }



    private void PickUpKey()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isMaster)
            {
                keyFound = true;
                UtilityKeyFound = true;
                Player.GetComponent<PlayerInfo>().UtilityKey = true;
            }
            else
            {
                keyFound = true;
                MasterKeyFound = true;
                Player.GetComponent<PlayerInfo>().MasterKey = true;
            }
        }
    }







/*
    private bool InPlayersView(PlayerMovement player)
    {
        Vector3 Direction = (player.transform.position - transform.position).normalized;
        if (Vector3.Dot(transform.forward, Direction) >= Mathf.Cos(fieldOfView))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Direction, out hit, collider.radius, lineOfSightLayer))
            {
                if (hit.transform.GetComponent<PlayerMovement>() != null)
                {
                    // Check if player is looking at key
                }  
            }
        }
        return false;
    }
    */
}
