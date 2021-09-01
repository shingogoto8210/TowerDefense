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
    private UIManager uiManager;

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
    /// “G‚Ìî•ñ‚ğList‚É’Ç‰Á
    /// </summary>
    /// <param name="enemy"></param>
    public void AddEnemyList(EnemyController enemy)
    {
        enemiesList.Add(enemy);
        generateEnemyCount++;
    }

    /// <summary>
    /// “G‚Ì¶¬‚ğ’â~‚·‚é‚©”»’è
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
        Debug.Log("”j‰ó‚µ‚½“G‚Ì”" + destroyEnemyCount);
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
}
