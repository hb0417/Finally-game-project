using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectionManager : MonoBehaviour
{
    public GameObject nicknamePanel; // 닉네임 입력 패널
    public InputField nicknameInputField; // 닉네임 입력 필드

    private string playerName = ""; // 플레이어 이름

    void Start()
    {
        // 인트로 씬이 시작되면 이전에 저장된 닉네임 삭제
        PlayerPrefs.DeleteKey("PlayerNickname");

        nicknamePanel.SetActive(true); // 닉네임 입력 패널 활성화
    }

   

    public void StartGame()
    {

        playerName = nicknameInputField.text;
        if (!string.IsNullOrEmpty(playerName))
        {
            // 플레이어 이름 저장
            PlayerPrefs.SetString("PlayerNickname", playerName);

            // 게임 씬으로 이동
            SceneManager.LoadScene("Stage1"); // "GameScene"을 실제 게임 씬 이름으로 대체
        }
        else
        {
            Debug.LogWarning("Please enter a nickname.");
        }
    }
}
