using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectionManager : MonoBehaviour
{
    public GameObject nicknamePanel; // 닉네임 입력 패널
    public GameObject characterSelectionPanel; // 캐릭터 선택 패널
    public InputField nicknameInputField; // 닉네임 입력 필드
    public Button startGameButton; // 게임 시작 버튼

    private string selectedCharacter = ""; // 선택된 캐릭터
    private string playerName = ""; // 플레이어 이름

    void Start()
    {
        // 인트로 씬이 시작되면 이전에 저장된 닉네임 삭제
        PlayerPrefs.DeleteKey("PlayerNickname");

        characterSelectionPanel.SetActive(false); // 캐릭터 선택 패널 비활성화
        nicknamePanel.SetActive(true); // 닉네임 입력 패널 활성화
        startGameButton.interactable = false; // 선택되지 않은 상태에서는 시작 버튼 비활성화
    }

    public void OpenCharacterSelection()
    {
        playerName = nicknameInputField.text;
        if (!string.IsNullOrEmpty(playerName))
        {
            PlayerPrefs.SetString("PlayerNickname", playerName); // 닉네임 저장
            nicknamePanel.SetActive(false); // 닉네임 입력 패널 비활성화
            characterSelectionPanel.SetActive(true); // 캐릭터 선택 패널 활성화
        }
        else
        {
            Debug.LogWarning("Please enter a nickname.");
        }
    }

    public void SelectCharacter(string characterName)
    {
        selectedCharacter = characterName;
        startGameButton.interactable = true; // 캐릭터 선택 시 시작 버튼 활성화
    }

    public void StartGame()
    {
        if (!string.IsNullOrEmpty(selectedCharacter) && !string.IsNullOrEmpty(playerName))
        {
            PlayerPrefs.SetString("SelectedCharacter", selectedCharacter); // 선택된 캐릭터 저장
            SceneManager.LoadScene(3); // 지정된 게임 씬으로 이동
        }
        else
        {
            Debug.LogWarning("Character or nickname not selected.");
        }
    }
}
