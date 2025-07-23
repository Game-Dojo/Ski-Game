using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreNumber : MonoBehaviour
{
    [SerializeField] private List<Sprite> scoreNumberImages;
    private Image _imageRef;
    private void Start()
    {
        _imageRef = GetComponent<Image>();
        SetValue(0);
    }

    public void SetValue(int number)
    {
        if (number > 9) return;
        _imageRef.sprite = scoreNumberImages[number];
    }
}
