using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCurrencyHandler : MonoBehaviour
{
    public static GameCurrencyHandler instance = null;
    [HideInInspector]
    public int Coins = 0;


    public delegate void _Delegate();
    public _Delegate Update_;


    public UIManager UI = null;
    private void Awake()
    {
        instance = this;

        Coins = GameData.instance.GetCoins();




        Update_ += UpdateUI;

    }
    private void Start()
    {
        Update_();
    }
    public void AddToCoins(int amount)
    {
        Coins = Coins + amount;
        if (Coins < 0)
            Coins = 0;
        GameData.instance.SetCoins(Coins);
        UpdateUI();
    }

    public void UpdateUI()
    {
        UIManager.instance.CoinText.text = "" + Coins;

    }

}
