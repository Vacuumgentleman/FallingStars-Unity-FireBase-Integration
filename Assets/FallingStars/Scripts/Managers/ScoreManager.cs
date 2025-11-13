using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameTimer gameTimer;
    [SerializeField] private TextMeshProUGUI scoreText;

    private int currentScore;
    private const int scoreMultiplier = 100;
    private bool isActive = true;

    private void Update()
    {
        if (!isActive || gameTimer == null) return;

        CalculateScore();
        UpdateScoreDisplay();
    }

    private void CalculateScore()
    {
        currentScore = Mathf.FloorToInt(gameTimer.GetElapsedTime() * scoreMultiplier);
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText == null) return;
        scoreText.text = $"{currentScore}";
    }

    public void StopScore()
    {
        isActive = false;
    }

    public int GetFinalScore()
    {
        return currentScore;
    }

    public void ResetScore()
    {
        currentScore = 0;
        isActive = true;
    }
}
