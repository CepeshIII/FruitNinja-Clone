using TMPro;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] int currentScore = 0;
    [SerializeField] TextMeshProUGUI scoreLayout;
    [SerializeField] TextMeshProUGUI bestScoreLayout;

    private void Awake()
    {
        UpdateScore();
    }

    public void IncreaseScore(int count)
    {
        currentScore += count;
        CheckScoreRecord();
        UpdateScore();
    }

    private void OnDisable()
    {
        CheckScoreRecord();
    }

    private void CheckScoreRecord()
    {
        var bestScore = PlayerPrefs.GetInt("BestScore");
        if (bestScore < currentScore)
            PlayerPrefs.SetInt("BestScore", currentScore);

    }

    private void UpdateScore()
    {
        if (scoreLayout != null)
            scoreLayout.text = "SCORE: " + currentScore.ToString();
        if (bestScoreLayout != null)
            bestScoreLayout.text = "BEST SCORE: " + PlayerPrefs.GetInt("BestScore").ToString();
    }
}
