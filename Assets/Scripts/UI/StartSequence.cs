using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartSequence : MonoBehaviour
{
    [Header("Sonidos")]
    public AudioClip _one;
    public AudioClip _two;
    public AudioClip _three;
    public AudioClip _four;

    [Header("Ajustes inicio")]
    public bool _startSequence;
    public float _startDelay;

    private float _timer;

    private AudioSource _audioSource;

    [Header("Elementos UI")]
    public TextMeshProUGUI _countdownText;
    public GameObject _UIIntro;
    public GameObject _UIJuego;

    [Header("Jugadores")]
    public MovimientoGato _gato;
    public MovimientoRaton _raton;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _UIJuego.SetActive(false);
        _UIIntro.SetActive(true);
        StartCoroutine(IniciarCorutina());
    }


    IEnumerator IniciarCorutina()
    {
        float bpmunit = 1f / (182f / 60f);
        while (_timer < _startDelay)
        {
            _timer += Time.deltaTime;
            yield return null;
        }
        _audioSource.PlayOneShot(_three);
        _countdownText.text = "3";
        while (_timer < bpmunit * 2 + _startDelay)
        {
            _timer += Time.deltaTime;
            yield return null;
        }
        _audioSource.PlayOneShot(_two);
        _countdownText.text = "2";
        while (_timer < bpmunit * 4 + _startDelay)
        {
            _timer += Time.deltaTime;
            yield return null;
        }
        _audioSource.PlayOneShot(_one);
        _countdownText.text = "1";
        while (_timer < bpmunit * 6 + _startDelay)
        {
            _timer += Time.deltaTime;
            yield return null;
        }
        _audioSource.PlayOneShot(_four);
        _countdownText.text = "GO!";
        _gato.Iniciar();
        _raton.Iniciar();
        while (_timer < bpmunit * 8 + _startDelay)
        {
            _timer += Time.deltaTime;
            yield return null;
        }
        _countdownText.text = "";
        _UIJuego.SetActive(true);
        _UIIntro.SetActive(false);
    }

}
