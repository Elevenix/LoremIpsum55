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
        if (!PlayerPrefs.HasKey("music") || !PlayerPrefs.HasKey("sounds"))
        {
            PlayerPrefs.SetFloat("music", -20);
            PlayerPrefs.SetFloat("sounds", -20);
        }
        else
        {
            sliderMusic.value = PlayerPrefs.GetFloat("music");
            sliderSounds.value = PlayerPrefs.GetFloat("sounds");

            ValueChangeMusic(sliderMusic.value);
            ValueChangeSound(sliderSounds.value);
        }

    }

    public void ValueChangeMusic(float soundLevel)
    {
        PlayerPrefs.SetFloat("music", soundLevel);
        mixer.SetFloat("Music", soundLevel);
    }

    public void ValueChangeSound(float soundLevel)
    {
        PlayerPrefs.SetFloat("sounds", soundLevel);
        mixer.SetFloat("Sounds", soundLevel);
    }

}
