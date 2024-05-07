using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tablon : MonoBehaviour
{
    public Tablon Izquierda;
    public Tablon Derecha;

    public bool Activar;

    public AudioClip _shake1;
    public AudioClip _shake2;
    public AudioClip _shake3;

    public float Retardo;
    public float Liquidez;

    public float Multiplicador;

    private float Velocidad;
    private float Tiempo;

    private float qRetardoTimer;
    private float qFuerza;
    private float qDireccion;
    private bool qEmision;

    private float OffsetY;

    private AudioSource _audioSource;

    void Start()
    {
        Tiempo = 0;
        qEmision = false;
        Velocidad = 0f;
        OffsetY = transform.position.y;
        _audioSource = GetComponent<AudioSource>(); 
    }

    void Update()
    {
        if (Activar)
        {
            Activar = false;
            Recibir(10f, 'a');
        }
        Tiempo += Time.deltaTime;
        Velocidad *= 0.935f;
        transform.position = new Vector3(transform.position.x, Velocidad * Mathf.Sin(Tiempo * Multiplicador) + OffsetY, transform.position.z);

        if (qEmision)
        {
            if (qRetardoTimer > 0)
            {
                qRetardoTimer -= Time.deltaTime;
            }
            else
            {
                qEmision = false;
                if (qDireccion == 'r' || qDireccion == 'a')
                {
                    if(Derecha != null)
                        Derecha.Recibir(qFuerza * Liquidez, 'r');
                }
                if (qDireccion == 'l' || qDireccion == 'a')
                {
                    if (Izquierda != null)
                        Izquierda.Recibir(qFuerza * Liquidez, 'l');
                }
            }
        }
    }

    public void Recibir(float Fuerza, char Direccion)
    {
        Velocidad = Fuerza;
        qRetardoTimer = Retardo;
        qFuerza = Fuerza;
        qDireccion = Direccion;
        qEmision = true;
        Tiempo = 0;
        if(Direccion == 'l')
            if(Random.Range(0,2) == 1)
            {
                _audioSource.PlayOneShot(_shake1);
            }
            else
            {
                if (Random.Range(0, 2) == 1)
                {
                    _audioSource.PlayOneShot(_shake2);
                }
                else
                {
                    _audioSource.PlayOneShot(_shake3);
                }
            }
    }
}