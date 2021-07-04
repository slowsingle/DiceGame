using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private PlayingCardsController playingCardsController;
    [SerializeField] private AudioSource turnOverCardSound;

    public delegate void ShowMessageHandler(string mark, int number, int sumTurnedOver);
    public static event ShowMessageHandler showMessage;

    public string cardTag = "PlayingCard";

    // めくったカードを記録しておく
    private List<GameObject> turnedOverCardList = new List<GameObject>();
    private int sumTurnedOver = 0;
    private int currentNumTurnedOver = 0, sumTotalNumTurnedOver = 0;

    private State state;  // ゲームの状態を取得できるようにする

    private void Start()
    {
        state = GameObject.Find("State").GetComponent<State>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && state.GetState() == State.StateID.TurnOverCards)
        {
            Ray ray = new Ray();
            RaycastHit hit = new RaycastHit();
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //マウスクリックした場所からRayを飛ばし、オブジェクトがあればtrue 
            //Rayの原点と方向から、飛ばす方向を定めてオブジェクトの衝突判定を行う
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                GameObject obj = hit.collider.gameObject;
                if (obj.CompareTag(cardTag))
                {
                    bool isFrontSide = obj.GetComponent<Card>().OnUserAction();

                    if (turnOverCardSound != null) turnOverCardSound.Play();

                    string objName = obj.name;
                    string cardName = objName.Split('_')[2];

                    Match markMatch = Regex.Match(cardName, @"[a-z]+", RegexOptions.IgnoreCase);
                    string mark = markMatch.Value;

                    Match numberMatch = Regex.Match(cardName, @"\d+");
                    int number = Int32.Parse(numberMatch.Value); 

                    sumTurnedOver += number;
                    if (showMessage != null) showMessage(mark, number, sumTurnedOver);

                    currentNumTurnedOver += 1;
                    sumTotalNumTurnedOver += 1;
                    turnedOverCardList.Add(obj);
                }
            }
        }   
    }

    // 現在のフェーズでめくったカードの数値の合計を返す
    public int GetSumTurnedOverCardNumber()
    {
        return sumTurnedOver;
    }

    // 現在のフェーズでカードをめくった回数（＝めくったカードの枚数）を返す
    public int GetCurrentNumTurnedOver()
    {
        return currentNumTurnedOver;
    }

    // ゲームスタート時からカードをめくった回数（＝めくったカードののべ回数）を返す
    public int GetSumTotalNumTurnedOver()
    {
        return sumTotalNumTurnedOver;
    }


    // めくったカードを元に戻す
    public void ResetPlayingCards()
    {
        foreach (GameObject obj in turnedOverCardList)
        {
            obj.GetComponent<Transform>().Rotate(new Vector3(0, 0, 1.0f), 180);
        }

        // 再初期化
        turnedOverCardList = new List<GameObject>();
        sumTurnedOver = 0;
        currentNumTurnedOver = 0;
    }

    // めくったカードを消す
    public void ClearPlayingCards()
    {
        foreach (GameObject obj in turnedOverCardList)
        {
            playingCardsController.DestroyCard(obj);
        }

        // 再初期化
        turnedOverCardList = new List<GameObject>();
        sumTurnedOver = 0;
        currentNumTurnedOver = 0;
    }

    // これ以上カードがめくれないかどうかを調べる
    public bool IsCannotTurnOver()
    {
        if (currentNumTurnedOver == playingCardsController.GetNumLeftPlayingCards())
        {
            // 場に残されているカードをすべてめくっている状態なので、これ以上はカードをめくることができない
            return true;
        }

        return false;
    }
}
