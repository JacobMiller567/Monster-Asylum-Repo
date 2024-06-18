using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

    [RequireComponent(typeof(NavMeshAgent))]
    public class CrawlerMovement : MonoBehaviour
    {
        public Transform Player;
        
        public float UpdateRate = 0.1f;
        public int attackDamage = 25;
        public float attackCoolDown = 3f;
        public float divingCoolDown = 0.75f;
        public float wanderRadius = 5f; 
        public float divingSpeed = 10f;
        public float wanderSpeed = 2f;

        private NavMeshAgent Agent;
        private Coroutine FollowingCoroutine;
        private Vector3 PlayersLastLocation;
        private Vector3 BirdsLastLocation;
        private Rigidbody rb;

        private bool canDamage = true;
        private bool onCooldown = false;
        private bool isDiving = false;

        [SerializeField] private int waypointIndex = 0;
        public Vector3[] Waypoints = new Vector3[3];


        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            Agent.areaMask = 1 << NavMesh.GetAreaFromName("Ceiling"); // TEST
            FollowingCoroutine = StartCoroutine(Wandering());
          //  FollowingCoroutine = StartCoroutine(Patroling());
        }

        private IEnumerator Wandering()
        {
            WaitForSeconds wait = new WaitForSeconds(UpdateRate);
            Agent.speed = wanderSpeed;
            while (true)
            {
                if (!Agent.enabled || !Agent.isOnNavMesh)
                {
                    yield return wait;
                }
                else if (Agent.remainingDistance <= Agent.stoppingDistance)
                {
                    Vector2 point = Random.insideUnitCircle * wanderRadius; // Random point in unit circle multiplied by radius
                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(Agent.transform.position + new Vector3(point.x, 0, point.y), out hit, 3f, Agent.areaMask))
                    {
                        Agent.SetDestination(hit.position);
                        // TEST

                        Vector3 directionToWaypoint = hit.position - transform.position;
                       // directionToWaypoint.x = 0f;
                        //directionToWaypoint.z = 0f;

                        if (directionToWaypoint != Vector3.zero)
                        {
                           Quaternion targetRotation = Quaternion.LookRotation(directionToWaypoint);

                            // Adjust for ceiling crawling by rotating around the x-axis
                            targetRotation *= Quaternion.Euler(180, 0, 0);

                            // Apply the rotation smoothly
                            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 280f * Time.deltaTime);
                        }
                        // END TEST
                    }
                }
                yield return wait;
            }
        }
        private IEnumerator Patroling() 
        {
            WaitForSeconds wait = new WaitForSeconds(UpdateRate);

            yield return new WaitUntil(() => Agent.enabled && Agent.isOnNavMesh); // Wait till enemy is on the navmesh
            Agent.SetDestination(Waypoints[waypointIndex]);

            while (true)
            {
                if (Agent.isOnNavMesh && Agent.enabled && Agent.remainingDistance <= Agent.stoppingDistance)
                {
                    waypointIndex++; 
                } 

                if (waypointIndex >= Waypoints.Length)
                {
                    waypointIndex = 0; 
                }

                Agent.SetDestination(Waypoints[waypointIndex]);
                yield return wait;
            }   
        }

        // If player in range then swoop down towards the player. Rest in the air for a few seconds. If player still in range then swoop down again.
        private IEnumerator SwoopAttack()
        {
            onCooldown = true;
            isDiving = true;
            Vector3 direction = (PlayersLastLocation - transform.position).normalized;
            rb.velocity = direction * divingSpeed;
            canDamage = true;

            yield return new WaitForSeconds(divingCoolDown);
            transform.position = BirdsLastLocation;
            rb.velocity = Vector3.zero;
            isDiving = false;
            Agent.enabled = true;
            canDamage = false;
            FollowingCoroutine = StartCoroutine(Wandering());
            //FollowingCoroutine = StartCoroutine(Patroling());

            yield return new WaitForSeconds(attackCoolDown);
            onCooldown = false;
            canDamage = true;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (canDamage && other.gameObject.CompareTag("Player"))
            {
                DamagePlayer();
                canDamage = false;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && !onCooldown && !isDiving)
            {
                PlayersLastLocation = Player.transform.position; // Players last known position
                Agent.enabled = false;
                BirdsLastLocation = transform.position; // Birds last position before diving
                FollowingCoroutine = StartCoroutine(SwoopAttack());
            }
        }

        private void DamagePlayer()
        {
        
        }

    }
