using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;
    [Header("�R�X�g�p�̒ʉ�")]
    public int currency;
    [Header("�ʉ݂̍ő�l")]
    public int maxCurrency;
    [Header("�ʉ݉��Z�܂ł̑ҋ@����")]
    public int currencyIntervalTimer;
    [Header("�ʉ݂̉��Z��")]
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
