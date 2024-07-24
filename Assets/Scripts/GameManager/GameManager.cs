using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform spawnPoint; // �÷��̾� ���� ��ġ
    public GameObject[] characterPrefabs; // ĳ���� ������ �迭

    void Start()
    {
        string playerName = PlayerPrefs.GetString("PlayerNickname", "Player");
        string selectedCharacter = PlayerPrefs.GetString("SelectedCharacter", "Character1");

        Debug.Log("Player Name: " + playerName);
        Debug.Log("Selected Character: " + selectedCharacter);

        GameObject characterPrefab = GetCharacterPrefab(selectedCharacter);
        if (characterPrefab != null)
        {
            Instantiate(characterPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogError("Selected character prefab not found!");
        }
    }

    private GameObject GetCharacterPrefab(string characterName)
    {
        foreach (GameObject prefab in characterPrefabs)
        {
            if (prefab.name == characterName)
            {
                return prefab;
            }
        }
        return null;
    }
}


