using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingDisplay : MonoBehaviour
{
    public RankingManager rankingManager;
    public Text rankingText; // 랭킹을 표시할 UI 텍스트

    void Start()
    {
        rankingText.gameObject.SetActive(false); // 처음에는 비활성화
    }

    public void UpdateRankingDisplay()
    {
        List<RankingEntry> rankings = rankingManager.GetRankings();
        rankingText.text = "";
        for (int i = 0; i < rankings.Count; i++)
        {
            rankingText.text += (i + 1) + ". " + rankings[i].playerName + " - " + rankings[i].time.ToString("F2") + "\n";
        }
    }
}
