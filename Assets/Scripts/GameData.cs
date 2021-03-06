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
    [Header("生成したキャラの数")]
    public int charaPlacementCount;
    [Header("デバッグモードの切り替え")]
    public bool isDebug; //trueの場合，デバッグモードとする
    public int defenseBaseDurability;
    public int stageNo;
    public List<int> clearStageNosList = new List<int>();
    public int totalClearPoint;
    public List<int> engageCharaNosList = new List<int>();
    private const string CLEAR_POINT_KEY = "clearPoint";

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

        //Debug用
        //SaveClearPoint();

        //LoadClearPoint();

        clearStageNosList.Add(0);
        //totalClearPoint = 0;
        engageCharaNosList.Add(0);
    }

    public void SaveClearPoint()
    {
        PlayerPrefs.SetInt(CLEAR_POINT_KEY, totalClearPoint);
        PlayerPrefs.Save();
        Debug.Log("セーブ：" + CLEAR_POINT_KEY + ":"+totalClearPoint);
    }

    public void LoadClearPoint()
    {
        totalClearPoint = PlayerPrefs.GetInt(CLEAR_POINT_KEY, 0);
        Debug.Log("ロード：" + CLEAR_POINT_KEY + ":" + totalClearPoint);
    }
}
