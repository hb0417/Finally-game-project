using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostItem : MonoBehaviour
{
    public float speedMultiplier = 2.0f;
    public float duration = 5.0f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                StartCoroutine(playerController.SpeedBoost(speedMultiplier, duration));
                Destroy(gameObject); // ¾ÆÀÌÅÛÀ» È¹µæÇÑ ÈÄ ÆÄ±«
            }
        }
    }
}
