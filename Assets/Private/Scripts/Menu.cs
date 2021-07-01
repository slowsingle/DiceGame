using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button diceRollButton;
    [SerializeField] private DiceController diceController;

    private void Start()
    {
        diceRollButton.onClick.AddListener(RunDiceRoll);
    }

    private void RunDiceRoll()
    {
        diceController.DiceRoll(6);
    }
}
