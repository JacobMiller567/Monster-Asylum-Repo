using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController controller;
    private Vector3 velocity;
    [SerializeField] private float speed = 6.5f;
    [SerializeField] private float jumpHeight = 4f;
    [SerializeField] private float sprint = 10f;
    [SerializeField] private float crouchSpeed = 2.5f;
    [SerializeField] private float crouchAmount = 0.25f; 
    private float gravity = -9.81f; 
    private float holdSpeed;

    public bool isIdle;
    private bool isRunning;
    private bool isCrouching;
    private bool onGround;

    public bool inKeyRadius = false;
    [SerializeField] private PlayerInfo Info;
    [SerializeField] private AudioSource WalkAudio, RunAudio, CrouchAudio;
    private Rigidbody rb;
    private float normalYLocalPosition = 1;
    private float yLocalPositionHolder = 1;
    public float ambientIntensity = 0f;
    public float reflectionIntensity = .4f;

    void Start()
    {
        RenderSettings.ambientIntensity = ambientIntensity; 
        RenderSettings.reflectionIntensity = reflectionIntensity; 


        rb = GetComponent<Rigidbody>();
        normalYLocalPosition = rb.transform.localScale.y; 
        controller = gameObject.GetComponent<CharacterController>();
        holdSpeed = speed;

        WalkAudio.enabled = false;
        RunAudio.enabled = false;
        CrouchAudio.enabled = false;
    }

    void Update()
    {
      onGround = controller.isGrounded;

      float horizontal = Input.GetAxis("Horizontal");
      float vertical = Input.GetAxis("Vertical");

      Vector3 move = (transform.right * horizontal + transform.forward * vertical).normalized;
      if (move != Vector3.zero)
      {
        isIdle = false;
        controller.Move(move * speed * Time.deltaTime);
        animator.SetFloat("BlendSpeed", 0.5f, 0.1f, Time.deltaTime); 

        if (!isRunning && !isCrouching)
        {
          WalkAudio.enabled = true;
        }

        if (velocity.y > 0)
        {
          WalkAudio.enabled = false;
          RunAudio.enabled = false;
          CrouchAudio.enabled = false;
        }

      }
      else
      {
        animator.SetFloat("BlendSpeed", 0f, 0.1f, Time.deltaTime); 
        isIdle = true;
        WalkAudio.enabled = false;
        RunAudio.enabled = false;
        CrouchAudio.enabled = false;
      }

      if (Input.GetKey(KeyCode.LeftShift)) 
      {
          if(!isRunning &&  !isCrouching) 
          {
              speed = sprint; 
              isRunning = true;
          }
          if (isRunning)
          {
            animator.SetFloat("BlendSpeed", 1f, 0.1f, Time.deltaTime); 
            WalkAudio.enabled = false;
            CrouchAudio.enabled = false;
            RunAudio.enabled = true;
          }
      }
      if (Input.GetKeyUp(KeyCode.LeftShift))
      {
          speed = holdSpeed; 
          isRunning = false;
          RunAudio.enabled = false;
      }

      if (Input.GetKey(KeyCode.LeftControl))
      {
        if (!isCrouching)
        {
          speed = crouchSpeed;
          isCrouching = true;
        }
        if (isCrouching)
        {
          WalkAudio.enabled = false;
          RunAudio.enabled = false;
          CrouchAudio.enabled = true;
          normalYLocalPosition = crouchAmount;
          rb.transform.localScale = new Vector3(rb.transform.localScale.x, normalYLocalPosition, rb.transform.localScale.z); // Set rigibodies transforms local scale to be a new Vector3 based on our localScale x
        }
      }
      if (Input.GetKeyUp(KeyCode.LeftControl)) 
      {
          speed = holdSpeed; 
          isCrouching = false;
          CrouchAudio.enabled = false;
          normalYLocalPosition = yLocalPositionHolder;
          rb.transform.localScale = new Vector3(rb.transform.localScale.x, normalYLocalPosition, rb.transform.localScale.z);
      }

      if (onGround)
      {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
          animator.SetTrigger("Jump");
          isIdle = false;
          velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
      }

      Interact();
      velocity.y += gravity * Time.deltaTime;
      controller.Move(velocity * Time.deltaTime);
      
  }


  public void Interact()
  { 
    if (Input.GetKeyDown(KeyCode.E))
    { 
      animator.SetTrigger("Grabbing");
      if (inKeyRadius == true && Info.UtilityKey == false)
      {
        Info.UtilityKey = true;
        Info.UtilityKeyMessage();
        inKeyRadius = false;
      }
      else if (inKeyRadius == true && Info.UtilityKey == true)
      {
        Info.MasterKey = true;
        Info.MasterKeyMessage();
        inKeyRadius = false;
      }
    }
  }


}

