using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    [SerializeField] private AudioClip music;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource source = MusicController.musicController.audioSource;
        if(source.clip != music)
        {
            source.Stop();
            source.clip = music;
            source.Play();
        }
    }
}
