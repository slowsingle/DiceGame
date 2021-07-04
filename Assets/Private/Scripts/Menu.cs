using System;
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

    private bool nowWaitingDiceRoll = false;
    private int currentSumDiceNumber = 0;
    private State state;  // ゲームの状態を取得できるようにする

    private void Start()
    {
        state = GameObject.Find("State").GetComponent<State>();
        diceRollButton.onClick.AddListener(RunDiceRoll);
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
            int currentSumCardNumber = sceneManager.getSumTurnedOverCardNumber();
            if (currentSumCardNumber >= currentSumDiceNumber)
            {
                if (currentSumCardNumber == currentSumDiceNumber)
                {
                    textController.AddMessage("ぴったり！");
                    StartCoroutine(DelayMethod(1.0f, () =>
                    {
                        sceneManager.ClearPlayingCards();
                    }));
                }
                else
                {
                    textController.AddMessage("超えてしまいました！");
                    StartCoroutine(DelayMethod(1.0f, () =>
                    {
                        sceneManager.ResetPlayingCards();
                    }));
                }

                // 最後に状態遷移をする
                state.ChangeState(State.StateID.DiceRoll);
                diceRollButton.interactable = true;
            }
        }

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
}
