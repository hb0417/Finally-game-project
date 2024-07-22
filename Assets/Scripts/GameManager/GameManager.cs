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

        // 선택된 캐릭터에 따라 캐릭터 생성 로직 추가
        // 예: Instantiate(characterPrefab, spawnPosition, spawnRotation);
    }
}
