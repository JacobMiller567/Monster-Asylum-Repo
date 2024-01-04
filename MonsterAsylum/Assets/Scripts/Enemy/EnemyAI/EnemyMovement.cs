using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    //// Big help with AI from Llama Academy: https://www.youtube.com/@LlamAcademy ////

    public Transform Player;
    public float UpdateRate = 0.1f;
    private NavMeshAgent Agent;
    public NavMeshTriangulation Triangulation;
    public EnemyDetection DetectionCheck;
    [SerializeField] private Animator Animation;
    [SerializeField] private AudioSource Audio;
    [SerializeField] private Transform AttackZone;
    [SerializeField] private GameObject JumpScareCamera;
    [SerializeField] private GameObject PlayersMesh;
    [SerializeField] private GameObject SecuritySystem;
    [SerializeField] private GameObject GameCanvas;
    [SerializeField] private AudioSource JumpScareAudio;
    [SerializeField] private MainMenu Menu;

    public EnemyState DefaultState;
    private EnemyState _state;

    public EnemyState State 
    {
        get 
        {
            return _state;
        }
        set 
        {
            OnStateChange?.Invoke(_state, value); // If state is not set
            _state = value; 
        }
    }

    public delegate void StateChangeEvent(EnemyState oldState, EnemyState newState);
    public StateChangeEvent OnStateChange;
    public float IdleLocationRadius = 4f; // Random location from enemy while in idle state
    public float sprintSpeed = 4f; 
    public float idleSpeed = 1.5f; 
    //[SerializeField] private int waypointIndex = 0;
    public Vector3[] Waypoints = new Vector3[6];
    public bool MasterKeyFound = false;
    private bool jumpScarePlayed = false;

    private Coroutine FollowingCoroutine;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        OnStateChange += StateChangeHandler;
        DetectionCheck.GainSight += GainSightHandler;
        DetectionCheck.LoseSight += LoseSightHandler;
    }


    private void GainSightHandler(PlayerMovement player)
    {
        State = EnemyState.Chase; 
        Agent.speed = sprintSpeed;
        Animation.SetBool("Wander", false); 
    }
    private void LoseSightHandler(PlayerMovement player)
    {
        if (MasterKeyFound == false)
        {
            State = DefaultState; // Revert back to last state
            Agent.speed = idleSpeed; 
            Animation.SetBool("Detected", false); 
            Animation.SetBool("Wander", true); 
        }
    }

    private void OnDisable()
    {
        _state = DefaultState; 
    }

    public void Spawn()
    {
        OnStateChange?.Invoke(EnemyState.Spawn, DefaultState);     
    }

    public void Start()
    {
        Triangulation = Triangulation; 
        Spawn();
    }

    private void StateChangeHandler(EnemyState oldState, EnemyState newState)
    {
        if (oldState != newState)
        {
            if (FollowingCoroutine != null)
            {
                StopCoroutine(FollowingCoroutine);
            }
            if (oldState == EnemyState.Idle)
            {
                Agent.speed = idleSpeed;
            }
            
            switch (newState)
            {
                case EnemyState.Idle:
                    Animation.SetBool("Wander", true);
                    FollowingCoroutine = StartCoroutine(IdleMotion());
                    break;
                    /*
                case EnemyState.Patrol:
                    FollowingCoroutine = StartCoroutine(PatrolMotion());
                    break;
                    */
                case EnemyState.Chase:
                    FollowingCoroutine = StartCoroutine(FollowTarget());
                    break;
            }
        }
    }

    private IEnumerator IdleMotion()
    {
        WaitForSeconds wait = new WaitForSeconds(UpdateRate);
        Agent.speed = idleSpeed;

        while (true)
        {
            if (!Agent.enabled || !Agent.isOnNavMesh) 
            {
                yield return wait;
            }
            else if (Agent.remainingDistance <= Agent.stoppingDistance)
            {
                Vector2 point = Random.insideUnitCircle * IdleLocationRadius; // Random point in unity circle multiplied by radius

                NavMeshHit hit;

                if (NavMesh.SamplePosition(Agent.transform.position + new Vector3(point.x, 0, point.y), out hit, 3f, Agent.areaMask))
                {
                    Audio.Play();
                    Agent.SetDestination(hit.position);
                }
            }
            yield return wait;
        }
    }

/*
    private IEnumerator PatrolMotion() // NOT USED (WIP)
    {
        WaitForSeconds wait = new WaitForSeconds(UpdateRate);

        yield return new WaitUntil(() => Agent.enabled && Agent.isOnNavMesh); // Wait till enemy is on the navmesh
        Agent.SetDestination(Waypoints[waypointIndex]);

        while (true)
        {
           if (Agent.isOnNavMesh && Agent.enabled && Agent.remainingDistance <= Agent.stoppingDistance)
            {
                waypointIndex++; // Change waypoint
            } 

            if (waypointIndex >= Waypoints.Length)
            {
                waypointIndex = 0; // Reset to inital waypoint
            }

            Agent.SetDestination(Waypoints[waypointIndex]);
        }
        yield return wait;
    }
*/

    private IEnumerator FollowTarget()
    {
        WaitForSeconds wait = new WaitForSeconds(UpdateRate);

        while (true) 
        {
            if (Agent.enabled) 
            {
                Agent.SetDestination(Player.transform.position); 
                Animation.SetBool("Detected", true); 
                if ((Player.transform.position - AttackZone. transform.position).sqrMagnitude < 1.65f && jumpScarePlayed == false)
                {
                    JumpScare();
                    yield return false;                   
                }
            }
            yield return wait;
        }
    }

    private void JumpScare()
    {
        Animation.SetBool("Caught", true);
        JumpScareAudio.Play();
        SecuritySystem.SetActive(false);
        PlayersMesh.SetActive(false); 
        GameCanvas.SetActive(false);
        JumpScareCamera.SetActive(true);
        jumpScarePlayed = true;
        StartCoroutine(ReturnToMenu());
    }


    private void OnDrawGizmosSelected() // Used in editor to show enemies next movement
    {
        for (int i = 0; i < Waypoints.Length; i++)
        {
            Gizmos.DrawWireSphere(Waypoints[i], 0.25f);
            if (i + 1 < Waypoints.Length)
            {
                Gizmos.DrawLine(Waypoints[i], Waypoints[i + 1]);
            }
            else
            {
                Gizmos.DrawLine(Waypoints[i], Waypoints[0]); 
            }
        }
    }

    public void IncreaseEnemyDifficulty()
    {
        idleSpeed = 2f;
        if (State == EnemyState.Idle) 
        {
            Agent.speed = idleSpeed; 
        }
    }

    public void BeginHunt()
    {
        State = EnemyState.Hunt;
        Agent.speed = sprintSpeed;
        StartCoroutine(HuntTarget());
    }

    private IEnumerator HuntTarget()
    {
        WaitForSeconds Wait = new WaitForSeconds(UpdateRate);
        while(true)
        {
            if (Agent.enabled)
            {
                Agent.SetDestination(Player.transform.position);
                Animation.SetBool("Detected", true);
            }
            yield return Wait;
        }
    }


    private IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(3f);
        Cursor.lockState = CursorLockMode.None;
        Menu.Menu();
    }



    
}
