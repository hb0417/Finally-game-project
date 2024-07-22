using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

// 실패작 사다리타기 더 연구할것

public class LadderClimb : MonoBehaviour
{
    public bool isClimbing = false;
    public bool isEnterLadder = false;
    public bool isNearLadder = false;

    bool isUpLadder = false;
    bool isDownLadder = false;

    Vector3 LadderPosition;
    Vector3 LadderRotation;

    Animator animator;

    CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        TryGetComponent(out animator);
    }

    public void characterToLadder()
    {
        Vector3 targetPosition = new Vector3(LadderPosition.x, transform.position.y, LadderPosition.z);
        Vector3 direction = (targetPosition - transform.position).normalized;

        float distanceToLadder = Vector3.Distance(transform.position, targetPosition);

        characterController.Move(direction * distanceToLadder);

        LookAtLadder();
        LadderStart();
    }

    public void LookAtLadder()
    {
        Vector3 Up = transform.rotation * Vector3.up;
        transform.rotation = Quaternion.AngleAxis(Vector3.SignedAngle(transform.forward, LadderRotation, Up), Up) * transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            NearLadderPosition(other);
            isUpLadder = true;
            isDownLadder = false;
        }
        else if (other.CompareTag("LadderDownEnd"))
        {
            EndOfLadder("LadderDownEnd");
        }
        else if (other.CompareTag("LadderUpStart"))
        {
            NearLadderPosition(other);
            isUpLadder = false;
            isDownLadder = true;
        }
        else if (other.CompareTag("LadderUpEnd"))
        {
            EndOfLadder("LadderUpEnd");
        }
    }

    private void NearLadderPosition(Collider other)
    {
        isNearLadder = true;
        LadderPosition = other.gameObject.transform.position;
        LadderRotation = other.gameObject.transform.forward;
    }

    private void EndOfLadder(string animName)
    {
        if (isEnterLadder && isClimbing)
        {
            animator.SetTrigger(animName);
            animator.SetBool("Ladder", false);
        }
    }

    public void LadderStart()
    {
        animator.SetBool("Ladder", true);

        if (isUpLadder)
        {
            animator.SetTrigger("LadderDownStart");
        }

        if (isDownLadder)
        {
            animator.SetTrigger("LadderUpStart");
        }

        isUpLadder = false;
        isDownLadder = false;

        isNearLadder = false;
        isEnterLadder = true;
    }

    public void EnterLadder()
    {
        isClimbing = true;
    }

    public void ExitLadder()
    {
        LookAtLadder();
        isClimbing = false;
        isEnterLadder = false;
    }
}
