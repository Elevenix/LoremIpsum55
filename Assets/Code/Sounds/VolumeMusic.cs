using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeMusic : MonoBehaviour
{
    [SerializeField] private Slider sliderMusic, sliderSounds;
    [SerializeField] private AudioMixer mixer;

    void Start()
    {
        if (!PlayerPrefs.HasKey("Music") || !PlayerPrefs.HasKey("Sounds"))
        {
            PlayerPrefs.SetFloat("Music", -30);
            PlayerPrefs.SetFloat("Sounds", -30);
        }
        sliderMusic.value = PlayerPrefs.GetFloat("Music");
        sliderSounds.value = PlayerPrefs.GetFloat("Sounds");

        ValueChangeMusic(sliderMusic.value);
        ValueChangeSound(sliderSounds.value);

    }

    public void ValueChangeMusic(float soundLevel)
    {
        PlayerPrefs.SetFloat("Music", soundLevel);
        mixer.SetFloat("Music", soundLevel);
    }

    public void ValueChangeSound(float soundLevel)
    {
        PlayerPrefs.SetFloat("Sounds", soundLevel);
        mixer.SetFloat("Sounds", soundLevel);
    }

}
