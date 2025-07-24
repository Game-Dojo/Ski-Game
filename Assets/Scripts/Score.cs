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
        string score = _gameManager.Score.ToString();
        int scoreLength = score.Length;

        if (scoreLength == 1)
        {
            GameObject lastDigit = transform.GetChild(_numCharacters - 1).gameObject;
            if (lastDigit)
                SetDigitValue(lastDigit, int.Parse(score));
            return;
        }
        
        var diff = _numCharacters - scoreLength;
        for (var i = _numCharacters - 1; i >= 0; i--)
        {
            if (i < diff) continue;
            GameObject obj = transform.GetChild(i).gameObject;
            
            // Char to int
            // (int) '0' -> 0 -> HEX -> 30
            if (obj) SetDigitValue(obj, score[i - scoreLength] - '0');
        }
    }

    private void SetDigitValue(GameObject obj, int value)
    {
        if (obj.TryGetComponent<ScoreNumber>(out ScoreNumber scoreNumber))
            scoreNumber.SetValue(value);
    }
}
