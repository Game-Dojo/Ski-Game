using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup wonPanel;
    [SerializeField] private CanvasGroup pausePanel;
    [SerializeField] private GameObject menuPanel;
    
    [SerializeField] private Toggle audioToggle;
    
    private GameManager _manager;
    private SaveManager _saveManager;
    
    private bool _isPaused = false;
    
    private void Start()
    {
        _manager = FindAnyObjectByType<GameManager>();
        _saveManager = _manager.GetSaveManager();

        bool isAudioEnable = _saveManager.LoadAudioSetting();
        audioToggle.isOn = isAudioEnable;
        
        _manager.OnGameWon += ShowWonPanel;
        _manager.OnGameEnd += ShowGameOverPanel;
        _manager.OnGameStart += ShowGameStart;
        _manager.OnGamePause += ShowGamePause;

        audioToggle.onValueChanged.AddListener(UpdateAudioState);

        wonPanel.alpha = 0;
        pausePanel.alpha = 0;
    }

    private void UpdateAudioState(bool value)
    {
        print("Update to: " + value);
        _saveManager.SaveAudioSetting(value);
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
