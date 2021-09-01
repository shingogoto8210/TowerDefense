using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;
    [Header("コスト用の通貨")]
    public int currency;
    [Header("通貨の最大値")]
    public int maxCurrency;
    [Header("通貨加算までの待機時間")]
    public int currencyIntervalTimer;
    [Header("通貨の加算分")]
    public int addCurrencyPoint;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
