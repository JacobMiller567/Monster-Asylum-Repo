using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] private Transform[] targets;
    [SerializeField] private Transform target;  
    [SerializeField] private float speed;  
    private int current;  

    // Update is called once per frame    
    void Update() { 
        /* 
        if (transform.position != target[current].position) {  
            Vector3 pos = Vector3.MoveTowards(transform.position, target[current].position, speed * Time.deltaTime);  
            GetComponent < Rigidbody > ().MovePosition(pos);  
        } else current = (current + 1) % target.Length; 

        */

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if ((transform.position - target.position).sqrMagnitude < 1f)
        {
            Destroy(gameObject);
        }
    } 
}
