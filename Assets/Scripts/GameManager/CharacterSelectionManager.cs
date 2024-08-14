using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectionManager : MonoBehaviour
{
    public GameObject nicknamePanel; // �г��� �Է� �г�
    public GameObject characterSelectionPanel; // ĳ���� ���� �г�
    public InputField nicknameInputField; // �г��� �Է� �ʵ�
    public Button startGameButton; // ���� ���� ��ư

    private string selectedCharacter = ""; // ���õ� ĳ����
    private string playerName = ""; // �÷��̾� �̸�

    void Start()
    {
        // ��Ʈ�� ���� ���۵Ǹ� ������ ����� �г��� ����
        PlayerPrefs.DeleteKey("PlayerNickname");

        characterSelectionPanel.SetActive(false); // ĳ���� ���� �г� ��Ȱ��ȭ
        nicknamePanel.SetActive(true); // �г��� �Է� �г� Ȱ��ȭ
        startGameButton.interactable = false; // ���õ��� ���� ���¿����� ���� ��ư ��Ȱ��ȭ
    }

    public void OpenCharacterSelection()
    {
        playerName = nicknameInputField.text;
        if (!string.IsNullOrEmpty(playerName))
        {
            PlayerPrefs.SetString("PlayerNickname", playerName); // �г��� ����
            nicknamePanel.SetActive(false); // �г��� �Է� �г� ��Ȱ��ȭ
            characterSelectionPanel.SetActive(true); // ĳ���� ���� �г� Ȱ��ȭ
        }
        else
        {
            Debug.LogWarning("Please enter a nickname.");
        }
    }

    public void SelectCharacter(string characterName)
    {
        selectedCharacter = characterName;
        startGameButton.interactable = true; // ĳ���� ���� �� ���� ��ư Ȱ��ȭ
    }

    public void StartGame()
    {
        if (!string.IsNullOrEmpty(selectedCharacter) && !string.IsNullOrEmpty(playerName))
        {
            PlayerPrefs.SetString("SelectedCharacter", selectedCharacter); // ���õ� ĳ���� ����
            SceneManager.LoadScene(3); // ������ ���� ������ �̵�
        }
        else
        {
            Debug.LogWarning("Character or nickname not selected.");
        }
    }
}
