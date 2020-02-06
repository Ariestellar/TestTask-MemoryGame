using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private int _totalScore;
    public int TotalScore => _totalScore;
    
    public void AddPoints()
    {
        _totalScore += 1;
    }
} 