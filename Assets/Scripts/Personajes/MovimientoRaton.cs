using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoRaton : MonoBehaviour
{
    [Header("Par�metros gato")]
    public float _aceleracion;
    public float _tiempoObjetivo;

    public float _velocidadAnimacion;

    public float _tiempoMovimiento;

    public bool Reiniciar;

    public AudioClip _jump;
    public AudioClip _die;

    public float StarRadius;

    private float _temporizador;
    private float _temporizadorCorutina;

    private KeyCode _ultimaTecla;

    private float _ObjetivoY;
    private float _OriginalY;
    private float _ObjetivoX;
    private float _OriginalX;
    private IEnumerator _verticalidad;

    private float _YNormal;
    private float _YTrans;
    private float _XTrans;

    public bool _startGame;

    private AudioSource _audioSource;

    private float _Retraso;

    private Transform _star1;
    private Transform _star2;

    float _gastado;
    public GameObject cleotilde;
    public GameObject piedrota;
    public GameObject dardos;
    public GameObject piedrita;


    // Start is called before the first frame update
    void Start()
    {
        _gastado=0;
        _temporizador = 0;

        _ultimaTecla = KeyCode.F1;

        _YNormal = transform.position.y;
        _YTrans = 20f;

        _XTrans = 5f;
        _startGame = false;

        _audioSource = GetComponent<AudioSource>();

        _star1 = transform.Find("Star1");
        _star2 = transform.Find("Star2");

        _star1.gameObject.SetActive(false);
        _star2.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(_startGame)
        {
            _temporizador += Time.deltaTime;
            if(_Retraso > 0)
            {
                transform.rotation = Quaternion.Euler(180f, 0, 0);
                _Retraso -= Time.deltaTime;
                _star1.gameObject.SetActive(true);
                _star2.gameObject.SetActive(true);
                _star1.localPosition = new Vector3(Mathf.Cos(_temporizador * 20f) * StarRadius, -1.5f, Mathf.Sin(_temporizador * 20f) * StarRadius);
                _star2.localPosition = new Vector3(-Mathf.Cos(_temporizador * 20f) * StarRadius, -1.5f, -Mathf.Sin(_temporizador * 20f) * StarRadius);
            }
            else
            {
                _star1.gameObject.SetActive(false);
                _star2.gameObject.SetActive(false);
                transform.rotation = Quaternion.identity;
                Inputs();
            }
        }
    }

    public void Iniciar()
    {
        _startGame = true;
    }

    private void Inputs()
    {
        if (_ultimaTecla == KeyCode.F1)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (_verticalidad != null)
                    StopCoroutine(_verticalidad);
                _verticalidad = SaltarCorutina();
                StartCoroutine(_verticalidad);
                _ultimaTecla = KeyCode.UpArrow;
                _audioSource.PlayOneShot(_jump);
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
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                Lanzar();
            }
        }
    }

    public void GatoGana()
    {
        StopAllCoroutines();
        _audioSource.PlayOneShot(_die);
        _startGame = false;
        transform.Rotate(90f, 0, 0);
        transform.position = new Vector3(transform.position.x, 3.5f, transform.position.z);
        StartCoroutine(MatarCorutina());
    }

    IEnumerator MatarCorutina()
    {
        _temporizadorCorutina = 0f;
        while (_temporizadorCorutina < 1f)
        {
            _temporizadorCorutina += Time.deltaTime;
            yield return null;
        }
        Destroy(this);
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
        transform.position = new Vector3(_ObjetivoX, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tabla"))
        {
            _Retraso = 5f;
        }
    }

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
}