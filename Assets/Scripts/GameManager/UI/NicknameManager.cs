using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NicknameManager : MonoBehaviour
{
    public InputField nicknameInputField; // �г��� �Է� �ʵ�
    public Button startGameButton; // ���� ���� ��ư

    private string playerName = ""; // �÷��̾� �̸�

    void Start()
    {
        startGameButton.interactable = false; // ���� �� ��ư ��Ȱ��ȭ
        nicknameInputField.onValueChanged.AddListener(OnNicknameChanged);
    }

    // �г��� �Է� �ʵ��� ���� ����� �� ȣ��
    public void OnNicknameChanged(string nickname)
    {
        playerName = nickname;
        startGameButton.interactable = !string.IsNullOrEmpty(playerName); // �г����� �ԷµǸ� ��ư Ȱ��ȭ
    }

    public void StartGame()
    {
        if (!string.IsNullOrEmpty(playerName))
        {
            // �÷��̾� �̸� ����
            PlayerPrefs.SetString("PlayerNickname", playerName);

            // ���콺 Ŀ�� ��Ȱ��ȭ �� ���
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // ���� ������ �̵�
            SceneManager.LoadScene("Stage1"); // "GameScene"�� ���� ���� �� �̸����� ��ü
        }
        else
        {
            Debug.LogWarning("Please enter a nickname.");
        }
    }

    // ��Ʈ�η� ���ư� �� �г����� ����
    public void ResetNickname()
    {
        PlayerPrefs.DeleteKey("PlayerNickname"); // ����� �г��� ����
        SceneManager.LoadScene("Intro"); // ��Ʈ�� ������ �̵�
    }
}
