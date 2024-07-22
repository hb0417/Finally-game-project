using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RankingEntry
{
    public string playerName;
    public float time;

    public RankingEntry(string playerName, float time)
    {
        this.playerName = playerName;
        this.time = time;
    }
}

public class RankingManager : MonoBehaviour
{

    public List<RankingEntry> rankings = new List<RankingEntry>();

    void Awake()
    {
        LoadRankings(); // 게임 시작 시 랭킹 불러오기
    }

    public void AddEntry(string playerName, float time)
    {
        rankings.Add(new RankingEntry(playerName, time));
        rankings.Sort((a, b) => a.time.CompareTo(b.time)); // 시간을 기준으로 오름차순 정렬
        SaveRankings(); // 랭킹 저장
    }

    public List<RankingEntry> GetRankings()
    {
        return rankings;
    }

    public void SaveRankings()
    {
        PlayerPrefs.SetInt("RankingCount", rankings.Count);
        for (int i = 0; i < rankings.Count; i++)
        {
            PlayerPrefs.SetString("RankingEntry_" + i + "_Name", rankings[i].playerName);
            PlayerPrefs.SetFloat("RankingEntry_" + i + "_Time", rankings[i].time);
        }
        PlayerPrefs.Save();
    }

    public void LoadRankings()
    {
        rankings.Clear();
        int count = PlayerPrefs.GetInt("RankingCount", 0);
        for (int i = 0; i < count; i++)
        {
            string name = PlayerPrefs.GetString("RankingEntry_" + i + "_Name", "Player");
            float time = PlayerPrefs.GetFloat("RankingEntry_" + i + "_Time", 0f);
            rankings.Add(new RankingEntry(name, time));
        }
    }

    // 랭킹 초기화
    public void ClearRankings()
    {
        rankings.Clear();
        PlayerPrefs.DeleteAll();
    }
}
