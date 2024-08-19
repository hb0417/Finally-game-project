using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingDisplay : MonoBehaviour
{
    public Text rankingText; // 랭킹을 표시할 텍스트 UI
    public RankingManager rankingManager; // 랭킹 매니저 참조

    public void UpdateRankingDisplay()
    {
        List<RankingEntry> ranking = rankingManager.GetRanking();

        rankingText.text = "";
        for (int i = 0; i < ranking.Count; i++)
        {
            rankingText.text += $"{i + 1}. {ranking[i].name} - {ranking[i].time:F2}\n";
        }
    }
}
