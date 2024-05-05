using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observador : MonoBehaviour
{

    public Transform _objetivo;
    public Vector3 _offset;

    void Update()
    {
        if(_objetivo != null)
        {
            Vector3 pos = _objetivo.position + _offset;
            pos.x = _offset.x;
            pos.y = _offset.y;
            transform.position = pos;
        }
    }
}
