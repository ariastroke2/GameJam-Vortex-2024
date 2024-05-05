using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tablon : MonoBehaviour
{
    public Tablon Izquierda;
    public Tablon Derecha;

    public bool Activar;

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

    void Start()
    {
        Tiempo = 0;
        qEmision = false;
        Velocidad = 0f;
        OffsetY = transform.position.y;
    }

    void Update()
    {
        if (Activar)
        {
            Activar = false;
            Recibir(10f, 'a');
        }
        Tiempo += Time.deltaTime;
        Velocidad *= 0.99f;
        transform.position = new Vector3(transform.position.x, - Velocidad * Mathf.Sin(Tiempo * Multiplicador) + OffsetY, transform.position.z);

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
    }
}