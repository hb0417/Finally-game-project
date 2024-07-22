using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public enum State
    {
        Walking,
        Climbing
    }

    public State state = State.Walking;

    private PlayerControls controls;
    public CharacterController controller;
    public float speed = 8f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;
    private Vector2 moveInput;
    private bool jumpInput;
    private bool climbInput;
    public float climbingSpeed = 5f;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.Player.Jump.performed += ctx => jumpInput = true;
        controls.Player.Jump.canceled += ctx => jumpInput = false;

        //controls.Player.Climb.performed += ctx => climbInput = true;
        //controls.Player.Climb.canceled += ctx => climbInput = false;
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    private void Update()
    {
        if (state == State.Climbing)
        {
            ClimbLadder();
        }
        else
        {
            Move();
        }
    }

    void Move()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 motion = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(motion * speed * Time.deltaTime);

        if (jumpInput && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void ClimbLadder()
    {
        float y = moveInput.y;
        Vector3 climbMotion = new Vector3(0, y, 0);
        controller.Move(climbMotion * climbingSpeed * Time.deltaTime);

        if (jumpInput)
        {
            state = State.Walking;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}


