using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Main references")]
    [SerializeField] private PlayerController player;
    
    public delegate void GameEvent();
    public GameEvent OnGameStart { get; set;}
    public GameEvent OnGameEnd { get; set;}
    public GameEvent OnGameWon { get; set; }
    
    public GameEvent OnGamePause { get; set; }

    public int Score { get; private set; }
    
    private bool _gameOver = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OnGamePause?.Invoke();
    }

    public void GameStart()
    {
        _gameOver = false;
        OnGameStart?.Invoke();
    }
    public void SetGameOver()
    {
        _gameOver = true;
        OnGameEnd?.Invoke();
    }
    public void SetGameWon()
    {
        _gameOver = true;
        OnGameWon?.Invoke();
    }

    public void SetScore(int amount)
    {
        Score += amount;
    }
}
