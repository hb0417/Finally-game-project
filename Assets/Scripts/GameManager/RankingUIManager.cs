using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingUIManager : MonoBehaviour
{
    public Text rankingText; // 랭킹을 표시할 텍스트 UI
    public RankingManager rankingManager; // 랭킹 매니저 참조
    public GameObject rankingPanel; // 랭킹 패널

    void Start()
    {
        rankingPanel.SetActive(false); // 시작 시 랭킹 패널 비활성화
    }

    // 각 버튼에서 호출할 메서드, rankingKey는 각 씬의 랭킹을 구분하는 키
    public void ShowRanking(string rankingKey)
    {
        rankingManager.rankingKey = rankingKey;
        List<RankingEntry> ranking = rankingManager.GetRanking();

        rankingText.text = "";
        for (int i = 0; i < ranking.Count; i++)
        {
            rankingText.text += $"{i + 1}. {ranking[i].name} - {ranking[i].time:F2}\n";
        }

        rankingPanel.SetActive(true); // 랭킹 패널 활성화
    }

    // 랭킹 패널을 닫는 메서드
    public void CloseRanking()
    {
        rankingPanel.SetActive(false);
    }
}
