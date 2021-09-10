using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private EnemyGenerator enemyGenerator;
    public bool isEnemyGenerate;
    public int generateIntervalTime;
    public int generateEnemyCount;
    public int maxEnemyCount;
    [SerializeField]
    private CharaGenerator charaGenerator;
    private int destroyEnemyCount;
    [SerializeField]
    public UIManager uiManager;
    [SerializeField]
    private List<CharaController> charaList = new List<CharaController>();

    public enum GameState
    {
        Preparate,
        Play,
        Stop,
        GameUp
    }

    public GameState currentGameState;

    [SerializeField]
    private List<EnemyController> enemiesList = new List<EnemyController>();

    void Start()
    {
        SetGameState(GameState.Preparate);
        StartCoroutine(charaGenerator.SetUpCharaGenerator(this));
        isEnemyGenerate = true;
        SetGameState(GameState.Play);
        StartCoroutine(enemyGenerator.PrepareteEnemyGenerate(this));
        StartCoroutine(TimeToCurrency());
    }

    /// <summary>
    /// 敵の情報をListに追加
    /// </summary>
    /// <param name="enemy"></param>
    public void AddEnemyList(EnemyController enemy)
    {
        enemiesList.Add(enemy);
        generateEnemyCount++;
    }

    /// <summary>
    /// 敵の生成を停止するか判定
    /// </summary>
    public void JudgeGenerateEnemyEnd()
    {
        if(generateEnemyCount >= maxEnemyCount)
        {
            isEnemyGenerate = false;
        }
    }

    public void SetGameState(GameState nextGameState)
    {
        currentGameState = nextGameState;
    }

    public void PauseEnemies()
    {
        for(int i = 0; i < enemiesList.Count; i++)
        {
            enemiesList[i].PauseMove();
        }
    }

    public void ResumeEnemies()
    {
        for (int i = 0; i < enemiesList.Count; i++)
        {
            enemiesList[i].ResumeMove();
        }
    }

    public void RemoveEnemyList(EnemyController removeEnemy)
    {
        enemiesList.Remove(removeEnemy);
        //Debug.Log(enemiesList.Count);
        //if(enemiesList.Count == 0 && isEnemyGenerate == false)
        //{
            //Debug.Log("GameUp");
        //}
    }

    public void CountUpDestroyEnemyCount(EnemyController enemyController)
    {
        RemoveEnemyList(enemyController);
        destroyEnemyCount++;
        Debug.Log("破壊した敵の数" + destroyEnemyCount);
        JudgeGameClear();
    }

    public void JudgeGameClear()
    {
        if(destroyEnemyCount >= maxEnemyCount)
        {
            Debug.Log("Game Clear");
        }
    }

    public IEnumerator TimeToCurrency()
    {
        int timer = 0;
        while(currentGameState == GameState.Play)
        {
            timer++;
            if(timer >= GameData.instance.currencyIntervalTimer && GameData.instance.currency < GameData.instance.maxCurrency)
            {
                timer = 0;
                GameData.instance.currency = Mathf.Clamp(GameData.instance.currency += GameData.instance.addCurrencyPoint, 0, GameData.instance.maxCurrency);
                uiManager.UpdateDisplayCurrency();
            }
            yield return null;
        }
    }

    /// <summary>
    /// 選択したキャラの情報をListに追加
    /// </summary>
    /// <param name="chara"></param>
    public void AddCharaList(CharaController chara)
    {
        charaList.Add(chara);
        //キャラ数カウント
        GameData.instance.charaPlacementCount++;
    } 

    /// <summary>
    /// 選択したキャラを破棄し，情報をListから削除
    /// </summary>
    /// <param name="chara"></param>
    public void RemoveCharaList(CharaController chara)
    {
        Destroy(chara.gameObject);
        charaList.Remove(chara);
    }

    /// <summary>
    /// 現在の配置しているキャラ数の取得
    /// </summary>
    /// <returns></returns>
    public int GetPlacementCharaCount()
    {
        return charaList.Count;
    }

    /// <summary>
    /// 配置解除を選択するポップアップ作成の準備（CharaControllerから呼び出される）
    /// </summary>
    /// <param name="chara"></param>
    public void PrepareteCreateCharaPopUp(CharaController chara)
    {
        SetGameState(GameState.Stop);
        PauseEnemies();
        uiManager.CreateReturnCharaPopUp(chara, this);
    }

    /// <summary>
    /// 選択したキャラの配置解除の確認（ReturnSelectCharaPopUpから呼び出される）
    /// </summary>
    /// <param name="isReturnChara"></param>
    /// <param name="chara"></param>
    public void JudgeReturnChara(bool isReturnChara, CharaController chara)
    {
        if (isReturnChara)
        {
            RemoveCharaList(chara);
        }
        SetGameState(GameState.Play);
        ResumeEnemies();
        StartCoroutine(TimeToCurrency());
    }
}
