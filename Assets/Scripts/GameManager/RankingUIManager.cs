using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingUIManager : MonoBehaviour
{
    public GameObject rankingPanel; // ��ŷ �г� UI
    public Text rankingText; // ��ŷ�� ǥ���ϴ� �ؽ�Ʈ
    public RankingManager rankingManager; // ��ŷ �Ŵ���

    void Start()
    {
        rankingPanel.SetActive(false); // ���� �� ��ŷ �г� ��Ȱ��ȭ
    }

    public void ShowRanking()
    {
        rankingPanel.SetActive(true); // ��ŷ �г� Ȱ��ȭ
        UpdateRankingDisplay();
    }

    public void HideRanking()
    {
        rankingPanel.SetActive(false); // ��ŷ �г� ��Ȱ��ȭ
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
