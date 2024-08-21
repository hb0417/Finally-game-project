using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NicknameManager : MonoBehaviour
{
    public InputField nicknameInputField; // 닉네임 입력 필드
    public Button startGameButton; // 게임 시작 버튼

    private string playerName = ""; // 플레이어 이름

    void Start()
    {
        startGameButton.interactable = false; // 시작 시 버튼 비활성화
        nicknameInputField.onValueChanged.AddListener(OnNicknameChanged);
    }

    // 닉네임 입력 필드의 값이 변경될 때 호출
    public void OnNicknameChanged(string nickname)
    {
        playerName = nickname;
        startGameButton.interactable = !string.IsNullOrEmpty(playerName); // 닉네임이 입력되면 버튼 활성화
    }

    public void StartGame()
    {
        if (!string.IsNullOrEmpty(playerName))
        {
            // 플레이어 이름 저장
            PlayerPrefs.SetString("PlayerNickname", playerName);

            // 마우스 커서 비활성화 및 잠금
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // 게임 씬으로 이동
            SceneManager.LoadScene("Stage1"); // "GameScene"을 실제 게임 씬 이름으로 대체
        }
        else
        {
            Debug.LogWarning("Please enter a nickname.");
        }
    }

    // 인트로로 돌아갈 때 닉네임을 삭제
    public void ResetNickname()
    {
        PlayerPrefs.DeleteKey("PlayerNickname"); // 저장된 닉네임 삭제
        SceneManager.LoadScene("Intro"); // 인트로 씬으로 이동
    }
}
