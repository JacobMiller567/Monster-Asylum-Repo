using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public bool isLocked = true;
    public Animator animator;


    public void UnlockDoor()
    {
        if (isLocked)
        {
            isLocked = false;
            animator.SetTrigger("OpenDoor");

        }
    }
}
