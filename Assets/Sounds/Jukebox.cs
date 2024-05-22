using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Jukebox : MonoBehaviour
{
    public AudioClip[] audioClips;
    [Header("Tiempo (en segundos) entre canciones")]
    [Range(1,5)]
    public float delay = 1;
    private int currentAC = 0;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        PlayCurrentSong();
        InvokeRepeating("TryNextSong",delay,delay);
    }

    void TryNextSong()
    {
        if (!audioSource.isPlaying)
        {
            currentAC = currentAC+1<audioClips.Length ? currentAC+1 : 0;
            PlayCurrentSong();    
        }
    }
    void PlayCurrentSong()
    {
        audioSource.clip = audioClips[currentAC];
        audioSource.Play();
    }
}
