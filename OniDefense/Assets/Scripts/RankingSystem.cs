using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class RankingSystem
{
    private static string filePath = Application.persistentDataPath + "/ranking.json";
    private const int maxEntries = 5;

    [Serializable]
    public class ScoreEntry
    {
        public string playerName;
        public int score;
    }

    [Serializable]
    public class ScoreList
    {
        public List<ScoreEntry> scores = new List<ScoreEntry>();
    }

    // Load scores from the file
    public static ScoreList LoadScores()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<ScoreList>(json);
        }
        return new ScoreList();
    }

    // Save scores to the file
    public static void SaveScores(ScoreList scoreList)
    {
        string json = JsonUtility.ToJson(scoreList, true);
        File.WriteAllText(filePath, json);
    }

    // Add a new score and update ranking
    public static void AddNewScore(string playerName, int newScore)
    {
        ScoreList scoreList = LoadScores();

        // Add the new score
        scoreList.scores.Add(new ScoreEntry { playerName = playerName, score = newScore });

        // Sort and keep only the top 5
        scoreList.scores.Sort((a, b) => b.score.CompareTo(a.score));
        if (scoreList.scores.Count > maxEntries)
        {
            scoreList.scores.RemoveAt(scoreList.scores.Count - 1);
        }

        // Save updated scores
        SaveScores(scoreList);
    }
}