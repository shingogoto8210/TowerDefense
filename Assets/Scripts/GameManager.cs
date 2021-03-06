using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    IEnumerator Start()
    {
        SetGameState(GameState.Preparate);
        //RefreshGameData();
        SetUpStageData();
        StartCoroutine(charaGenerator.SetUpCharaGenerator(this));
        defenseBase.SetUpDefenseBase(this, currentStageData.defenseBaseDurability, uiManager);
        yield return StartCoroutine(uiManager.CreateOpeningLogo());
        yield return StartCoroutine(uiManager.openingLogo.PlayOpening());
        isEnemyGenerate = true;
        SetGameState(GameState.Play);
        StartCoroutine(enemyGenerator.PrepareteEnemyGenerate(this,currentStageData));
        Debug.Log("GΆ¬");
        StartCoroutine(TimeToCurrency());
    }

    /// <summary>
    /// GΜξρπListΙΗΑ
    /// </summary>
    /// <param name="enemy"></param>
    public void AddEnemyList(EnemyController enemy)
    {
        enemiesList.Add(enemy);
        generateEnemyCount++;
    }

    /// <summary>
    /// GΜΆ¬πβ~·ι©»θ
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
        Debug.Log("jσ΅½GΜ" + destroyEnemyCount);
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
    /// Iπ΅½LΜξρπListΙΗΑ
    /// </summary>
    /// <param name="chara"></param>
    public void AddCharaList(CharaController chara)
    {
        charaList.Add(chara);
        //LJEg
        GameData.instance.charaPlacementCount++;
    } 

    /// <summary>
    /// Iπ΅½Lπjό΅CξρπList©ην
    /// </summary>
    /// <param name="chara"></param>
    public void RemoveCharaList(CharaController chara)
    {
        Destroy(chara.gameObject);
        charaList.Remove(chara);
    }

    /// <summary>
    /// »έΜzu΅Δ’ιLΜζΎ
    /// </summary>
    /// <returns></returns>
    public int GetPlacementCharaCount()
    {
        return charaList.Count;
    }

    /// <summary>
    /// zuππIπ·ι|bvAbvμ¬ΜυiCharaController©ηΔΡo³κιj
    /// </summary>
    /// <param name="chara"></param>
    public void PrepareteCreateCharaPopUp(CharaController chara)
    {
        SetGameState(GameState.Stop);
        PauseEnemies();
        uiManager.CreateReturnCharaPopUp(chara, this);
    }

    /// <summary>
    /// Iπ΅½LΜzuπΜmFiReturnSelectCharaPopUp©ηΔΡo³κιj
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
    /// Xe[Wf[^Μέθ
    /// </summary>
    private void SetUpStageData()
    {
        currentStageData = DataBaseManager.instance.stageDataSO.stageDatasList[GameData.instance.stageNo]; //Q[f[^©ηXe[Wf[^πζΎ
        generateIntervalTime = currentStageData.generateIntervalTime;@                                    //GπΆ¬·ιΤuπέθ
        maxEnemyCount = currentStageData.mapInfo.appearEnemyInfos.Length;                                  //o»·ιGΜπέθ

        currentMapInfo = Instantiate(currentStageData.mapInfo);                                           //}bvπμ¬
        charaGenerator.SetUpMapInfoandGrid(currentMapInfo.GetMapInfoandGrid());
        //charaGenerator.SetUpMapInfo(currentMapInfo.GetMapInfo());
        //charaGenerator.SetUpMapInfoGrid(currentMapInfo.GetMapInfoGrid());
        defenseBase = Instantiate(defenseBasePrefab, currentMapInfo.GetDefenseBaseTran());@@@@@@@@ //DefenseBase πzu

        PathData[] pathDatas = new PathData[currentStageData.mapInfo.appearEnemyInfos.Length];             //GΜoHΜξρπόκι 
        for(int i = 0; i < currentStageData.mapInfo.appearEnemyInfos.Length; i++)
        {
            pathDatas[i] = currentStageData.mapInfo.appearEnemyInfos[i].enemyPathData;@                   //G1ΜΈΒoHπέθ 
        }
        enemyGenerator.SetUpPathDatas(pathDatas);
    }

    private IEnumerator GameClearAndResult()
    {
        GameUpToCommon();
        yield return StartCoroutine(uiManager.CreateClearLogo());
        yield return StartCoroutine(uiManager.clearLogo.PlayClear());
        GameData.instance.totalClearPoint += currentStageData.clearPoint;
        GameData.instance.stageNo++;
        if (!GameData.instance.clearStageNosList.Contains(GameData.instance.stageNo))
        {
            GameData.instance.clearStageNosList.Add(GameData.instance.stageNo);
        }
        SceneStateManager.instance.PreparateNextScene(SceneType.StageSelect);
        GameData.instance.SaveClearPoint();
    }

    private void GameUpToCommon()
    {
        SetGameState(GameState.GameUp);
        charaGenerator.InactivatePlacementCharaSelectPopUp();
        GameData.instance.currency = 0;
        for(int i = 0; i < enemiesList.Count; i++)
        {
            enemiesList[i].tween.Kill();
        }
    }

    public IEnumerator GameOver()
    {
        GameUpToCommon();
        yield return StartCoroutine(uiManager.CreateGameOverLogo());
        yield return StartCoroutine(uiManager.gameoverLogo.PlayGameOver());
        SceneStateManager.instance.PreparateNextScene(SceneType.StageSelect);
    }
}
