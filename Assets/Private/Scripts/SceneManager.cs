using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public string cardTag = "PlayingCard";

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray();
            RaycastHit hit = new RaycastHit();
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //マウスクリックした場所からRayを飛ばし、オブジェクトがあればtrue 
            //Rayの原点と方向から、飛ばす方向を定めてオブジェクトの衝突判定を行う
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                //Debug.Log(hit.collider.gameObject.tag);
                //Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.CompareTag(cardTag))
                {
                    bool isFrontSide = hit.collider.gameObject.GetComponent<Card>().OnUserAction();

                    string objName = hit.collider.gameObject.name;
                    string cardName = objName.Split('_')[2];

                    Match markMatch = Regex.Match(cardName, @"[a-z]+", RegexOptions.IgnoreCase);
                    string mark = markMatch.Value;

                    Match numberMatch = Regex.Match(cardName, @"\d+");
                    string number = numberMatch.Value; 

                    Debug.Log("name is " + mark + " : " + number + ", isFrontSide is " + isFrontSide);
                }
            }
        }   
    }
}
