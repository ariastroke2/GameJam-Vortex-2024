using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class launch : MonoBehaviour
{
    [Header("Par�metros gato")]
    private float _temporizador;
    public float _gastado;
    public GameObject cleotilde;
    public GameObject piedrota;
    public GameObject dardos;
    public GameObject piedrita;
    void Lanzar (){
        if(_temporizador-_gastado>1f){
            if(_temporizador-_gastado>3f){
               if(_temporizador-_gastado>7f){
                    if(_temporizador-_gastado>15f){
                        _gastado+=15f;
                        GameObject carro=Instantiate(cleotilde,(new Vector3(2,6,-180)),Quaternion.identity);
                    }
                    else{
                        _gastado+=7f;
                        GameObject piedrag=Instantiate(piedrota,transform.position,Quaternion.identity);
                    }
               } 
               else{
                    _gastado+=3f;
                    GameObject dart=Instantiate(dardos,transform.position,Quaternion.identity);
               }
            }
            else{
                _gastado+=1f;
                GameObject cricko=Instantiate(piedrita,transform.position,Quaternion.identity);
            }
        }
        else{
            Debug.Log("PERDEDOR,NO TIENE NADA QUE LANZAR");
        }
    }
    private void Inputs(){
        if (Input.GetKeyDown(KeyCode.RightShift))
            {
                Lanzar();
            }
    }
    void Start(){
        _gastado=0;
    }
}