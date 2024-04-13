using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomSounds : MonoBehaviour
{
    [SerializeField] private bool playOnAwake = false;

    [SerializeField] private GrounpSound[] groupSounds;

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

    /// <summary>
    /// Play the first group of sound
    /// </summary>
    public void PlaySound()
    {
        RandomSound(0);
        _audioS.Play();
    }

    /// <summary>
    /// Name
    /// </summary>
    /// <param name="name"> Name given of the group sound</param>
    public void PlaySound(string name)
    {
        int id = GetGroupSoundId(name);
        RandomSound(id);
        _audioS.Play();
    }

    /// <summary>
    /// Launch random sound from an array of clips
    /// </summary>
    /// <param name="clips"></param>
    public void PlaySound(AudioClip[] clips)
    {
        RandomSound(clips);
        _audioS.Play();
    }

    /// <summary>
    /// Play the sound given
    /// </summary>
    /// <param name="clip"></param>
    public void PlaySound(AudioClip clip)
    {
        _audioS.clip = clip;
        _audioS.Play();
    }

    private void RandomSound(int id)
    {
        _audioS.clip = groupSounds[id].sounds[UnityEngine.Random.Range(0, groupSounds[id].sounds.Length)];
        _audioS.pitch = UnityEngine.Random.Range(groupSounds[id].pitch.x, groupSounds[id].pitch.y);
        _audioS.volume = groupSounds[id].volume;
    }

    private void RandomSound(AudioClip[] clips)
    {
        _audioS.clip = clips[UnityEngine.Random.Range(0, clips.Length)];
        _audioS.pitch = 1;
    }

    private int GetGroupSoundId(string name)
    {
        int i = 0;
        while (i < groupSounds.Length)
        {
            if (groupSounds[i].nameGroupSound.Equals(name))
                return i;
        }
        return -1;
    }
}

[Serializable]
public class GrounpSound
{
    public string nameGroupSound;
    [Range(0f, 1f)] public float volume = .5f;
    public Vector2 pitch = Vector2.one;
    public AudioClip[] sounds;
}
