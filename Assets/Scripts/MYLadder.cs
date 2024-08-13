using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사다리같은 오르는 스크립트
public class MYLadder : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent<PlayerController>(out var player)) 
        {
            player.state = PlayerController.State.Climbing;
            player.rb.useGravity = false; // 중력 비활성화
            player.rb.velocity = Vector3.zero; // 사다리 오를 때 속도 초기화
            player.GetComponent<Animator>().SetBool("Climb", true); // 사다리 오르는 애니메이션 활성화
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent<PlayerController>(out var player))
        {
            player.state = PlayerController.State.Walking;
            player.rb.useGravity = true; // 중력 활성화
            player.GetComponent<Animator>().SetBool("Climb", false); // 사다리 오르는 애니메이션 비활성화
        }
    }
}
