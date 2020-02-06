using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttemptsCounter : MonoBehaviour
{
    [SerializeField] private int _defaultQuantityAttempts;
    [SerializeField] private int _currentAttempts;
    [SerializeField] private Session _endGame;

    public int CurrentAttempts => _currentAttempts;

    private void Start()
    {
        ResetPoints();
    }

    public void AddPoints()
    {
        _currentAttempts += 1;
    }

    public void RemovePoints()
    {
        _currentAttempts -= 1;
        if (_currentAttempts < 0)
        {
            _endGame.Lose();
        }
    }

    public void ResetPoints()
    {
        _currentAttempts = _defaultQuantityAttempts;
    }
}
