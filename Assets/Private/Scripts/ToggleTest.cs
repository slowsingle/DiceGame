using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleTest : MonoBehaviour
{
    [SerializeField] List<Toggle> toggles;
    public void OnClickToggle()
    {
        foreach (var t in toggles)
        {
            Debug.Log(t.name + "のisOnは" + t.isOn + "です。");
        }
    }
}