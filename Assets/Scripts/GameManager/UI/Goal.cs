using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public RankingManager rankingManager; // 랭킹 매니저
    public RankingDisplay rankingDisplay; // 랭킹 디스플레이
    public GameObject rankingUI; // 랭킹 UI
    private bool hasReachedGoal = false; // Goal에 도달했는지 확인하는 변수

    public string introSceneName = "IntroScene"; // 인트로 씬 이름
    public string gameSceneName = "GameScene"; // 게임 씬 이름
    public string anotherSceneName = "AnotherScene"; // 또 다른 씬 이름

    private PlayerController playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasReachedGoal && other.CompareTag("Player"))
        {
            hasReachedGoal = true; // 중복 실행 방지

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
                playerController = other.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    playerController.enabled = false;
                }
            }
        }
    }

    private void Update()
    {
        if (hasReachedGoal)
        {
            // 마우스 활성화 상태를 지속적으로 확인하고 설정
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    } 

    public void RestartGame()
    {
        // 상태 초기화
        ResetGameState();

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

    private void ResetGameState()
    {
        // 필요한 상태들 초기화
        hasReachedGoal = false; // Goal 도달 상태 초기화
        PlayerPrefs.DeleteAll(); // PlayerPrefs 초기화 예시
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
}
