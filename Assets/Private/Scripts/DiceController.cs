using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    [SerializeField] private Vector3 spawnPointOffset;
    [SerializeField] private Vector3 spawnPointRandomVal;
    [SerializeField] private float forceVal;

    private GameObject spawnPoint;
    private int nCurrentDice;

    // ダイスが完全に振り終わったか？
    public bool isRollingEnd = false;


    private void Start()
    {
        spawnPoint = GameObject.Find("spawnPoint");
    }

    // Update is called once per frame
    private void Update()
    {
        if (Dice2.Count("")>0)
        {
            List<int> values = Dice2.GetValues();
            string st = "";
            foreach (int val in values)
            {
                st += " | " + val;
            }
            Debug.Log(st);

            if (isRollingEnd)
            {
                int sum_val = 0;
                foreach (int val in values)
                {
                    sum_val += val;
                }
                Debug.Log("sum is " + sum_val);
            }
        }
    }

    public List<int> GetValues()
    {
        return Dice2.GetValues();
    }

    public void DiceRoll(int nDice)
    {
        nCurrentDice = nDice;
        isRollingEnd = false;
        Dice2.Clear();
        StartCoroutine("UpdateRoll", nCurrentDice);
        StartCoroutine("WaitRollingDice", .05f * (nCurrentDice) + 3.0f);
    }

    private Vector3 Force()
    {
        Vector3 rollTarget = spawnPointOffset + new Vector3(spawnPointRandomVal.x * Random.value, spawnPointRandomVal.y * Random.value, spawnPointRandomVal.z * Random.value);
        return rollTarget.normalized * forceVal; //* (-35 - Random.value * 20);
    }

    private IEnumerator UpdateRoll(int nDice) 
    {
        for (int i = 0; i < nDice; i++)
        {
            Dice2.Roll("1d6", "d6-" + randomColor + "-dots", spawnPoint.transform.position, Force());
            yield return new WaitForSeconds(.05f);
        }
    }

    private IEnumerator WaitRollingDice(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isRollingEnd = true;
    }

	string randomColor
	{
		get
		{
			string _color = "blue";
			int c = System.Convert.ToInt32(Random.value * 6);
			switch(c)
			{
				case 0: _color = "red"; break;
				case 1: _color = "green"; break;
				case 2: _color = "blue"; break;
				case 3: _color = "yellow"; break;
				case 4: _color = "white"; break;
				case 5: _color = "black"; break;				
			}
			return _color;
		}
	}
}
