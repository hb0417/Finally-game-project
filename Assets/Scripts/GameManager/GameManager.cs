using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        string playerName = PlayerPrefs.GetString("PlayerNickname", "Player");
        string selectedCharacter = PlayerPrefs.GetString("SelectedCharacter", "Character1");

        Debug.Log("Player Name: " + playerName);
        Debug.Log("Selected Character: " + selectedCharacter);

        // ���õ� ĳ���Ϳ� ���� ĳ���� ���� ���� �߰�
        // ��: Instantiate(characterPrefab, spawnPosition, spawnRotation);
    }
}
