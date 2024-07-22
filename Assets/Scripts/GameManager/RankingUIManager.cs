using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingUIManager : MonoBehaviour
{
    public GameObject rankingPanel; // 랭킹 패널 UI
    public Text rankingText; // 랭킹을 표시하는 텍스트
    public RankingManager rankingManager; // 랭킹 매니저

    void Start()
    {
        rankingPanel.SetActive(false); // 시작 시 랭킹 패널 비활성화
    }

    public void ShowRanking()
    {
        rankingPanel.SetActive(true); // 랭킹 패널 활성화
        UpdateRankingDisplay();
    }

    public void HideRanking()
    {
        rankingPanel.SetActive(false); // 랭킹 패널 비활성화
    }

    public void UpdateRankingDisplay()
    {
        List<RankingEntry> rankings = rankingManager.GetRankings();
        rankingText.text = "";
        for (int i = 0; i < rankings.Count; i++)
        {
            rankingText.text += $"{i + 1}. {rankings[i].playerName} - {rankings[i].time:F2}\n";
        }
    }

    public void ClearRankings()
    {
        rankingManager.ClearRankings();
        UpdateRankingDisplay();
    }
}
