using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatoWinTrigger : MonoBehaviour
{
    public MovimientoRaton Raton;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Gato")
        {
            other.gameObject.GetComponent<MovimientoGato>().Ganar();
            Raton.GatoGana();
        }
    }
}
