using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string _livesKey = "playerLives";
    private string _scoreKey = "playerScore";
    
    private string _audioKey = "playerAudio";

    private string _listKey = "playerItem_";
    private int _listCurrentIndex = 0;
    
    public void DefaultSettings()
    {
        if (!PlayerPrefs.HasKey(_audioKey))
            SaveAudioSetting(true);
        
        if (!PlayerPrefs.HasKey(_scoreKey))
            SaveScore();
    }

    public void SaveListItem(string itemName)
    {
        if (!PlayerPrefs.HasKey(_listKey + (_listCurrentIndex + 1))) //playerItem_1
        {
            PlayerPrefs.SetString(_listKey + (_listCurrentIndex + 1), itemName);
            _listCurrentIndex += 1;
        }
    }

    public List<string> LoadList()
    {
        List<string> items = new List<string>();

        for (int i = 0; i < _listCurrentIndex; i++)
        {
            string itemName = PlayerPrefs.GetString(_listKey + (_listCurrentIndex + 1));
            items.Add(itemName);
        }

        return items;
    }

    public void SaveScore(int score = 0)
    {
        PlayerPrefs.SetInt(_scoreKey, score);
    }
    public int LoadScore()
    {
        return PlayerPrefs.GetInt(_scoreKey, 0);
    }
    
    public void SaveAudioSetting(bool value)
    {
        print("Saving Audio Settings - " + value);
        PlayerPrefs.SetInt(_audioKey, value ? 1 : 0);
    }
    public bool LoadAudioSetting()
    {
        return PlayerPrefs.GetInt(_audioKey) == 1;
    }
}
