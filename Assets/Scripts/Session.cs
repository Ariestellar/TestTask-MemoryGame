using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Session : MonoBehaviour
{
    [SerializeField] private UI _ui;
    [SerializeField] private int _restMinutes;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private LevelGenerator _levelGenerator;

    [SerializeField] private int _currency;

    public int Currency => _currency;   

    public int RestMinutes => _restMinutes;

    private void Awake()
    {
        Begin();
    }

    public void Lose()
    {
        _currency = _scoreCounter.TotalScore * 15 * 1;
        _restMinutes = 5;
        _levelGenerator.DeleteGridCards();
        _ui.ShowFinishPanel();        
    }

    public void Complete()
    {
        PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
        PlayerPrefs.SetInt("RestMinutes", _restMinutes);
        PlayerPrefs.SetInt("Currency", _currency);
        Application.Quit();
    }

    public void Begin()
    {
        if (PlayerPrefs.HasKey("LastSession"))
        {
            TimeSpan WaitingTime = DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("LastSession"));
            _restMinutes = PlayerPrefs.GetInt("RestMinutes");
            _restMinutes -= WaitingTime.Minutes;
        }
        else 
        {
            _restMinutes = 0;
        }

        if (PlayerPrefs.HasKey("Currency"))
        {
            _currency = PlayerPrefs.GetInt("Currency");
        }
        else
        {
            _currency = 0;
        }
    }
}
