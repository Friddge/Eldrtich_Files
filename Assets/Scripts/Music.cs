using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] AudioSource MusicSource;

    public AudioClip Bgmusic;

    private void Start()
    {
        MusicSource.clip = Bgmusic;
        MusicSource.Play();
    }
}
