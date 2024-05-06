using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public enum Score
    {
        AIScore, PlayerScore
    }
    private int maxScore = 5;

    public UIManager uIManager;
    #region  Scores
    private int aiScore, playerScore;

    private int AIScore
    {
        get { return aiScore; }
        set
        {
            aiScore = value;
            if (value == maxScore)
            {
                uIManager.ShowRestartCanvas(true);
            }
        }
    }
    private int PlayerScore
    {
        get => playerScore;
        set
        {
            playerScore = value;
            if (value == maxScore)
            {
                uIManager.ShowRestartCanvas(false);
            }
        }
    }

    #endregion


    public TextMeshProUGUI AIScoreText, PlayerScoreText;

    public void Increment(Score whichScore)
    {
        if (whichScore == Score.AIScore)
        {
            AIScoreText.text = (++AIScore).ToString();
        }
        else
        {
            PlayerScoreText.text = (++PlayerScore).ToString();
        }
    }

    public void Decrement(Score whichScore)
    {
        if (whichScore == Score.AIScore)
        {
            AIScoreText.text = (--AIScore).ToString();
        }
        else
        {
            PlayerScoreText.text = (--PlayerScore).ToString();
        }
    }
    public void ResetScores()
    {
        AIScore = 0;
        PlayerScore = 0;
        AIScoreText.text = "0";
        PlayerScoreText.text = "0";
    }
}
