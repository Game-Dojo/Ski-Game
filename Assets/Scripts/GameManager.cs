using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Main references")]
    [SerializeField] private PlayerController player;
    [SerializeField] private Score scoreUI;
    
    public delegate void GameEvent();
    public GameEvent OnGameStart { get; set;}
    public GameEvent OnGameEnd { get; set;}
    public GameEvent OnGameWon { get; set; }
    public GameEvent OnGamePause { get; set; }

    public int Score { get; private set; }
    
    private bool _gameOver = false;
    private SaveManager _saveManager;

    private void Awake()
    {
        _saveManager = GetComponent<SaveManager>();
        _saveManager.DefaultSettings();
        
        print("Audio Setting: " + _saveManager.LoadAudioSetting());
    }

    private void Start()
    {
        print("Score Saved: " + _saveManager.LoadScore());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            InvokePauseEvent();
    }

    public void InvokePauseEvent()
    {
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
        scoreUI.UpdateScore();

        int savedScore = _saveManager.LoadScore();
        if (Score > savedScore)
            _saveManager.SaveScore(Score);
    }
    
    public SaveManager GetSaveManager() => _saveManager;
}
