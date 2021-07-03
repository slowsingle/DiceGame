using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayingCardsTest : MonoBehaviour
{
    [SerializeField] private Transform basePoint;

    [SerializeField] private PlayingCardsTable playingCardsTable;

    private List<GameObject> playingCardInstanceList = new List<GameObject>();

    private int row = 4, col = 5;  // 行と列


    private void Start()
    {
        playingCardsTable.Initialize();
        playingCardsTable.Shuffle();

        List<GameObject> cardDeck = playingCardsTable.getCardDeck();

        for (int r = 0; r < row; r++)
        {
            float _offset_z = ((float) r - (float) row / 2.0f) * 1.0f;
            for (int c = 0; c < col; c++)
            {
                float _offset_x = ((float) c - (float) col / 2.0f) * 1.0f;
                Vector3 _point = basePoint.position + new Vector3(_offset_x, 0f, _offset_z);
                GameObject inst = (GameObject) GameObject.Instantiate(cardDeck[r * col + c], _point, Quaternion.identity);
                inst.GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
                if (inst == null)
                {
                    Debug.LogError("cannot create instance");
                }
                playingCardInstanceList.Add(inst);
            }
        }
    }


    private void Update()
    {
        
    }
}
