using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private TMP_Text _totalScore;
    [SerializeField] private TMP_Text _numberAttempts;    
    [SerializeField] private TMP_Text _currentLevel;
    [SerializeField] private TMP_Text _restMinutes;
    [SerializeField] private TMP_Text _currency;
    [SerializeField] private LevelGenerator _levelGeneration;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private AttemptsCounter _attemptsCounter; 
    [SerializeField] private GameObject _finishPanel;
    [SerializeField] private Session _session;

    private void Start()
    {
        UpdateScoreText();
        UpdateNumberAttempts();
        UpdateCurrentLevel();
        if (_session.RestMinutes > 0)
        {
            ShowFinishPanel();
        }
    }

    public void ShowFinishPanel()
    {
        _currency.text = Convert.ToString(_session.Currency);
        _restMinutes.text = Convert.ToString(_session.RestMinutes);
        _finishPanel.SetActive(true);        
    }

    public void UpdateScoreText()
    {
        _totalScore.text = Convert.ToString(_scoreCounter.TotalScore);
    }

    public void UpdateNumberAttempts()
    {
        _numberAttempts.text = Convert.ToString(_attemptsCounter.CurrentAttempts);
    }

    public void UpdateCurrentLevel()
    {
        _currentLevel.text = Convert.ToString(_levelGeneration.CurrentLevel);
    }
}
