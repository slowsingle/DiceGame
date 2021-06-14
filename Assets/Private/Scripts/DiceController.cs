using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    private Die_d6 _die_d6;
    private Transform _transform;
    private Rigidbody _rigidbody;

    void Start()
    {
        
        _die_d6 = gameObject.GetComponent<Die_d6>();
        _transform = gameObject.GetComponent<Transform>();
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        Debug.Log(_die_d6.value);

        if (Input.GetButton("Jump"))
        {
            // _transform.position = new Vector3(0f, 2f, 1f);
            // _rigidbody.AddForce(new Vector3(0f, 10f, -30f));
            //_rigidbody.AddTorque(new Vector3(-2f, -2f, -2f));
        }
    }
}
