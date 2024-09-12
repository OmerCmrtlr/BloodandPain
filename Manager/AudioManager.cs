using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer musicMixer, effectMixer;

    public Slider masterSldr, effectSldr;

    public AudioSource BGFirstLevelMusicAS;
    public static AudioManager instance;

    [Range(-80,20)]
    public float effectVol, masterVol;

    public Image effectHandle, musicHandle;

    public Sprite soundOff, soundOn, musicOn, musicOff;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }

    public void MasterVolume()
    {
        DataManager.instance.SetMusicData(masterSldr.value);
        musicMixer.SetFloat("masterVolume", PlayerPrefs.GetFloat("MusicVolume"));
    }

    public void EffectVolume() 
    {
        DataManager.instance.FXData(effectSldr.value);
        effectMixer.SetFloat("effectVolume", PlayerPrefs.GetFloat("FXVolume"));
    }






    void Start()
    {
        PlayAudio(BGFirstLevelMusicAS);
        //masterSldr.value = masterVol;
        //effectSldr.value = effectVol;

        masterSldr.minValue = -80;
        masterSldr.maxValue = 20;

        effectSldr.minValue = -80;
        effectSldr.maxValue = 20;

        masterSldr.value = PlayerPrefs.GetFloat("MusicVolume", 0f);
        effectSldr.value = PlayerPrefs.GetFloat("FXVolume", 0f);
    }

    
    void Update()
    {
        //masterVolume();
        //effectVolume();

        MainVolume();
        OtherVolume();
      
    }

    public void PlayAudio(AudioSource audio) 
    {
        audio.Play();
    }

    public void MainVolume() 
    {
        if (masterSldr.value < -70)
        {
            musicHandle.sprite = musicOff;
        }

        else
        {
            musicHandle.sprite = musicOn;
        }
    }

    public void OtherVolume() 
    {

        if (effectSldr.value < -70)
        {
            effectHandle.sprite = soundOff;
        }

        else
        {
            effectHandle.sprite = soundOn;
        }

    }
}
