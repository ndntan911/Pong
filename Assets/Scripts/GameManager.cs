using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private int scorePlayer1, scorePlayer2;
    public Action OnReset;
    [SerializeField] public GameUI gameUI;
    private int maxScore = 4;
    public GameAudio gameAudio;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        gameAudio = GetComponent<GameAudio>();
    }

    private void Start()
    {
        gameUI.OnStartGame += GameUI_OnStartGame;
    }

    private void OnDestroy()
    {
        gameUI.OnStartGame -= GameUI_OnStartGame;
    }

    private void GameUI_OnStartGame()
    {
        scorePlayer1 = 0;
        scorePlayer2 = 0;
        gameUI.UpdateScores(scorePlayer1, scorePlayer2);
        OnReset?.Invoke();
    }

    public void OnScoreZoneReached(int id)
    {

        if (id == 1)
        {
            scorePlayer1++;
        }
        else if (id == 2)
        {
            scorePlayer2++;
        }

        gameUI.UpdateScores(scorePlayer1, scorePlayer2);
        gameUI.HighlightScore(id);

        CheckWin();
    }

    private void CheckWin()
    {
        int winnerId = scorePlayer1 == maxScore ? 1 : scorePlayer2 == maxScore ? 2 : 0;

        if (winnerId != 0)
        {
            gameUI.OnGameEnds(winnerId);
            gameAudio.PlayWinSound();
        }
        else
        {
            OnReset?.Invoke();
            gameAudio.PlayScoreSound();

        }
    }
}
