using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoRaton : MonoBehaviour
{
    [Header("Parámetros gato")]
    public float _aceleracion;
    public float _tiempoObjetivo;

    public float _velocidadAnimacion;

    public float _tiempoMovimiento;

    public bool Reiniciar;

    private float _temporizador;
    private float _temporizadorCorutina;

    private KeyCode _ultimaTecla;
    private string _estado;


    private float _ObjetivoY;
    private Vector3 _OriginalEscala;
    private float _OriginalY;
    private Vector3 _ObjetivoEscala;
    private float _ObjetivoX;
    private float _OriginalX;
    private IEnumerator _verticalidad;

    private float _YNormal;
    private float _YTrans;
    private float _XTrans;

    // Start is called before the first frame update
    void Start()
    {
        _temporizador = 0;
        _estado = "nada";

        _ultimaTecla = KeyCode.F1;

        _YNormal = transform.position.y;
        _YTrans = 20f;

        _XTrans = 5f;

    }

    // Update is called once per frame
    void Update()
    {
        _temporizador += Time.deltaTime;
        Inputs();
    }

    private void Inputs()
    {
        if (_ultimaTecla == KeyCode.F1)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (_verticalidad != null)
                    StopCoroutine(_verticalidad);
                _estado = "saltar";
                _verticalidad = SaltarCorutina();
                StartCoroutine(_verticalidad);
                _ultimaTecla = KeyCode.UpArrow;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (transform.position.x > -3)
                    StartCoroutine(HorizontalCorutina(-1));
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (transform.position.x < 3)
                    StartCoroutine(HorizontalCorutina(1));
            }
        }
        if (_ultimaTecla == KeyCode.DownArrow)
        {
            if (Input.GetKeyUp(_ultimaTecla))
            {
                if (_verticalidad != null)
                    StopCoroutine(_verticalidad);
                _verticalidad = ReiniciarYCorutina();
                StartCoroutine(_verticalidad);
                _ultimaTecla = KeyCode.F1;
                _estado = "correr";
            }
        }


    }

    IEnumerator SaltarCorutina()
    {
        _temporizadorCorutina = 0f;
        float EscalaTiempo = 5f;
        while (_temporizadorCorutina * Mathf.Rad2Deg < 180)
        {

            _temporizadorCorutina += Time.deltaTime * EscalaTiempo;
            transform.position += _YTrans * 2f * Mathf.Cos(_temporizadorCorutina) * Vector3.up * Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, _YNormal, transform.position.z);
        _ultimaTecla = KeyCode.F1;
        _estado = "correr";
    }

    IEnumerator SuperSaltarCorutina()
    {
        _temporizadorCorutina = 0f;
        float EscalaTiempo = 3f;
        while (_temporizadorCorutina * Mathf.Rad2Deg < 140)
        {

            _temporizadorCorutina += Time.deltaTime * EscalaTiempo;
            if (_temporizadorCorutina * Mathf.Rad2Deg < 90)
            {
                transform.position += _YTrans * 20f * Mathf.Cos(_temporizadorCorutina) * Vector3.up * Time.deltaTime;
            }
            else
            {
                transform.position += _YTrans * 20f * Mathf.Cos(_temporizadorCorutina * 3f + (Mathf.PI * 9f / 3f)) * Vector3.up * Time.deltaTime;
            }
            yield return null;
        }
        RaycastHit hit;
        Physics.BoxCast(transform.position - Vector3.down * 4f, new Vector3(0.5f, 10f, 3f), Vector3.down, out hit);
        hit.collider.gameObject.GetComponent<Tablon>()?.Recibir(20f, 'a');
        transform.position = new Vector3(transform.position.x, _YNormal, transform.position.z);
        _ultimaTecla = KeyCode.F1;
        _estado = "correr";
    }

    IEnumerator AgacharCorutina()
    {
        _temporizadorCorutina = 0f;
        _OriginalY = transform.position.y;
        _ObjetivoY = _OriginalY - _YTrans;
        _OriginalEscala = transform.localScale;
        float EscalaTiempo = 1 / _tiempoMovimiento;
        while (_temporizadorCorutina < 1f)
        {
            _temporizadorCorutina += Time.deltaTime * EscalaTiempo;
            transform.localScale = Vector3.Slerp(_OriginalEscala, new Vector3(2, 0.5f, 0), _temporizadorCorutina);
            transform.position = new Vector3(transform.position.x, Mathf.SmoothStep(_OriginalY, _ObjetivoY, _temporizadorCorutina), transform.position.z);
            yield return null;
        }
    }

    IEnumerator HorizontalCorutina(int dir)
    {
        _temporizadorCorutina = 0f;
        _OriginalX = transform.position.x;
        _ObjetivoX = _OriginalX + (_XTrans * dir);
        _ObjetivoX = Mathf.Round(_ObjetivoX / 5f) * 5f;
        float EscalaTiempo = 1 / _tiempoMovimiento;
        while (_temporizadorCorutina < 1f)
        {
            _temporizadorCorutina += Time.deltaTime * EscalaTiempo;
            transform.position = new Vector3(Mathf.SmoothStep(_OriginalX, _ObjetivoX, _temporizadorCorutina), transform.position.y, transform.position.z);
            yield return null;
        }
        _estado = "correr";
    }

    IEnumerator ReiniciarYCorutina()
    {
        _temporizadorCorutina = 0f;
        _OriginalY = transform.position.y;
        _ObjetivoY = _YNormal;
        float EscalaTiempo = 5f;
        while (_temporizadorCorutina < 1f)
        {
            _temporizadorCorutina += Time.deltaTime * EscalaTiempo;
            transform.localScale = Vector3.Slerp(transform.localScale, Vector3.one, _temporizadorCorutina);
            transform.position = new Vector3(transform.position.x, Mathf.SmoothStep(_OriginalY, _ObjetivoY, _temporizadorCorutina), transform.position.z);
            yield return null;
        }
    }
}