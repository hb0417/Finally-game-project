using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectionManager : MonoBehaviour
{
    public GameObject nicknamePanel; // �г��� �Է� �г�
    public InputField nicknameInputField; // �г��� �Է� �ʵ�

    private string playerName = ""; // �÷��̾� �̸�

    void Start()
    {
        // ��Ʈ�� ���� ���۵Ǹ� ������ ����� �г��� ����
        PlayerPrefs.DeleteKey("PlayerNickname");

        nicknamePanel.SetActive(true); // �г��� �Է� �г� Ȱ��ȭ
    }

   

    public void StartGame()
    {

        playerName = nicknameInputField.text;
        if (!string.IsNullOrEmpty(playerName))
        {
            // �÷��̾� �̸� ����
            PlayerPrefs.SetString("PlayerNickname", playerName);

            // ���� ������ �̵�
            SceneManager.LoadScene("Stage1"); // "GameScene"�� ���� ���� �� �̸����� ��ü
        }
        else
        {
            Debug.LogWarning("Please enter a nickname.");
        }
    }
}
