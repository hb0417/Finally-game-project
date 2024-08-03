using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public RankingManager rankingManager; // 랭킹 매니저
    public RankingDisplay rankingDisplay; // 랭킹 디스플레이
    public GameObject rankingUI; // 랭킹 UI
    public string introSceneName = "IntroScene"; // 인트로 씬 이름
    public string gameSceneName = "GameScene"; // 게임 씬 이름
    public string anotherSceneName = "AnotherScene"; // 또 다른 씬 이름
    private PlayerController playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Timer timer = FindObjectOfType<Timer>();
            if (timer != null)
            {
                timer.StopTimer();
                float time = timer.GetElapsedTime(); // 경과 시간 가져오기
                string playerName = PlayerPrefs.GetString("PlayerNickname", "Player"); // 저장된 닉네임 가져오기

                // 플레이어 이름과 시간 기록을 랭킹에 추가
                rankingManager.AddEntry(playerName, time);

                // 랭킹 UI 업데이트 및 활성화
                rankingDisplay.UpdateRankingDisplay();
                rankingUI.SetActive(true);

                // 게임 일시정지 및 마우스 활성화
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                // 캐릭터 컨트롤 비활성화
                PlayerController playerController = other.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    playerController.enabled = false;
                }
            }
        }
    }

    public void RestartGame()
    {
        // 게임 일시정지 해제 및 마우스 잠금
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 캐릭터 컨트롤 활성화
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        // 현재 씬 다시 로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoToIntro()
    {
        // 게임 일시정지 해제
        Time.timeScale = 1f;

        // 인트로 씬으로 이동
        SceneManager.LoadScene(introSceneName);
    }

    public void GoToAnotherScene()
    {
        // 게임 일시정지 해제
        Time.timeScale = 1f;

        // 다른 씬으로 이동
        SceneManager.LoadScene(anotherSceneName);
    }

    // 랭킹 기록 초기화
    public void ClearRankings()
    {
        //rankingManager.ClearRankings();
        rankingDisplay.UpdateRankingDisplay();
    }
}
