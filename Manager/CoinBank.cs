using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinBank : MonoBehaviour
{
    public int bank;
    public Text bankText;

    public static CoinBank instance;

    private void Awake()
    {
        if (instance == null ) 
        {
            instance = this;
        }
    }

    void Start()
    {
        bankText.text = "X " + bank.ToString();
        bank = PlayerPrefs.GetInt("Coins", 0);

    }

    
    void Update()
    {
        bankText.text = "X " + bank.ToString();
    }

    public void Money(int coinCollected) 
    {
        bank += coinCollected;
        bankText.text = "X " + bank.ToString();

        DataManager.instance.CoinsData(bank);
        bank = PlayerPrefs.GetInt("Coins");
    }
}
