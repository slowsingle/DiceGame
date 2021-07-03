using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayingCardsTable
{
    [SerializeField] private GameObject club01, club02, club03, club04, club05, club06, club07, club08, club09, club10, club11, club12, club13; 
    [SerializeField] private GameObject diamond01, diamond02, diamond03, diamond04, diamond05, diamond06, diamond07, diamond08, diamond09, diamond10, diamond11, diamond12, diamond13; 
    [SerializeField] private GameObject heart01, heart02, heart03, heart04, heart05, heart06, heart07, heart08, heart09, heart10, heart11, heart12, heart13; 
    [SerializeField] private GameObject spade01, spade02, spade03, spade04, spade05, spade06, spade07, spade08, spade09, spade10, spade11, spade12, spade13;

    
    private List<GameObject> cardDeck = null;
    public List<GameObject> getCardDeck()
    {
        if (cardDeck == null) Debug.LogError("not initialized yet!");
        return cardDeck;
    }

    public void Initialize()
    {
        cardDeck = new List<GameObject>() {
            club01, club02, club03, club04, club05, club06, club07, club08, club09, club10, club11, club12, club13,
            diamond01, diamond02, diamond03, diamond04, diamond05, diamond06, diamond07, diamond08, diamond09, diamond10, diamond11, diamond12, diamond13,
            heart01, heart02, heart03, heart04, heart05, heart06, heart07, heart08, heart09, heart10, heart11, heart12, heart13,
            spade01, spade02, spade03, spade04, spade05, spade06, spade07, spade08, spade09, spade10, spade11, spade12, spade13,
        };
    }

    public void Shuffle()
    {
        for(int i = 0; i < cardDeck.Count; i++)
        {
            int k = Random.Range(0, cardDeck.Count);
            GameObject tmp = cardDeck[i];
            cardDeck[i] = cardDeck[k];
            cardDeck[k] = tmp;
        }
    }
    
    public PlayingCardsTable()
    {

    }
}