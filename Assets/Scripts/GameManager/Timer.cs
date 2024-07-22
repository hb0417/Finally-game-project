using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText; // Ÿ�̸Ӹ� ǥ���� �ؽ�Ʈ UI
    private float time = 0f; // ��� �ð�
    private bool timerActive = false; // Ÿ�̸� �۵� ����

    void Start()
    {
        timerText.text = "00:00.00";
        timerText.gameObject.SetActive(true); // Ÿ�̸� �ؽ�Ʈ Ȱ��ȭ
        StartTimer(); // �� ���� �� Ÿ�̸� ����
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

    //Ÿ�̸��� ��� �ð��� ��ȯ�ϴ� �޼���
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
