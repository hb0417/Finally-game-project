using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingDisplay : MonoBehaviour
{
    public RankingManager rankingManager;
    public Text rankingText; // ��ŷ�� ǥ���� UI �ؽ�Ʈ

    void Start()
    {
        rankingText.gameObject.SetActive(false); // ó������ ��Ȱ��ȭ
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
