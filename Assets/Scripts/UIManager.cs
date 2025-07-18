using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup wonPanel;
    
    private GameManager _manager;
    private void Start()
    {
        _manager = FindAnyObjectByType<GameManager>();
        _manager.OnGameWon += ShowWonPanel;
        _manager.OnGameEnd += ShowGameOverPanel;
        _manager.OnGameStart += ShowGameStart;
    }
    private void ShowGameStart()
    {
    }
    private void ShowGameOverPanel()
    {
    }
    private void ShowWonPanel()
    {
        wonPanel.alpha = 1;
    }

    private void OnDestroy()
    {
        _manager.OnGameWon -= ShowWonPanel;
        _manager.OnGameEnd -= ShowGameOverPanel;
        _manager.OnGameStart -= ShowGameStart;
    }
}
