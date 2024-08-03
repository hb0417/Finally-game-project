using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingUIManager : MonoBehaviour
{
    public Text rankingText; // ��ŷ�� ǥ���� �ؽ�Ʈ UI
    public RankingManager rankingManager; // ��ŷ �Ŵ��� ����
    public GameObject rankingPanel; // ��ŷ �г�

    void Start()
    {
        rankingPanel.SetActive(false); // ���� �� ��ŷ �г� ��Ȱ��ȭ
    }

    // �� ��ư���� ȣ���� �޼���, rankingKey�� �� ���� ��ŷ�� �����ϴ� Ű
    public void ShowRanking(string rankingKey)
    {
        rankingManager.rankingKey = rankingKey;
        List<RankingEntry> ranking = rankingManager.GetRanking();

        rankingText.text = "";
        for (int i = 0; i < ranking.Count; i++)
        {
            rankingText.text += $"{i + 1}. {ranking[i].name} - {ranking[i].time:F2}\n";
        }

        rankingPanel.SetActive(true); // ��ŷ �г� Ȱ��ȭ
    }

    // ��ŷ �г��� �ݴ� �޼���
    public void CloseRanking()
    {
        rankingPanel.SetActive(false);
    }
}
