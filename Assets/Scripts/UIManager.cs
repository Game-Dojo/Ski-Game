using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup wonPanel;
    [SerializeField] private CanvasGroup pausePanel;
    
    private GameManager _manager;
    
    private bool _isPaused = false;
    
    private void Start()
    {
        _manager = FindAnyObjectByType<GameManager>();
        
        _manager.OnGameWon += ShowWonPanel;
        _manager.OnGameEnd += ShowGameOverPanel;
        _manager.OnGameStart += ShowGameStart;
        _manager.OnGamePause += ShowGamePause;

        wonPanel.alpha = 0;
        pausePanel.alpha = 0;
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

    private void ShowGamePause()
    {
        _isPaused = !_isPaused;
        
        if (pausePanel.TryGetComponent<Animator>(out var panelAnimator))
        {
            panelAnimator.Play( _isPaused ?  "PauseIn" : "PauseOut" );
        }
    }

    private void OnDestroy()
    {
        _manager.OnGameWon -= ShowWonPanel;
        _manager.OnGameEnd -= ShowGameOverPanel;
        _manager.OnGameStart -= ShowGameStart;
        _manager.OnGamePause -= ShowGamePause;
    }
}
