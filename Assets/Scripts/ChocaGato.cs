using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocaGato : MonoBehaviour
{
    public GameObject gatoGordo;
    public GameObject gatoFlaco;
    public bool flaco;
    private void Start(){
        flaco=false;
        gatoGordo.SetActive(true);
        gatoFlaco.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enflacador")){
            Enflacar();
        }
        if(other.CompareTag("Dardos")){

        }
        if(other.CompareTag("Piedrita")){

        }
        if(other.CompareTag("Piedrota")){

        }
        if(other.CompareTag("Cleotilde")){
            
        }
    }
    public void Enflacar(){
        flaco=true;
        gatoGordo.SetActive(false);
        gatoFlaco.SetActive(true);
    }
}