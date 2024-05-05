using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityScaler : MonoBehaviour
{
    public MovimientoGato GatoTarget;
    public MovimientoRaton RatonTarget;
    private AudioSource _audioSource;

    public AudioClip IdleSong;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>(); 
    }

    void Update()
    {
        if (GatoTarget == null || RatonTarget == null)
        {
            _audioSource.Stop();
            _audioSource.clip = IdleSong;
            _audioSource.loop = true;
            _audioSource.pitch = 1;
            _audioSource.Play();
            Destroy(this);
        }
        else
            _audioSource.pitch = (GatoTarget._velocidad * 1f) + 0.25f;
    }
}
