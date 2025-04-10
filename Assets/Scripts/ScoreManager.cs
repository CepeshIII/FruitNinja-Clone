using TMPro;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] int currentScore = 0;
    [SerializeField] TextMeshProUGUI scoreLayout;


    public void IncreaseScore(int count)
    {
        currentScore += count;
        if(scoreLayout != null) 
            scoreLayout.text = "SCORE: " + currentScore.ToString();
    }

}
