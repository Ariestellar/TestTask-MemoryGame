using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] private GameObject _backCard;
    [SerializeField] private CardOpener _cardOpener;  
       
    [SerializeField] private int _idCard;
    [SerializeField] public int Id => _idCard;

    private void Start()
    {
        _cardOpener = FindObjectOfType<CardOpener>();
    }

    public void SetFaceCard(int idCard)
    {
        _idCard = idCard;
        GetComponentInChildren<TextMeshPro>().text = Convert.ToString(idCard);

    }

    private void OnMouseDown()
    {
        if (_backCard.activeSelf && !_cardOpener.OnTwoCardsOpen)
        {
            _backCard.SetActive(false);            
            _cardOpener.Open(this);
        }
    }

    public void CloseBack()
    {
        _backCard.SetActive(true);
    }

    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}
