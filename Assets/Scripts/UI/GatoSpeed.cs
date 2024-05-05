using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GatoSpeed : MonoBehaviour
{

    public MovimientoGato _target;
    private Image _image;

    void Start()
    {
        _image = GetComponent<Image>();
        Debug.Log(_image.type);
    }

    void Update()
    {
        _image.fillAmount = _target._velocidad;
        if(_target._velocidad > 0.99f)
        {
            Vector3 hsv = Vector3.zero;
            Color.RGBToHSV(_image.color, out hsv.x, out hsv.y, out hsv.z);
            hsv.x += 2f * Time.deltaTime;
            _image.color = Color.HSVToRGB(hsv.x, hsv.y, hsv.z);
        }
        else
        {
            _image.color = Color.red;
        }
    }
}
