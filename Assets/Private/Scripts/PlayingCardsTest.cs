using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayingCardsTest : MonoBehaviour
{
    [SerializeField] private Transform basePoint;

    [SerializeField] private PlayingCardsTable playingCardsTable;

    private List<GameObject> playingCardInstanceList = new List<GameObject>();

    //private int row = 4, col = 5;  // 行と列


    private void Start()
    {
        playingCardsTable.Initialize();
        playingCardsTable.Shuffle();

        List<GameObject> cardDeck = playingCardsTable.getCardDeck();

        for (int i = 0; i < cardDeck.Count; i++)
        {
            float _offset = ((float) i - (float) cardDeck.Count / 2.0f) * 1.0f;
            Vector3 _point = basePoint.position + new Vector3(_offset, 0f, 0f);
            GameObject inst = (GameObject) GameObject.Instantiate(cardDeck[i], _point, Quaternion.identity);
            inst.GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
            if (inst == null)
            {
                Debug.LogError("cannot create instance");
            }
            playingCardInstanceList.Add(inst);
        }
    }


    private void Update()
    {
        
    }
}
