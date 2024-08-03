using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public RankingManager rankingManager; // ��ŷ �Ŵ���
    public RankingDisplay rankingDisplay; // ��ŷ ���÷���
    public GameObject rankingUI; // ��ŷ UI
    public string introSceneName = "IntroScene"; // ��Ʈ�� �� �̸�
    public string gameSceneName = "GameScene"; // ���� �� �̸�
    public string anotherSceneName = "AnotherScene"; // �� �ٸ� �� �̸�
    private PlayerController playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Timer timer = FindObjectOfType<Timer>();
            if (timer != null)
            {
                timer.StopTimer();
                float time = timer.GetElapsedTime(); // ��� �ð� ��������
                string playerName = PlayerPrefs.GetString("PlayerNickname", "Player"); // ����� �г��� ��������

                // �÷��̾� �̸��� �ð� ����� ��ŷ�� �߰�
                rankingManager.AddEntry(playerName, time);

                // ��ŷ UI ������Ʈ �� Ȱ��ȭ
                rankingDisplay.UpdateRankingDisplay();
                rankingUI.SetActive(true);

                // ���� �Ͻ����� �� ���콺 Ȱ��ȭ
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                // ĳ���� ��Ʈ�� ��Ȱ��ȭ
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
        // ���� �Ͻ����� ���� �� ���콺 ���
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // ĳ���� ��Ʈ�� Ȱ��ȭ
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        // ���� �� �ٽ� �ε�
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoToIntro()
    {
        // ���� �Ͻ����� ����
        Time.timeScale = 1f;

        // ��Ʈ�� ������ �̵�
        SceneManager.LoadScene(introSceneName);
    }

    public void GoToAnotherScene()
    {
        // ���� �Ͻ����� ����
        Time.timeScale = 1f;

        // �ٸ� ������ �̵�
        SceneManager.LoadScene(anotherSceneName);
    }

    // ��ŷ ��� �ʱ�ȭ
    public void ClearRankings()
    {
        //rankingManager.ClearRankings();
        rankingDisplay.UpdateRankingDisplay();
    }
}
