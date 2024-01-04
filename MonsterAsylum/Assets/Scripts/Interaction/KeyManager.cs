using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Keys;
    private int randomKey;


    void Start()
    {
        randomKey = Random.Range(0, Keys.Length);
        Debug.Log(randomKey);
        Keys[randomKey].SetActive(true);
    }
}
