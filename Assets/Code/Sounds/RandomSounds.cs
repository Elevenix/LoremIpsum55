using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSounds : MonoBehaviour
{
    [SerializeField] private bool playOnAwake = false;

    [SerializeField] private AudioClip[] sounds;

    [SerializeField] private float minPitch = 1;
    [SerializeField] private float maxPitch = 1;

    private AudioSource _audioS;

    // Start is called before the first frame update
    void Awake()
    {
        _audioS = GetComponent<AudioSource>();
        _audioS.playOnAwake = false;
    }

    private void Start()
    {
        if (playOnAwake)
            PlaySound();
        
    }

    public void PlaySound()
    {
        RandomSound();
        _audioS.Play();
    }

    public void PlaySound(AudioClip clip)
    {
        _audioS.clip = clip;
        _audioS.Play();
    }

    private void RandomSound()
    {
        _audioS.clip = sounds[Random.Range(0, sounds.Length)];
        _audioS.pitch = Random.Range(minPitch, maxPitch);
    }
}
