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


    public void OnUserAction()
    {
        _transform.Rotate(new Vector3(0, 0, 1), 180);
    }
}
