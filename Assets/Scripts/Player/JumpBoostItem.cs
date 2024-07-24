using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoostItem : MonoBehaviour
{
    public float jumpMultiplier = 1.5f;
    public float duration = 5.0f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                StartCoroutine(playerController.JumpBoost(jumpMultiplier, duration));
                Destroy(gameObject); // ¾ÆÀÌÅÛÀ» È¹µæÇÑ ÈÄ ÆÄ±«
            }
        }
    }
}
