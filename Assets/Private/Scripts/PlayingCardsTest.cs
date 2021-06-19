using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingCardsTest : MonoBehaviour
{
    [SerializeField] private List<GameObject> playingCardPrefabList;
    [SerializeField] private Transform basePoint;

    private List<GameObject> playingCardInstanceList = new List<GameObject>();


    void Start()
    {
        for (int i = 0; i < playingCardPrefabList.Count; i++)
        {
            float _offset = ((float) i - (float) playingCardPrefabList.Count / 2.0f) * 1.0f;
            Vector3 _point = basePoint.position + new Vector3(_offset, 0f, 0f);
            GameObject inst = (GameObject) GameObject.Instantiate(playingCardPrefabList[i], _point, Quaternion.identity);
            inst.GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
            if (inst == null)
            {
                Debug.LogError("cannot create instance");
            }
            playingCardInstanceList.Add(inst);
        }
    }


    void Update()
    {
        
    }
}
