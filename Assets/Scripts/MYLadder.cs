using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ٸ����� ������ ��ũ��Ʈ
public class MYLadder : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent<PlayerController>(out var player)) 
        {
            player.state = PlayerController.State.Climbing;
            player.rb.useGravity = false; // �߷� ��Ȱ��ȭ
            player.rb.velocity = Vector3.zero; // ��ٸ� ���� �� �ӵ� �ʱ�ȭ
            player.GetComponent<Animator>().SetBool("Climb", true); // ��ٸ� ������ �ִϸ��̼� Ȱ��ȭ
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent<PlayerController>(out var player))
        {
            player.state = PlayerController.State.Walking;
            player.rb.useGravity = true; // �߷� Ȱ��ȭ
            player.GetComponent<Animator>().SetBool("Climb", false); // ��ٸ� ������ �ִϸ��̼� ��Ȱ��ȭ
        }
    }
}
