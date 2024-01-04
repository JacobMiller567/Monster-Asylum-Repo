using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class OldEnemyMovement : MonoBehaviour
{
    public Transform Player;
    [SerializeField] private Animator animator;
    public float UpdateRate = 0.1f;
    private NavMeshAgent Agent;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StartCoroutine(FollowTarget());
    }

    private IEnumerator FollowTarget()
    {
        WaitForSeconds Wait = new WaitForSeconds(UpdateRate);
        
        while(enabled)
        {
            Agent.SetDestination(Player.transform.position);
            animator.SetBool("Detected", true);
            yield return Wait;
        }
    }
}