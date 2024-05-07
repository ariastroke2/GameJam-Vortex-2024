using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movobj : MonoBehaviour
{
    float velocidad;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 30f);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObjectDestroyer") || other.CompareTag("Player"))
            Destroy(gameObject);
    }
}
