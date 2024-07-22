using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public enum State
    {
        Walking,
        Climbing
    }


    [Header("Movement")]
    public float walkSpeed;
    public float runSpeed;
    public float climbSpeed;
    private Vector2 curMovementInput;
    private bool isRunning = false;
    public float jumpForce;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;

    private Vector2 mouseDelta;

    [HideInInspector]
    public bool canLook = true;

    [HideInInspector]
    public Rigidbody rb;

    public State state = State.Walking; // 현재 상태

    private Animator animator; // 애니메이터 추가

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // 회전 잠금
        animator = GetComponent<Animator>(); // 애니메이터 컴포넌트 가져오기
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case State.Walking:
                Move();
                break;
            case State.Climbing:
                Climb();
                break;
        }

        UpdateAnimator(); // 애니메이션 상태 업데이트
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }

        if (!IsGrounded() && rb.velocity.y < 0)
        {
            animator.SetBool("IsFalling", true);
        }
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            if (state == State.Walking)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                animator.SetTrigger("Jump");
                animator.SetBool("Grounded", false);
                animator.SetBool("IsFalling", true); // 점프하면 낙하 상태로 전환
            }
            else if (state == State.Climbing)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                animator.SetTrigger("Jump");
                animator.SetBool("Grounded", false);
                animator.SetBool("IsFalling", true); // 점프하면 낙하 상태로 전환
            }
        }
    }

    public void OnRunInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            isRunning = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isRunning = false;
        }
    }

    private void Move()
    {
        float speed = isRunning ? runSpeed : walkSpeed;
        Vector3 moveDir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        Vector3 newPos = rb.position + moveDir * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);
    }

    private void Climb()
    {
        Vector3 climbDir = new Vector3(curMovementInput.x, curMovementInput.y, 0);
        Vector3 newPos = rb.position + climbDir * climbSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        foreach (Ray ray in rays)
        {

            if (Physics.Raycast(ray, 0.1f, groundLayerMask))
            {
                animator.SetBool("Grounded", true); // 땅에 닿으면 Grounded를 true로 설정
                animator.SetBool("IsFalling", false); // 땅에 닿으면 낙하 상태 종료
                animator.SetBool("Jump", false); // 땅에 닿은후 점프 리셋
                return true;
            }

        }

        animator.SetBool("Grounded", false); // 땅에 닿지 않으면 Grounded를 false로 설정
        return false;
    }

    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

    private void UpdateAnimator()
    {
        float speed = curMovementInput.magnitude * (isRunning ? runSpeed : walkSpeed);
        animator.SetFloat("Speed", curMovementInput.magnitude * speed);
        animator.SetBool("Idle", curMovementInput == Vector2.zero && state == State.Walking);
        animator.SetBool("Climb", state == State.Climbing);

        if (animator.GetBool("Grounded"))
        {
            float verticalSpeed = rb.velocity.y;
            animator.SetFloat("VerticalSpeed", verticalSpeed);

            if (verticalSpeed <= 0.1f)
            {
                // Set landing state based on movement input
                float movementMagnitude = curMovementInput.magnitude;
                animator.SetFloat("LandState", movementMagnitude);
            }
        }
    }

    private void OnFootstep(AnimationEvent animationEvent)
    {
        // 발소리 재생 등의 로직을 여기에 추가
        Debug.Log("Footstep event triggered");
    }

    // Animation Event Receiver 함수
    public void OnLand()
    {
        Debug.Log("Player has landed.");
        // 이곳에 착지 후 처리할 로직을 추가할 수 있습니다.
    }

}
