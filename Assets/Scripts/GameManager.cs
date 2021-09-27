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

    private DefenseBase defenseBase;
    [SerializeField]
    private MapInfo currentMapInfo;
    [SerializeField]
    private DefenseBase defenseBasePrefab;
    [SerializeField]
    private StageDataSO.StageData currentStageData;
    [SerializeField]
    private LogoEffect logoEffect;

    IEnumerator Start()
    {
        SetGameState(GameState.Preparate);
        //RefreshGameData();
        SetUpStageData();
        StartCoroutine(charaGenerator.SetUpCharaGenerator(this));
        defenseBase.SetUpDefenseBase(this, currentStageData.defenseBaseDurability, uiManager);
        yield return StartCoroutine(logoEffect.PlayOpening());
        isEnemyGenerate = true;
        SetGameState(GameState.Play);
        StartCoroutine(enemyGenerator.PrepareteEnemyGenerate(this,currentStageData));
        Debug.Log("敵生成");
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
            StartCoroutine(GameClearAndResult());
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

    /// <summary>
    /// ステージデータの設定
    /// </summary>
    private void SetUpStageData()
    {
        currentStageData = DataBaseManager.instance.stageDataSO.stageDatasList[GameData.instance.stageNo]; //ゲームデータからステージデータを取得
        generateIntervalTime = currentStageData.generateIntervalTime;　                                    //敵を生成する間隔を設定
        maxEnemyCount = currentStageData.mapInfo.appearEnemyInfos.Length;                                  //出現する敵の数を設定

        currentMapInfo = Instantiate(currentStageData.mapInfo);                                           //マップを作成
        charaGenerator.SetUpMapInfoandGrid(currentMapInfo.GetMapInfoandGrid());
        //charaGenerator.SetUpMapInfo(currentMapInfo.GetMapInfo());
        //charaGenerator.SetUpMapInfoGrid(currentMapInfo.GetMapInfoGrid());
        defenseBase = Instantiate(defenseBasePrefab, currentMapInfo.GetDefenseBaseTran());　　　　　　　　 //DefenseBase を配置

        PathData[] pathDatas = new PathData[currentStageData.mapInfo.appearEnemyInfos.Length];             //敵の経路の情報を入れる箱
        for(int i = 0; i < currentStageData.mapInfo.appearEnemyInfos.Length; i++)
        {
            pathDatas[i] = currentStageData.mapInfo.appearEnemyInfos[i].enemyPathData;　                   //敵1体ずつ経路を設定 
        }
        enemyGenerator.SetUpPathDatas(pathDatas);
    }

    private IEnumerator GameClearAndResult()
    {
        GameUpToCommon();
        yield return StartCoroutine(logoEffect.PlayClear());
        GameData.instance.totalClearPoint += currentStageData.clearPoint;
        GameData.instance.stageNo++;
        if (!GameData.instance.clearStageNosList.Contains(GameData.instance.stageNo))
        {
            GameData.instance.clearStageNosList.Add(GameData.instance.stageNo);
        }
        SceneStateManager.instance.PreparateNextScene(SceneType.StageSelect);
    }

    private void GameUpToCommon()
    {
        SetGameState(GameState.GameUp);
        charaGenerator.InactivatePlacementCharaSelectPopUp();
    }

    public void GameOver()
    {
        GameUpToCommon();
        SceneStateManager.instance.PreparateNextScene(SceneType.StageSelect);
    }
}
