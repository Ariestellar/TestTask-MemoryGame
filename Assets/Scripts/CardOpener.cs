using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardOpener : MonoBehaviour
{
    [SerializeField] private MemoryCard _firstCard;
    [SerializeField] private MemoryCard _secondCard;    
    [SerializeField] private UnityEvent _сoupleFound;
    [SerializeField] private UnityEvent _сoupleNotFound;
    [SerializeField] private bool _onTwoCardsOpen;

    public bool OnTwoCardsOpen => _onTwoCardsOpen;
       
    public void Open(MemoryCard card)
    {
        if (_firstCard == null)
        {
            _firstCard = card;            
        } 
        else if (_secondCard == null)
        {
            _secondCard = card;
            _onTwoCardsOpen = true;
            StartCoroutine(CheckСards());
        }
    }

    private void FlipPairsBack()
    {
        _firstCard.CloseBack();
        _secondCard.CloseBack();
    }

    private IEnumerator CheckСards() 
    {
        if (_firstCard.Id == _secondCard.Id)
        {
            _сoupleFound.Invoke();
        }
        else if (_firstCard.Id != _secondCard.Id)
        {
            yield return new WaitForSeconds(.5f);            
            _сoupleNotFound.Invoke();
            FlipPairsBack();
        }
        _onTwoCardsOpen = false;
        _firstCard = null;
        _secondCard = null;
    }   
}
