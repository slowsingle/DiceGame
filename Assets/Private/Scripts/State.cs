using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class State : MonoBehaviour
{
    public enum StateID
    {
        DiceRoll,
        TurnOverCards,
    }

    // ステート本体
    private StateID _stateID;

    private void Start()
    {
        _stateID = StateID.DiceRoll;
    }

    public void Initialize()
    {
        _stateID = StateID.DiceRoll;
    }

    public void ChangeState(StateID nextStateID)
    {
        _stateID = nextStateID;
    }

    public StateID GetState()
    {
        return _stateID;
    }
}