using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayingCardsController : MonoBehaviour
{
    [SerializeField] private Transform basePoint;

    [SerializeField] private PlayingCardsTable playingCardsTable;

    private List<GameObject> playingCardInstanceList = new List<GameObject>();

    private int row, col;  // 行と列  4,7

    private int numberPlayingCards = 0;
    private int numberLeftPlayingCards = 0;


    private void Start()
    {
        playingCardsTable.Initialize();
        playingCardsTable.Shuffle();

        string mode = StartButton.GetSelectedMode();
        if (mode == "Normal") { row = 2; col = 3;}
        else if (mode == "Hard") { row = 4; col = 7;}
        else Debug.LogError("Invalid mode : " + mode);

        List<GameObject> cardDeck = playingCardsTable.getCardDeck();

        // HACK: カードを中央ぞろえに配置するために、rowとcolにそれぞれ1を足している
        for (int r = 0; r < row + 1; r++)
        {
            float _offset_z = ((float) r - (float) row / 2.0f) * 1.2f;
            for (int c = 0; c < col + 1; c++)
            {
                float _offset_x = ((float) c - (float) col / 2.0f) * 1.0f;
                Vector3 _point = basePoint.position + new Vector3(_offset_x, 0f, _offset_z);
                GameObject inst = (GameObject) GameObject.Instantiate(cardDeck[r * (col + 1) + c], _point, Quaternion.identity);
                inst.GetComponent<Transform>().localScale = new Vector3(0.12f, 0.12f, 0.12f);
                inst.GetComponent<Transform>().Rotate(new Vector3(0, 0, 1), 180);
                if (inst == null)
                {
                    Debug.LogError("cannot create instance");
                }
                playingCardInstanceList.Add(inst);
                numberPlayingCards += 1;
            }
        }

        numberLeftPlayingCards = numberPlayingCards;
    }


    public void DestroyCard(GameObject obj)
    {
        Destroy(obj);
        numberLeftPlayingCards -= 1;
    }

    public int GetNumLeftPlayingCards()
    {
        return numberLeftPlayingCards;
    }
}
