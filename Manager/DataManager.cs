using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
   public static DataManager instance;

    private void Awake()
    {
        if(instance == null) 
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetMusicData(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
    }
    public void FXData(float value)
    {
        PlayerPrefs.SetFloat("FXVolume", value);
    }
    public void ExperienceData(float value) 
    {
        PlayerPrefs.SetFloat("Experience", value);
    }
    public void LevelData(int value) 
    {
        PlayerPrefs.SetInt("CurrentLevel", value);
    }

    public void ExperienceToNextLevel (float value) 
    {
        PlayerPrefs.SetFloat("ExperienceTNL", value);
    }

    public void MagicBallData(int value) 
    {
        PlayerPrefs.SetInt("MagicBallAmount", value);
    }

    public void CoinsData(int value) 
    {
        PlayerPrefs.SetInt("Coins", value);
    }
}
