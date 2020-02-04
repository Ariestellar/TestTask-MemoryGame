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

    private int[] _cardDeck;
    private MemoryCard[] _allCardsField;
    private int _numberGridRows;
    private int _numberGridCols;    

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

    private void Start()
    {
        if (_session.RestMinutes <= 0)
        {
            SetSizeGridDeck(_numberPairs * 2);
            _cardDeck = GetDeckCards(_numberPairs * 2);
            CreateGridCards(_cardDeck, _numberGridRows, _numberGridCols, _columnSpacing, _lineSpacing);
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

    private void CreateGridCards(int [] cardDeck, int numberLines, int numberColumns, float columnSpacing, float lineSpacing)
    {     
        for (int i = 0; i < numberLines; i++)
        {            
            for (int j = 0; j < numberColumns; j++)
            {                    
                float positionCardsX = columnSpacing * i;
                float positionCardsY = lineSpacing * j;
                Vector3 positionCard = new Vector3(positionCardsX, positionCardsY, 0);

                int index = j * numberLines + i;
                if (index < cardDeck.Length) 
                {
                    int idCard = cardDeck[index];
                    CreateCard(positionCard, idCard);
                }                                     
            }                       
        }        
    }   

    private void CreateCard(Vector3 positionCard, int idCard)
    {
        GameObject newCard = Instantiate(_templateCard, _cardField.transform);
        newCard.transform.position += positionCard; 
        newCard.GetComponent<MemoryCard>().SetFaceCard(idCard);        
    }

    public void AddCardDeck()
    {
        _numberPairs += 1;
        DeleteGridCards();        
        SetSizeGridDeck(_numberPairs * 2);
        _cardDeck = GetDeckCards(_numberPairs * 2);
        CreateGridCards(_cardDeck, _numberGridCols, _numberGridRows, _columnSpacing, _lineSpacing);
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

    private void SetSizeGridDeck(int totalNumberCards)
    {        
        totalNumberCards = Mathf.Clamp(totalNumberCards, 16, 36);
        if (totalNumberCards == 16)
        {
            _numberGridRows = 4;
            _numberGridCols = 4;
        }
        else if (totalNumberCards == 18)
        {
            _numberGridRows = 4;
            _numberGridCols = 5;
        }
        else if (totalNumberCards == 20)
        {
            _numberGridRows = 5;
            _numberGridCols = 5;
        }
        else if (totalNumberCards > 20 && totalNumberCards < 31)
        {
            _numberGridRows = 5;
            _numberGridCols = 6;
        }
        else if (totalNumberCards > 30 && totalNumberCards < 36)
        {
            _numberGridRows = 5;
            _numberGridCols = 7;
        }
        else if (totalNumberCards == 36)
        {
            _numberGridRows = 6;
            _numberGridCols = 6;
        }
    }
}