using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText; // 타이머를 표시할 텍스트 UI
    private float time = 0f; // 경과 시간
    private bool timerActive = false; // 타이머 작동 여부

    void Start()
    {
        timerText.text = "00:00.00";
        timerText.gameObject.SetActive(true); // 타이머 텍스트 활성화
        StartTimer(); // 씬 시작 시 타이머 시작
    }

    void Update()
    {
        if (timerActive)
        {
            time += Time.deltaTime;
            UpdateTimerText();
        }
    }

    public void StartTimer()
    {
        timerActive = true;
        time = 0f;
    }

    public void StopTimer()
    {
        timerActive = false;
    }

    //타이머의 경과 시간을 반환하는 메서드
    public float GetElapsedTime()
    {
        return time;
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 100) % 100);
        timerText.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopTimer();
        }
    }
}
