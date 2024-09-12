using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Unity.Collections.LowLevel.Unsafe;

public class Experience : MonoBehaviour
{
    public Text levelText;
    public Image expImg;
    public float currentExperience;
    public float expToNextLevel;
    public int currentLevel;
    public static Experience instance;

    public AudioSource levelUpAS;

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
            
    }

    void Start()
    {
        expImg.fillAmount = currentExperience / expToNextLevel;
        currentLevel = 1;
        levelText.text = currentLevel.ToString();

        currentExperience = PlayerPrefs.GetFloat("Experience", 0);
        expToNextLevel = PlayerPrefs.GetFloat("ExperienceTNL", expToNextLevel);
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");
    }

    
    void Update()
    {

        expImg.fillAmount = currentExperience / expToNextLevel;
        levelText.text = currentLevel.ToString();


    }

    public void expMod(float experience)
    {
        currentExperience = PlayerPrefs.GetFloat("Experience", 0);
        currentExperience += experience;
        expToNextLevel = PlayerPrefs.GetFloat("ExperienceTNL", expToNextLevel);

        expImg.fillAmount = currentExperience / expToNextLevel;
        if (currentExperience >= expToNextLevel) 
        {
            expToNextLevel *= 2;
            currentExperience = 0;
            currentLevel++;
            levelText.text = currentLevel.ToString();

            
            AudioManager.instance.PlayAudio(levelUpAS);

            //currentLevel = PlayerPrefs.GetInt("CurrentLevel", currentLevel);
        }

        DataManager.instance.ExperienceData(currentExperience);
        DataManager.instance.ExperienceToNextLevel(expToNextLevel);
        DataManager.instance.LevelData(currentLevel);

        currentExperience = PlayerPrefs.GetFloat("Experience");
        expToNextLevel = PlayerPrefs.GetFloat("ExperienceTNL");
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");
    }
}
