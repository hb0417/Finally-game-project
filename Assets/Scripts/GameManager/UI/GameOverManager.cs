using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel; // ���� ���� �г�

    private void Start()
    {
        gameOverPanel.SetActive(false); // ���� �� ���� ���� �г� ��Ȱ��ȭ
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
        // ���� �Ͻ����� �� ���콺 Ȱ��ȭ
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameOverPanel.SetActive(true); // ���� ���� �г� Ȱ��ȭ
        /*
        Time.timeScale = 0f; // ���� ���߱�
        gameOverPanel.SetActive(true); // ���� ���� �г� Ȱ��ȭ

        Cursor.lockState = CursorLockMode.None; // ���콺 Ŀ�� Ȱ��ȭ
        Cursor.visible = true;
        */
    }


    public void RestartGame()
    {
        Time.timeScale = 1f; // ���� �ӵ� �缳��
        Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ�� ���
        Cursor.visible = false;

        // ���� �ٽ� �ε��Ͽ� ��� ������Ʈ�� ������ ���� �ʱ�ȭ
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToIntro()
    {
        Time.timeScale = 1f; // ���� �ӵ� �缳��
        Cursor.lockState = CursorLockMode.None; // ���콺 Ŀ�� Ȱ��ȭ
        Cursor.visible = true;
        SceneManager.LoadScene("Intro"); // ��Ʈ�� ������ �̵�
    }

}
