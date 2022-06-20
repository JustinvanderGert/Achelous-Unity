using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_FirstPerson : MonoBehaviour

{
  
    private SpeedReader SpeedReader; 

    private void start( )
    {
        SpeedReader = GameObject.Find("Speedometer").GetComponent<SpeedReader>();
    }
    public float speedScore; Vector3 oldPosition;
    void FixedUpdate()
    {
        speedScore = Vector3.Distance(oldPosition, transform.position) * 100f;
        oldPosition = transform.position;
        //Debug.Log("Speed: " + speedScore.ToString("F0"));
        
        
    }

    CharacterController characterController;
    Animator animator;
    AudioSource audioSource;

    public float baseSpeed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public bool doubleJump = false;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;
    bool walking = false;
    bool playingWalkingSound;

    public float speed = 12f;
    

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = -Input.GetAxis("Vertical");
        float z = Input.GetAxis("Horizontal");

        if (animator && audioSource)
        {
            if (x > 0 || x < 0 || z < 0 || z > 0)
            {
                walking = true;
                animator.SetBool("Walking", true);
            }
            else
            {
                audioSource.Stop();
                animator.SetBool("Walking", false);
                walking = false;
                playingWalkingSound = false;
            }

            if (walking && !playingWalkingSound && isGrounded)
            {
                audioSource.Play();
                playingWalkingSound = true;
            } else if(!isGrounded)
            {
                playingWalkingSound = false;
                audioSource.Stop();
            }
        }

        Vector3 move = transform.forward * -x + transform.right * z;

        characterController.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && (isGrounded || doubleJump))
        {
            if(!isGrounded && doubleJump)
            {
                doubleJump = false;
            }
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }

    public void SpeedBoost(bool crit = false)
    {
        if(crit)
        {
            if (speed == baseSpeed)
            {
                speed += 20;
            }
        }
        else
        {
            if (speed == baseSpeed)
            {
                speed += 10f;
            }
        }

        StartCoroutine(BoostTimer(10));
    }

    public void StartDoubleJump(bool crit = false)
    {
        if (crit)
            StartCoroutine(SetDoubleJump(3));
        else
            StartCoroutine(SetDoubleJump(1));
    }

    public IEnumerator SetDoubleJump(float doubleJumpTimer)
    {
        doubleJump = true;
        yield return new WaitForSeconds(doubleJumpTimer);
        doubleJump = false;
    }

    IEnumerator BoostTimer(float time)
    {
        yield return new WaitForSeconds(time);
        speed = baseSpeed;
    }
}