using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicBallBank : MonoBehaviour
{
    public int magicBank;
    public Text magicBallText;
    public static MagicBallBank instance;
    

    private void Awake()
    {
        instance = this; 
    }
    void Start()
    {
        magicBallText.text = " x " + magicBank.ToString();
        magicBank = PlayerPrefs.GetInt("MagicBallAmount", 0);
    }

    // Update is called once per frame
    void Update()
    {
        magicBallText.text = " x " + magicBank.ToString();
    }

    public void Collect(int magicCollect) 
    {
        magicBank += magicCollect;
        magicBallText.text = " x " + magicBank.ToString();

        DataManager.instance.MagicBallData(magicBank);
        magicBank = PlayerPrefs.GetInt("MagicBallAmount");
    }
}
