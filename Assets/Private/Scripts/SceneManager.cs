using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private TextController textController;

    public string cardTag = "PlayingCard";

    // めくったカードを記録しておく
    private List<GameObject> turnedOverCardList = new List<GameObject>();
    private int sum_number = 0;

    private State state;  // ゲームの状態を取得できるようにする

    private void Start()
    {
        state = GameObject.Find("State").GetComponent<State>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray();
            RaycastHit hit = new RaycastHit();
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //マウスクリックした場所からRayを飛ばし、オブジェクトがあればtrue 
            //Rayの原点と方向から、飛ばす方向を定めてオブジェクトの衝突判定を行う
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                GameObject obj = hit.collider.gameObject;
                //Debug.Log(obj.tag + ", " + obj.name);
                if (obj.CompareTag(cardTag))
                {
                    bool isFrontSide = obj.GetComponent<Card>().OnUserAction();

                    string objName = obj.name;
                    string cardName = objName.Split('_')[2];

                    Match markMatch = Regex.Match(cardName, @"[a-z]+", RegexOptions.IgnoreCase);
                    string mark = markMatch.Value;

                    Match numberMatch = Regex.Match(cardName, @"\d+");
                    int number = Int32.Parse(numberMatch.Value); 

                    sum_number += number;
                    textController.AddMessage("めくったカードのマークは " + mark + " で数字は " + number + " です（合計値は" + sum_number + "）");

                    turnedOverCardList.Add(obj);
                    
                    //Debug.Log("name is " + mark + " : " + number + ", isFrontSide is " + isFrontSide);
                    state.ChangeState(State.StateID.TurnOverCards);
                }
            }
        }   
    }

    public int getSumTurnedOverCardNumber()
    {
        return sum_number;
    }
}
