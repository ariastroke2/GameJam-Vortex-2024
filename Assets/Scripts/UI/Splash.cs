using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    public Transform[] objetos;

    private float _timer;
    void Start()
    {
        _timer = 0f;
    }

    void Update()
    {
        _timer += Time.deltaTime;
        foreach (Transform one in objetos)
        {
            float Sine = Mathf.Sin(_timer * 10f);
            one.localScale += new Vector3(Sine / 3f, Sine / 3f) * 0.10f;
        }
    }
}
