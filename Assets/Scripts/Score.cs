using UnityEngine;

public class Score : MonoBehaviour
{
    private int _numCharacters = 0;
    
    private GameManager _gameManager;
    
    private void Start()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
        _numCharacters = transform.childCount;
    }

    public void UpdateScore()
    {
        //string score = _gameManager.Score.ToString();
        int score = _gameManager.Score;
        //Debug.Log("1000: " + score % 1000 );
        //Debug.Log("100: " + score % 100 );
        Debug.Log("10: " + score % 10 );
    }
}
