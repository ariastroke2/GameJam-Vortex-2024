using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClotildeSounds : MonoBehaviour
{
    private AudioSource _audioSource;

    public AudioClip Boot;
    public AudioClip Drift1;
    public AudioClip Drift2;

    private float Next;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayOneShot(Boot);
        Next = Random.Range(2f, 5f);
    }

    void Update()
    {
        if(Next > 0)
        {
            Next -= Time.deltaTime;
        }
        else
        {
            Next = Random.Range(2f, 5f);
            if(Random.Range(0, 2) == 1)
            {
                _audioSource.PlayOneShot(Drift1);
            }
            else
            {
                _audioSource.PlayOneShot(Drift2);
            }
        }
    }
}
