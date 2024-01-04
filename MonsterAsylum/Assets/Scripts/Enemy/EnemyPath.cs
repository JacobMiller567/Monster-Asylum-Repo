using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] private Transform[] targets;
    [SerializeField] private Transform target;  
    [SerializeField] private float speed;  
    private int current;  
 
    void Update() { 
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if ((transform.position - target.position).sqrMagnitude < 1f)
        {
            Destroy(gameObject);
        }
    } 
}
