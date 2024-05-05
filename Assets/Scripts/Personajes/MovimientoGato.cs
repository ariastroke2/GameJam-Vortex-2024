using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoGato : MonoBehaviour
{
    [Header("Parámetros gato")]
    public float _aceleracion;
    public float _tiempoObjetivo;

    public float _radioBrazos;
    public float _velocidadAnimacion;

    public float _tiempoMovimiento;

    public bool Reiniciar;

    private float _gordura;
    public float _velocidad; // va de 0 a 1
    private float _retrasoVelocidad;

    private float _temporizador;
    private float _temporizadorSeno;
    private float _temporizadorCorutina;
    private float _velocidadHaciaObjetivo;

    private KeyCode _ultimaTecla;
    public bool _puedeAvanzar;
    private string _estado;


    private float _ObjetivoY;
    private Vector3 _OriginalEscala;
    private float _OriginalY;
    private Vector3 _ObjetivoEscala;
    private float _ObjetivoX;
    private float _OriginalX;
    private IEnumerator _verticalidad;

    private Transform _manoDerecha;
    private Transform _manoIzquierda;
    private Transform _cuerpo;
    private Transform _cabeza;

    private float _YNormal;
    private float _YTrans;
    private float _XTrans;

    // Start is called before the first frame update
    void Start()
    {
        _velocidadHaciaObjetivo = 400f / _tiempoObjetivo;  // aproximado de cuanto tiempo va a tardar el gato en llegar al ratón
        _velocidad = 0;
        _retrasoVelocidad = 0;
        _temporizador = 0;
        _puedeAvanzar = true;
        _estado = "correr";

        _manoDerecha = transform.Find("Mano Derecha");
        _manoIzquierda = transform.Find("Mano Izquierda");
        _cuerpo = transform.Find("Cuerpo");
        _cabeza = transform.Find("Cabeza");

        _ultimaTecla = KeyCode.F1;

        _YNormal = transform.position.y;
        _YTrans = 3f;

        _XTrans = 5f;

    }

    // Update is called once per frame
    void Update()
    {
        _temporizador += Time.deltaTime;
        _temporizadorSeno += Time.deltaTime * _velocidad;
        if( _velocidad > 0.95f)
            _temporizadorSeno += Time.deltaTime * _velocidad;
        if (_retrasoVelocidad > 0)
        {
            _retrasoVelocidad -= Time.deltaTime;
        }
        else
        {
            _velocidad += _aceleracion * Time.deltaTime;
            if (_velocidad > 1f)
            {
                _velocidad = 1f;
            }

        }

        float TransformacionZ = _velocidadHaciaObjetivo * _velocidad;
        if (_puedeAvanzar)
        {
            transform.position += new Vector3(0, 0, -TransformacionZ) * Time.deltaTime;
            if(_velocidad > 0.95f)
                transform.position += new Vector3(0, 0, -TransformacionZ) * Time.deltaTime * 0.2f;
        }

        Animacion();
        Inputs();

        if (Reiniciar)
        {
            Reiniciar = false;
            _velocidad -= 0.5f;
        }
    }

    private void Inputs()
    {
        if(_ultimaTecla == KeyCode.F1)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                if(_verticalidad != null)
                    StopCoroutine(_verticalidad);
                _estado = "agachar";
                _puedeAvanzar = false;
                _verticalidad = AgacharCorutina();
                StartCoroutine(_verticalidad);
                _ultimaTecla = KeyCode.S;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (_verticalidad != null)
                    StopCoroutine(_verticalidad);
                _estado = "saltar";
                _verticalidad = SaltarCorutina();
                StartCoroutine(_verticalidad);
                _ultimaTecla = KeyCode.W;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (transform.position.x < 3)
                    StartCoroutine(HorizontalCorutina(1));
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (transform.position.x > -3)
                    StartCoroutine(HorizontalCorutina(-1));
            }
        }
        if(_ultimaTecla == KeyCode.S)
        {
            if (Input.GetKeyUp(_ultimaTecla))
            {
                if (_verticalidad != null)
                    StopCoroutine(_verticalidad);
                _puedeAvanzar = true;
                _verticalidad = ReiniciarYCorutina();
                StartCoroutine(_verticalidad);
                _ultimaTecla = KeyCode.F1;
                _estado = "correr";
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (_verticalidad != null)
                    StopCoroutine(_verticalidad);
                _estado = "supersaltar";
                _verticalidad = SuperSaltarCorutina();
                StartCoroutine(_verticalidad);
                _ultimaTecla = KeyCode.W;
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
            if(_temporizadorCorutina * Mathf.Rad2Deg < 90)
            {
                _manoIzquierda.localPosition = Vector3.Slerp(_manoIzquierda.localPosition, new Vector3(1.5f, -2f, 2f), _temporizadorCorutina / EscalaTiempo);
                _manoDerecha.localPosition = Vector3.Slerp(_manoDerecha.localPosition, new Vector3(-1.5f, 2f, -2f), _temporizadorCorutina / EscalaTiempo);

                transform.localScale = Vector3.Slerp(transform.localScale, new Vector3(2, 0.5f, 1f), _temporizadorCorutina / EscalaTiempo);
            }
            else
            {
                transform.localScale = Vector3.Slerp(transform.localScale, Vector3.one, (_temporizadorCorutina - ((90 * Mathf.Deg2Rad))) / EscalaTiempo);
            }
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
                _manoIzquierda.localPosition = Vector3.Slerp(_manoIzquierda.localPosition, new Vector3(1.5f, -2f, 2f), _temporizadorCorutina / EscalaTiempo);
                _manoDerecha.localPosition = Vector3.Slerp(_manoDerecha.localPosition, new Vector3(-1.5f, 2f, -2f), _temporizadorCorutina / EscalaTiempo);

                transform.localScale = Vector3.Slerp(transform.localScale, new Vector3(2, 0.5f, 1f), _temporizadorCorutina / EscalaTiempo);
                transform.position += _YTrans * 20f * Mathf.Cos(_temporizadorCorutina) * Vector3.up * Time.deltaTime;
            }
            else
            {
                transform.localScale = Vector3.Slerp(transform.localScale, Vector3.one, (_temporizadorCorutina - ((90 * Mathf.Deg2Rad))) / EscalaTiempo);
                transform.position += _YTrans * 20f * Mathf.Cos(_temporizadorCorutina * 3f + (Mathf.PI * 9f / 3f)) * Vector3.up * Time.deltaTime;
            }
            yield return null;
        }
        RaycastHit hit;
        Physics.BoxCast(transform.position - Vector3.down * 4f, new Vector3(0.5f, 10f, 3f), Vector3.down, out hit);
        if(hit.collider != null)
            hit.collider.gameObject.GetComponent<Tablon>()?.Recibir(20f, 'a');
        transform.position = new Vector3(transform.position.x, _YNormal, transform.position.z);
        _ultimaTecla = KeyCode.F1;
        _estado = "correr";
        _puedeAvanzar = true;
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

    private void Animacion()
    {
        float SenoTiempo = Mathf.Sin(_temporizadorSeno * _velocidadAnimacion);
        float CosenoTiempo = Mathf.Cos(_temporizadorSeno * _velocidadAnimacion);

        if (_estado == "correr")
        {
            _manoIzquierda.localPosition = new Vector3(1.5f, Mathf.Abs(SenoTiempo) * -1f * _radioBrazos, CosenoTiempo * _radioBrazos * 2f);
            _manoDerecha.localPosition = new Vector3(-1.5f, Mathf.Abs(SenoTiempo) * -1f * _radioBrazos, -CosenoTiempo * _radioBrazos * 2f);
            _cuerpo.localPosition = new Vector3(0, -2.6f + Mathf.Abs(SenoTiempo) * 1f, 0);
            _cabeza.localPosition = new Vector3(0, Mathf.Abs(SenoTiempo) * 1f, 0);
        }
        if(_estado == "agachar")
        {
            _manoIzquierda.localPosition = Vector3.Lerp(_manoIzquierda.localPosition, new Vector3(1.5f, 0, 0), 0.1f);
            _manoDerecha.localPosition = Vector3.Lerp(_manoDerecha.localPosition, new Vector3(-1.5f, 0, 0), 0.1f);
        }
    }

    public void RecibirDaño(float porcentaje, float retraso)
    {
        _velocidad -= porcentaje;
        _retrasoVelocidad = retraso;
    }
}
