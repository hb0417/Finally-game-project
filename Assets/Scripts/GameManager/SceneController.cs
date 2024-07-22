using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public InputField nicknameInputField;

    public void StartGame()
    {
        string nickname = nicknameInputField.text;
        if (!string.IsNullOrEmpty(nickname))
        {
            PlayerPrefs.SetString("PlayerNickname", nickname); // �г��� ����
            SceneManager.LoadScene("TestScene"); // ���� ������ �̵�
        }
    }
}
