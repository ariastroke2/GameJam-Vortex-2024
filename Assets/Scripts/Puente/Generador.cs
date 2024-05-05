using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generador : MonoBehaviour
{
    public GameObject Tablon;

    public float EspacioEntreTablas;
    public float InicioZ;
    public float FinZ;

    public float OffsetY;

    public float Retardo;
    public float Liquidez;

    void Start()
    {
        GameObject TablaIzquierda = null;
        for(float tab = InicioZ; tab < FinZ; tab += EspacioEntreTablas)
        {
            GameObject Tabla = Instantiate(Tablon, new Vector3(0, OffsetY, tab), Quaternion.identity);
            if(TablaIzquierda != null) {
                Tabla.GetComponent<Tablon>().Izquierda = TablaIzquierda.GetComponent<Tablon>();
                TablaIzquierda.GetComponent<Tablon>().Derecha = Tabla.GetComponent<Tablon>();
            }
            Tabla.GetComponent<Tablon>().Liquidez = Liquidez;
            Tabla.GetComponent<Tablon>().Retardo = Retardo;
            Tabla.name = tab.ToString();
            TablaIzquierda = Tabla;
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
