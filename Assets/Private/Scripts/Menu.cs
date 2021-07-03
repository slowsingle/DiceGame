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

    bool nowWaitingDiceRoll = false;

    private void Start()
    {
        diceRollButton.onClick.AddListener(RunDiceRoll);
    }

    private void Update()
    {
        if(nowWaitingDiceRoll && diceController.isRollingEnd)
        {
            List<int> values = diceController.GetValues();
            
            string st = "サイコロの出目は";
            int sum_val = 0;
            foreach (int val in values)
            {
                st += " " + ((val == 0) ? "?" : val.ToString());
                sum_val += val;
            }
            st += " で、合計は " + sum_val +"です";
            textController.AddMessage(st);
            textController.AddMessage("合計値がちょうどぴったりになるようにカードを好きなだけめくってください");

            nowWaitingDiceRoll = false;
            diceRollButton.interactable = true;            
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
