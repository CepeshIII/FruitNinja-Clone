using System;
using TMPro;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] int currentScore = 0;
    [SerializeField] int missFruitScore = 0;

    [SerializeField] TextMeshProUGUI scoreLayout;
    [SerializeField] TextMeshProUGUI bestScoreLayout;
    [SerializeField] MissFruitDisplayer missFruitShower;

    private int maxMissFruitCount = 3;

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

    public void IncreaseMissFruitScore(int count)
    {
        missFruitScore += count;
        UpdateMissFruitScore();
    }

    private void UpdateMissFruitScore()
    {
        missFruitShower.Display(maxMissFruitCount, missFruitScore);
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

    internal bool CheckIfGameOverByLoseFruit()
    {
        return missFruitScore > maxMissFruitCount;
    }
}
