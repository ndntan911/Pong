using System;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private ScoreText scoreTextLeft, scoreTextRight;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private TextMeshProUGUI volumeValueText;
    public Action OnStartGame;

    public void UpdateScores(int scorePlayer1, int scorePlayer2)
    {
        scoreTextLeft.SetScore(scorePlayer1);
        scoreTextRight.SetScore(scorePlayer2);
    }

    public void HighlightScore(int id)
    {
        if (id == 1)
        {
            scoreTextLeft.Highlight();
        }
        else if (id == 2)
        {
            scoreTextRight.Highlight();
        }
    }

    public void OnStartGameButtonClicked()
    {
        menuUI.SetActive(false);
        OnStartGame?.Invoke();
    }

    public void OnGameEnds(int winnerId)
    {
        menuUI.SetActive(true);
        winText.text = $"Player {winnerId} wins!";
    }

    public void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
        volumeValueText.text = $"{Mathf.RoundToInt(value * 100)}%";
    }
}
