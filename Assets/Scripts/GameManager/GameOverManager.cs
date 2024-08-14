using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel; // 게임 오버 패널

    private void Start()
    {
        gameOverPanel.SetActive(false); // 시작 시 게임 오버 패널 비활성화
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f; // 게임 멈추기
        gameOverPanel.SetActive(true); // 게임 오버 패널 활성화

        Cursor.lockState = CursorLockMode.None; // 마우스 커서 활성화
        Cursor.visible = true;

        // 마우스 커서 상태 확인용 디버그 로그
        Debug.Log("Cursor.lockState: " + Cursor.lockState);
        Debug.Log("Cursor.visible: " + Cursor.visible);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // 게임 속도 재설정
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬 다시 로드

        // 게임 재시작 후 커서 설정 확인
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void GoToIntro()
    {
        Time.timeScale = 1f; // 게임 속도 재설정
        SceneManager.LoadScene("IntroScene"); // 인트로 씬으로 이동

        // 인트로 씬으로 이동 후 커서 설정 확인
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
