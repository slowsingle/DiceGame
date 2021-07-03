using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSelector : MonoBehaviour
{
    [SerializeField] List<Toggle> toggles;

    private int selectNumDice = 0;

    public void OnClickToggle()
    {
        // foreach (var t in toggles)
        // {
        //     Debug.Log(t.name + "のisOnは" + t.isOn + "です。");
        // }

        selectNumDice = 0;
        for (int i = 0; i < toggles.Count; i++)
        {
            if (toggles[i].isOn)
            {
                selectNumDice = i + 1;
                break;
            }
        }
    }

    public int getSelectNumDice()
    {
        return selectNumDice;
    }
}