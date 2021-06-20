using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoTest : MonoBehaviour
{
    [SerializeField] private Vector3 spawnPointOffset;
    [SerializeField] private Vector3 spawnPointRandomVal;
    [SerializeField] private float forceVal;

    private GameObject spawnPoint = null;

    private void Start()
    {
        spawnPoint = GameObject.Find("spawnPoint");
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(Dice.MOUSE_LEFT_BUTTON))
        {
            Dice.Clear();
            StartCoroutine("UpdateRoll");
        }
    }

    private Vector3 Force()
    {
        Vector3 rollTarget = spawnPointOffset + new Vector3(spawnPointRandomVal.x * Random.value, spawnPointRandomVal.y * Random.value, spawnPointRandomVal.z * Random.value);
        return rollTarget.normalized * forceVal; //* (-35 - Random.value * 20);
    }

    IEnumerator UpdateRoll() 
    {
        for (int i = 0; i < 8; i++)
        {
            Dice.Roll("1d6", "d6-" + randomColor + "-dots", spawnPoint.transform.position, Force());
            yield return new WaitForSeconds(.05f);
        }
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
