using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatonWeapon : MonoBehaviour
{
    public Sprite[] sprites;
    public Image filler;
    public Image icon;
    public MovimientoRaton rata;

    void Update()
    {
        if(rata != null)
        {
            icon.sprite = sprites[rata.Actual + 1];
            filler.fillAmount = rata.Siguiente;
        }
    }
}
