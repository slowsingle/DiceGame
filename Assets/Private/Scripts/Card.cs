using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class Card : MonoBehaviour
{
    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    
    private void Update()
    {
        
    }


    public bool OnUserAction()
    {
        // カードをひっくり返す
        _transform.Rotate(new Vector3(0, 0, 1), 180);
        bool isFrontSide = _transform.rotation.eulerAngles.z % 360 == 0;
        return isFrontSide;
    }
}
