using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private int numLine = 4;

    private List<string> textMessage = new List<string>();

    private void Start()
    {
        for (int i = 0; i < numLine; i++)
        {

            textMessage.Add("aaaaaa " + i);
        }
    }

    private void Update()
    {
        string vAll = "";
        foreach(string v in textMessage)
        {
            vAll += v + "\n";
        }
        text.text = vAll; 
    }
}
