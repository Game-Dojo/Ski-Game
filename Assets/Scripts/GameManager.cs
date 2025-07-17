using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Main references")]
    [SerializeField] private PlayerController player;
    
    public delegate void GameEvent();
    public GameEvent OnGameStart { get; }
    public GameEvent OnGameEnd { get; }
    public GameEvent OnGameWon { get; }
    
    public int Score { get; private set; }
    
    private bool _gameOver = false;

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
