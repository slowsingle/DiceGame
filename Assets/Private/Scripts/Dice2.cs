using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice2 : Dice
{
    public static List<int> GetValues()
    {
        List<int> values = new List<int>();
        for (int d = 0; d < allDice.Count; d++)
        {
            RollingDie rDie = (RollingDie)allDice[d];
            values.Add(rDie.die.value);
        }

        return values;
    }
}
