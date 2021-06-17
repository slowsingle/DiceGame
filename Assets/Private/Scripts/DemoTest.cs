using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoTest : MonoBehaviour
{

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
            Debug.Log("GetMouseButtonDown");
            UpdateRoll();
        }
    }

    private Vector3 Force()
    {
        Vector3 rollTarget = Vector3.zero + new Vector3(2 + 6 * Random.value, .5F + 3 * Random.value, -2 - 3 * Random.value);
        return Vector3.Lerp(spawnPoint.transform.position, rollTarget, 1).normalized * (-35 - Random.value * 20);
    }

    private void UpdateRoll()
    {
        // left mouse button clicked so roll random colored dice 2 of each dieType
        Dice.Clear();

        Dice.Roll("1d10", "d10-" + randomColor, spawnPoint.transform.position, Force());
        Dice.Roll("1d10", "d10-" + randomColor, spawnPoint.transform.position, Force());
        Dice.Roll("1d10", "d10-" + randomColor, spawnPoint.transform.position, Force());
        Dice.Roll("1d10", "d10-" + randomColor, spawnPoint.transform.position, Force());
        Dice.Roll("1d6", "d6-" + randomColor, spawnPoint.transform.position, Force());
        Dice.Roll("1d6", "d6-" + randomColor, spawnPoint.transform.position, Force());
        Dice.Roll("1d6", "d6-" + randomColor, spawnPoint.transform.position, Force());
        Dice.Roll("1d6", "d6-" + randomColor, spawnPoint.transform.position, Force());
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
