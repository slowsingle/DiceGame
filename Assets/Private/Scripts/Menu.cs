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
            
            string st = "each number of dice is";
            int sum_val = 0;
            foreach (int val in values)
            {
                st += " " + ((val == 0) ? "?" : val.ToString());
                sum_val += val;
            }
            st += " => " + sum_val;
            //Debug.Log(st);
            nowWaitingDiceRoll = false;
            diceRollButton.interactable = true;

            textController.AddMessage(st);
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
