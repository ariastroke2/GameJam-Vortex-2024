using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observador : MonoBehaviour
{

    public Transform _objetivo;
    public Vector3 _offset;

    void Update()
    {
        transform.position = _objetivo.position + _offset;
    }
}
