using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    [SerializeField] private GameObject hiddenPoint;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private GameObject hidePopup;
    [SerializeField] private Vector3 initalRotation;
    private Vector3 lastPosition;
    private Coroutine checkForInputCoroutine;
    private Coroutine nextInputCoroutine;
    private bool isHidden = false;


    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isHidden)
        {
            lastPosition = player.LastLocation();
            hidePopup.SetActive(true);
            checkForInputCoroutine = StartCoroutine(CheckForInput());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hidePopup.SetActive(false);
            if (checkForInputCoroutine != null)
            {
                StopCoroutine(checkForInputCoroutine);
                checkForInputCoroutine = null;
            }
            if (nextInputCoroutine != null)
            {
                StopCoroutine(nextInputCoroutine);
                nextInputCoroutine = null;
            }
        }
    }

    IEnumerator CheckForInput()
    {
        yield return new WaitForSeconds(0.1f);
        while (!isHidden)
        {
            if (Input.GetKeyDown(KeyCode.E)) // FIX: Glitch causing player to teleport back to locker even if far away!
            {
                isHidden = true;
                hidePopup.SetActive(false);
                HidePlayer(hiddenPoint.transform.position);
                nextInputCoroutine = StartCoroutine(NextInput());
                yield break;
            }
            yield return null;
        }
    }


    IEnumerator NextInput()
    {
        yield return new WaitForSeconds(0.1f);
        while (isHidden)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isHidden = false;
                hidePopup.SetActive(true);
                HidePlayer(lastPosition);
                checkForInputCoroutine = StartCoroutine(CheckForInput());
                yield break;
            }
            yield return null;
        }

    }
    public void HidePlayer(Vector3 pos)
    {
        player.ChangePosition(pos, Quaternion.Euler(initalRotation));
    }
}
