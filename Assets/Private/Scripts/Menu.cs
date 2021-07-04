﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button diceRollButton;
    [SerializeField] private DiceController diceController;
    [SerializeField] private TextController textController;
    [SerializeField] private ToggleSelector toggleSelector;
    [SerializeField] private SceneManager sceneManager;
    [SerializeField] private Text sumTotalTurnedOverCardsText;

    private bool nowWaitingDiceRoll = false;
    private int currentSumDiceNumber = 0;
    private State state;  // ゲームの状態を取得できるようにする

    private void Start()
    {
        state = GameObject.Find("State").GetComponent<State>();
        diceRollButton.onClick.AddListener(RunDiceRoll);

        SceneManager.showMessage += ShowCardsAndDiceMessage;
    }

    private void Update()
    {
        State.StateID currentState = state.GetState();

        if (currentState == State.StateID.DiceRoll)
        {
            // サイコロを振り終わるのを待つ状態で、かつ振り終わりのフラグが立っているとき、出目の合計を計算する
            if(nowWaitingDiceRoll && diceController.isRollingEnd)
            {
                List<int> values = diceController.GetValues();
                
                string st = "サイコロの出目は";
                currentSumDiceNumber = 0;
                foreach (int val in values)
                {
                    st += " " + ((val == 0) ? "?" : val.ToString());
                    currentSumDiceNumber += val;
                }
                st += " で、合計は " + currentSumDiceNumber +"です";
                textController.AddMessage(st);
                textController.AddMessage("合計値がちょうどぴったりになるようにカードを好きなだけめくってください");
                nowWaitingDiceRoll = false;  // ダイスが振り終わるのを待つ状態でなくなったのでfalseにする

                // 最後に状態遷移をする
                state.ChangeState(State.StateID.TurnOverCards);
            }
        }
        else if (currentState == State.StateID.TurnOverCards)
        {
            // めくったトランプの合計値と比較する
            int currentSumCardNumber = sceneManager.GetSumTurnedOverCardNumber();
            if (currentSumCardNumber >= currentSumDiceNumber)
            {
                if (currentSumCardNumber == currentSumDiceNumber)
                {
                    textController.AddMessage("ぴったり！");
                    StartCoroutine(DelayMethod(1.5f, () =>
                    {
                        sceneManager.ClearPlayingCards();
                    }));
                }
                else
                {
                    textController.AddMessage("超えてしまいました！");
                    StartCoroutine(DelayMethod(1.5f, () =>
                    {
                        sceneManager.ResetPlayingCards();
                    }));
                }

                // 最後に状態遷移をする
                state.ChangeState(State.StateID.DiceRoll);
                diceRollButton.interactable = true;
            }
            else if (sceneManager.IsCannotTurnOver())
            {
                // 今のところここに入るときはめくれるカードが1枚もないかつカードの合計値が出目より低い状態の場合のみ
                textController.AddMessage("足りない！");
                StartCoroutine(DelayMethod(1.5f, () =>
                {
                    sceneManager.ResetPlayingCards();
                }));

                // 最後に状態遷移をする
                state.ChangeState(State.StateID.DiceRoll);
                diceRollButton.interactable = true;
            }
        }

        sumTotalTurnedOverCardsText.text = "" + sceneManager.GetSumTotalNumTurnedOver();

    }

    private void RunDiceRoll()
    {
        if (state.GetState() != State.StateID.DiceRoll) return;

        int nDice = toggleSelector.getSelectNumDice();
        if (nDice == 0)
        {
            textController.AddMessage("振るサイコロの数を決めてください");
            return;
        }

        diceRollButton.interactable = false;
        diceController.DiceRoll(nDice);
        nowWaitingDiceRoll = true;
    }

    private IEnumerator DelayMethod(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }

    private void ShowCardsAndDiceMessage(string mark, int number, int sumTurnedOver)
    {
        textController.AddMessage("めくったカードのマークは " + mark + " で数字は " + number + " です（カードの合計は" + sumTurnedOver + "で、ダイスの合計は" + currentSumDiceNumber +"）");
    }

}
