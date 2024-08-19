using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RankingEntry
{
    public string name;
    public float time;

    public RankingEntry(string name, float time)
    {
        this.name = name;
        this.time = time;
    }
}

public class RankingManager : MonoBehaviour
{

    public string rankingKey; // ∞¢ æ¿∫∞ ∞Ì¿Ø«— ∑©≈∑ ≈∞

    public List<RankingEntry> GetRanking()
    {
        List<RankingEntry> ranking = new List<RankingEntry>();
        int count = PlayerPrefs.GetInt(rankingKey + "_Count", 0);

        for (int i = 0; i < count; i++)
        {
            string entry = PlayerPrefs.GetString(rankingKey + "_" + i, "");
            if (!string.IsNullOrEmpty(entry))
            {
                string[] parts = entry.Split('-');
                string name = parts[0].Trim();
                float time = float.Parse(parts[1]);
                ranking.Add(new RankingEntry(name, time));
            }
        }

        ranking.Sort((a, b) => a.time.CompareTo(b.time));
        return ranking;
    }

    public void AddEntry(string name, float time)
    {
        List<RankingEntry> ranking = GetRanking();
        ranking.Add(new RankingEntry(name, time));

        ranking.Sort((a, b) => a.time.CompareTo(b.time));
        SaveRanking(ranking);
    }

    private void SaveRanking(List<RankingEntry> ranking)
    {
        PlayerPrefs.SetInt(rankingKey + "_Count", ranking.Count);

        for (int i = 0; i < ranking.Count; i++)
        {
            string entry = ranking[i].name + " - " + ranking[i].time;
            PlayerPrefs.SetString(rankingKey + "_" + i, entry);
        }
    }
}
