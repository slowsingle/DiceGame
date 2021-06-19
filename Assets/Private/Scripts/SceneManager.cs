using System.Collections;
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
                if (hit.collider.gameObject.CompareTag(cardTag))
                {
                    hit.collider.gameObject.GetComponent<Card>().OnUserAction();
                }
            }
        }   
    }
}
