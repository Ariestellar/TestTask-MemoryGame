using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{        
    [SerializeField] private int _numberPairs;
    [SerializeField] private float _columnSpacing;
    [SerializeField] private float _lineSpacing;
    [SerializeField] private GameObject _templateCard;
    [SerializeField] private GameObject _cardField;
    [SerializeField] private int _currentLevel;
    [SerializeField] private Session _session;
    [SerializeField] private CameraMovement _camera;  

    [SerializeField] private int _numberGridRows;
    [SerializeField] private int _numberGridCols;

    private int[] _cardDeck;
    private MemoryCard[] _allCardsField;

    public int CurrentLevel => _currentLevel;
    public int NumberPairs => _numberPairs;

    public void AddLevel()
    {
        _currentLevel += 1;
    }

    public void DeleteGridCards()
    {
        _allCardsField = _cardField.GetComponentsInChildren<MemoryCard>();
        foreach (MemoryCard card in _allCardsField)
        {
            card.OnDestroy();
        }        
    }

    public void AddCardDeck()
    {
        _numberPairs += 1;
        DeleteGridCards();
        IncreaseGridSize(_numberPairs * 2);
        _cardDeck = GetDeckCards(_numberPairs * 2);
        CreateGridCards(_cardDeck, _numberGridRows, _numberGridCols);
    }

    private void Start()
    {
        if (_session.RestMinutes <= 0)
        {            
            _cardDeck = GetDeckCards(_numberPairs * 2);
            CreateGridCards(_cardDeck, _numberGridRows, _numberGridCols);
        }            
    }

    private int [] GetDeckCards(int totalNumberCards)
    {
        int [] cardDeck = new int[totalNumberCards];

        for (int i = 0, j = 0; i < cardDeck.Length; i += 2, j++)
        {
            cardDeck[i] = j;
            cardDeck[i + 1] = j;
        }
        
        return ShuffleArray(cardDeck);
    }

    private void CreateGridCards(int [] cardDeck, int numberRows, int numberColumns)
    {        
        for (int i = 0; i < numberColumns; i++)
        {            
            for (int j = 0; j < numberRows; j++)
            {
                int index = j * numberColumns + i;

                if (index < cardDeck.Length) 
                {
                    int idCard = cardDeck[index];
                    float positionCardsX = _columnSpacing * i;
                    float positionCardsY = _lineSpacing * j;
                    Vector3 positionCard = new Vector3(positionCardsX, positionCardsY, 0);
                    CreateCard(positionCard, idCard);                    
                }                                     
            }            
        }        
    }   

    private void CreateCard(Vector3 positionCard, int idCard)
    {
        GameObject newCard = Instantiate(_templateCard, _cardField.transform);
        newCard.transform.position = positionCard; 
        newCard.GetComponent<MemoryCard>().SetFaceCard(idCard);        
    }   

    private int[] ShuffleArray(int[] number)
    {
        int[] newArray = number.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }  

    private void IncreaseGridSize(int totalNumberCards)
    {
        if ((_numberGridRows - _numberGridCols) != 0)
        {
            if (totalNumberCards > (_numberGridRows * _numberGridCols))
            {
                _numberGridRows += 1;
                _camera.ZoomOutCamera(_columnSpacing);  
            }
        }
        else
        {
            if (totalNumberCards > (_numberGridRows * _numberGridCols))
            {                
                _numberGridCols += 1;
                _camera.MoveCamera(_numberGridRows / (_numberGridRows - 1) / _lineSpacing);
            }            
        }
    }
}