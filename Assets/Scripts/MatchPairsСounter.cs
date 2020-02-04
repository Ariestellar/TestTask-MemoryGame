using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MatchPairsСounter : MonoBehaviour
{
    [SerializeField] private UnityEvent _openAllPairs;
    [SerializeField] private LevelGenerator _levelGenerator;

    private int _currentNumberOpenPairs;    
   
    public void AddOpenPair()
    {
        _currentNumberOpenPairs += 1;
        if (_levelGenerator.NumberPairs == _currentNumberOpenPairs)
        {
            _openAllPairs.Invoke();
            _currentNumberOpenPairs = 0;
        }
            
    }

    public void TestVictory()
    {
        _openAllPairs.Invoke();
    }
}
