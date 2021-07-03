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
        if (currentState == State.StateID.DiceRoll){
            Debug.Log("OK!");
        }
        Debug.Log(state.GetState());
        
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

            nowWaitingDiceRoll = false;
            diceRollButton.interactable = true;            
        }

        // めくったトランプの合計値と比較する
        if (true)
        {
            int currentSumCardNumber = sceneManager.getSumTurnedOverCardNumber();
            if (currentSumCardNumber == currentSumDiceNumber)
            {
                textController.AddMessage("ぴったり！");
            }
            else if (currentSumCardNumber > currentSumDiceNumber)
            {
                textController.AddMessage("超えてしまいました！");
            }
        }

    }

    private void RunDiceRoll()
    {
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
}
